using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Options;
using JNCC.PublicWebsite.Core.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using System.Linq;
using JNCC.PublicWebsite.Core.Utilities;
using Newtonsoft.Json.Linq;

namespace JNCC.PublicWebsite.Core.Notifications
{
    public class ContentPublishedNotificationHandler : INotificationHandler<ContentPublishedNotification>
    {
        //private readonly ISearchIndexingQueueService _searchIndexingQueueService;

        //private readonly IOptions<AmazonServiceConfigurationOptions> _amazonServiceConfigurationOptions;

        private readonly ILogger<ContentPublishedNotificationHandler> _logger;

        public ContentPublishedNotificationHandler(ILogger<ContentPublishedNotificationHandler> logger)
        {
            _logger = logger;
        }

        public void Handle(ContentPublishedNotification notification)
        {
            _logger.LogWarning("Hello world from handler");
        }

        //private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        //private const string _site = SearchIndexingSites.Website;


        //public ContentPublishedNotificationHandler(ISearchIndexingQueueService searchIndexingQueueService, IOptions<AmazonServiceConfigurationOptions> amazonServiceConfigurationOptions, ILogger<ContentPublishedNotificationHandler> logger, IUmbracoContextAccessor umbracoContextAccessor)
        //{
        //    _searchIndexingQueueService = searchIndexingQueueService;
        //    _amazonServiceConfigurationOptions = amazonServiceConfigurationOptions;
        //    _logger = logger;
        //    _umbracoContextAccessor = umbracoContextAccessor;
        //}

        //public void Handle(ContentPublishedNotification notification)
        //{
        //    if (!_amazonServiceConfigurationOptions.Value.EnableIndexing)
        //    {
        //        _logger.LogWarning("Amazon indexing disabled");
        //    }

        //    // _umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext);

        //    _umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext);

        //    string leftPartUrl = umbracoContext.OriginalRequestUrl.GetLeftPart(UriPartial.Authority);


        //    foreach (var entity in notification.PublishedEntities)
        //    {
        //        var contentBuilder = new StringBuilder();

        //        var fieldsToIndex = entity.Properties.Where(p => _amazonServiceConfigurationOptions.Value.IndexFields.Contains(p.Alias));

        //        foreach (var contentField in fieldsToIndex)
        //        {
        //            var contentFieldValues = contentField.Values;

        //            foreach (var contentFieldValue in contentFieldValues)
        //            {
        //                var contentFieldValueString = contentFieldValue.ToString();

        //                _logger.LogInformation("{{alias} - {value}", contentField.Alias, contentFieldValueString);

        //                // Check if it has a value and append it
        //                //if (string.IsNullOrEmpty(contentFieldValueString) == false)
        //                //{
        //                //    if (contentFieldValueString.DetectIsJson() && JsonUtility.TryParseJson(contentFieldValueString, out object parsedJson))
        //                //    {
        //                //        var processedJsonValue = ProcessJsonValue(parsedJson);

        //                //        if (string.IsNullOrEmpty(processedJsonValue) == false)
        //                //        {
        //                //            contentBuilder.AppendLine(processedJsonValue);
        //                //        }
        //                //    }
        //                //    else
        //                //    {
        //                //        var sanitisedValue = contentFieldValueString.StripHtml().Trim();

        //                //        if (string.IsNullOrWhiteSpace(sanitisedValue) == false)
        //                //        {
        //                //            contentBuilder.AppendLine(sanitisedValue);
        //                //        }
        //                //    }
        //                //}
        //            }

        //        }

        //        //string content = contentBuilder.ToString().Trim();

        //        //var document = new SearchIndexDocumentModel()
        //        //{
        //        //    NodeId = entity.Id,
        //        //    Site = _site,
        //        //    Published = DateTime.Parse(entity.PublishDate.ToString()),
        //        //    Title = entity.Name,
        //        //    Url = leftPartUrl + umbracoContext.Content.GetById(entity.Id).Url(),
        //        //    Content = content
        //        //};


        //        //_searchIndexingQueueService.QueueUpsert(document);
        //    }
        //}


        //private string ProcessJsonValue(object obj)
        //{
        //    var processedValue = new StringBuilder();
        //    var objType = obj.GetType();

        //    if (objType == typeof(JObject))
        //    {
        //        var jObj = obj as JObject;
        //        if (jObj != null)
        //        {
        //            foreach (var field in _searchConfiguration.NestedIndexFields)
        //            {
        //                var nestedField = jObj[field.Alias];
        //                if (nestedField == null)
        //                {
        //                    continue;
        //                }

        //                var valueType = nestedField.GetType();
        //                var value = nestedField.Value<string>();

        //                if (typeof(JContainer).IsAssignableFrom(valueType))
        //                {
        //                    var innerValue = ProcessJsonValue(value);

        //                    if (string.IsNullOrWhiteSpace(innerValue) == false)
        //                    {
        //                        processedValue.AppendLine(innerValue);
        //                    }
        //                }
        //                else
        //                {
        //                    var sanitisedValue = value.StripHtml().Trim();

        //                    if (string.IsNullOrWhiteSpace(sanitisedValue) == false)
        //                    {
        //                        processedValue.AppendLine(sanitisedValue);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    else if (objType == typeof(JArray))
        //    {
        //        var jArr = obj as JArray;
        //        if (jArr != null)
        //        {
        //            for (var i = 0; i < jArr.Count; i++)
        //            {
        //                var item = jArr[i];

        //                foreach (var field in _searchConfiguration.NestedIndexFields)
        //                {
        //                    var nestedField = item[field.Alias];

        //                    if (nestedField == null)
        //                    {
        //                        continue;
        //                    }

        //                    var valueType = nestedField.GetType();
        //                    var value = nestedField.Value<string>();

        //                    if (typeof(JContainer).IsAssignableFrom(valueType))
        //                    {
        //                        var innerValue = ProcessJsonValue(value);

        //                        if (string.IsNullOrWhiteSpace(innerValue) == false)
        //                        {
        //                            processedValue.AppendLine(innerValue);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        var sanitisedValue = value.StripHtml().Trim();

        //                        if (string.IsNullOrWhiteSpace(sanitisedValue) == false)
        //                        {
        //                            processedValue.AppendLine(sanitisedValue);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return processedValue.ToString().Trim();
        //}
    }
}
