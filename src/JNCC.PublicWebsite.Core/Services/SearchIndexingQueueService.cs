using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.SQS;
using Amazon.SQS.ExtendedClient;
using Amazon.SQS.Model;
using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace JNCC.PublicWebsite.Core.Services
{
    public sealed class SearchIndexingQueueService : ISearchIndexingQueueService, IDisposable
    {
        private readonly JsonSerializerSettings _jsonSettings;
        private readonly AmazonSQSExtendedClient _sqsExtendedClient;
        private readonly ILogger<SearchIndexingQueueService> _logger;
        private readonly IOptions<AmazonServiceConfigurationOptions> _amazonServiceConfigurationOptions;

        public SearchIndexingQueueService(ILogger<SearchIndexingQueueService> logger, IOptions<AmazonServiceConfigurationOptions> amazonServiceConfigurationOptions)
        {
            _jsonSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            _amazonServiceConfigurationOptions = amazonServiceConfigurationOptions;
            _logger = logger;

            _sqsExtendedClient = CreateExtendedClient();

        }

        public void QueueUpsert(SearchIndexDocumentModel document)
        {
            if (string.IsNullOrWhiteSpace(document.Content))
            {
                _logger.LogInformation("Skipping Queue Upsert for node (ID: {0}, Title: {1}). Reason: Content is Null or WhiteSpace.",document.NodeId, document.Title);
                return;
            }

            var request = new SearchIndexQueueRequestModel
            {
                Verb = SearchIndexingVerbs.Upsert,
                Index = _amazonServiceConfigurationOptions.Value.AWSESIndex,
                Document = document
            };

            QueueRequest(request);
        }

        public void QueueDelete(SearchIndexDocumentModel document)
        {
            var request = new SearchIndexQueueRequestModel
            {
                Verb = SearchIndexingVerbs.Delete,
                Index = _amazonServiceConfigurationOptions.Value.AWSESIndex,
                Document = document
            };

            QueueRequest(request);
        }

        public void Dispose()
        {
            _sqsExtendedClient.Dispose();
        }

        private void QueueRequest(SearchIndexQueueRequestModel request)
        {
            var message = JsonConvert.SerializeObject(request, Formatting.None, _jsonSettings);

            if (!string.IsNullOrEmpty(_amazonServiceConfigurationOptions.Value.AWSSQSEndpoint))
            {
                var sendRequest = new SendMessageRequest(_amazonServiceConfigurationOptions.Value.AWSSQSEndpoint, message);

                if (_sqsExtendedClient != null)
                {
                    var response = _sqsExtendedClient.SendMessageAsync(sendRequest).Result;

                    if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                    {
                        _logger.LogWarning("[Failure] Document Request (ID: {0}, Title: {1}) has not been pushed up to SQS. Response HTTP Status Code: {2}. MD5 of message attributes: {3}. MD5 of message body: {4}.", request.Document.NodeId, request.Document.Title, response.HttpStatusCode, response.MD5OfMessageAttributes, response.MD5OfMessageBody);
                    }
                    else
                    {
                        _logger.LogInformation("[Success] Document Request (ID: {0}, Title: {1}) has been pushed up to SQS.", request.Document.NodeId, request.Document.Title);
                    }
                }
                else
                {
                    _logger.LogWarning("[Failure] Amazon SQS Extended Client could not be created.");
                }

            }
            else
            {
                _logger.LogWarning("[Failure] AWS SQS Endpoint not supplied.");
            }

           
        }

        private async Task QueueRequestAsync(SearchIndexQueueRequestModel request)
        {
            var message = JsonConvert.SerializeObject(request, Formatting.None, _jsonSettings);

            var sendRequest = new SendMessageRequest(_amazonServiceConfigurationOptions.Value.AWSSQSEndpoint, message);

            var response = await _sqsExtendedClient.SendMessageAsync(sendRequest);

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                _logger.LogWarning("[Failure] Document Request (ID: {0}, Title: {1}) has not been pushed up to SQS. Response HTTP Status Code: {2}. MD5 of message attributes: {3}. MD5 of message body: {4}.", request.Document.NodeId, request.Document.Title, response.HttpStatusCode, response.MD5OfMessageAttributes, response.MD5OfMessageBody);
            }
            else
            {
                _logger.LogInformation("[Success] Document Request (ID: {0}, Title: {1}) has been pushed up to SQS.", request.Document.NodeId, request.Document.Title);
            }
        }

        private AmazonSQSExtendedClient CreateExtendedClient()
        {
            if (!string.IsNullOrEmpty(_amazonServiceConfigurationOptions.Value.AWSSQSAccessKey) 
                && !string.IsNullOrEmpty(_amazonServiceConfigurationOptions.Value.AWSSQSSecretKey)
                && !string.IsNullOrEmpty(_amazonServiceConfigurationOptions.Value.AWSESRegion)
                && !string.IsNullOrEmpty(_amazonServiceConfigurationOptions.Value.AWSSQSPayloadBucket))
            {
                var credentials = new BasicAWSCredentials(_amazonServiceConfigurationOptions.Value.AWSSQSAccessKey, _amazonServiceConfigurationOptions.Value.AWSSQSSecretKey);
                var region = RegionEndpoint.GetBySystemName(_amazonServiceConfigurationOptions.Value.AWSESRegion);
                var s3 = new AmazonS3Client(credentials, region);
                var sqs = new AmazonSQSClient(credentials, region);

                return new AmazonSQSExtendedClient(sqs, new ExtendedClientConfiguration().WithLargePayloadSupportEnabled(s3, _amazonServiceConfigurationOptions.Value.AWSSQSPayloadBucket));
            }
            else
            {
                return null;
            }
          
        }
    }
}
