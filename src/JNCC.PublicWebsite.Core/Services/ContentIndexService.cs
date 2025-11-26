using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Notifications;
using JNCC.PublicWebsite.Core.Options;
using JNCC.PublicWebsite.Core.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Services
{
    internal class UmbracoBlocklistJsonModel
    {
        [JsonProperty(PropertyName = "contentUdi")]
        internal string ContentUdi { get; set; } = string.Empty;
    }

    internal class LayoutJsonModel
    {
        [JsonProperty(PropertyName = "Umbraco.BlockList")]
        internal List<UmbracoBlocklistJsonModel> UmbracoBlockList { get; set; } = new List<UmbracoBlocklistJsonModel>();

    }

    internal class BlocklistJsonModel
    {
        [JsonProperty(PropertyName = "layout")]
        internal LayoutJsonModel Layout { get; set; } = new LayoutJsonModel();

        [JsonProperty(PropertyName = "contentData")]
        internal JArray ContentData { get; set; } = new JArray();

        [JsonProperty(PropertyName = "settingsData")]
        internal List<object> SettingsData { get; set; } = new List<object>();
    }

    public class ContentIndexService : IContentIndexService
    {
        private readonly ISearchIndexingQueueService _searchIndexingQueueService;
        private readonly IOptions<AmazonServiceConfigurationOptions> _amazonServiceConfigurationOptions;
        private readonly ILogger<ContentIndexService> _logger;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private const string _site = SearchIndexingSites.Website;


        public ContentIndexService(
            ISearchIndexingQueueService searchIndexingQueueService,
            IOptions<AmazonServiceConfigurationOptions> amazonServiceConfigurationOptions,
            ILogger<ContentIndexService> logger,
            IUmbracoContextAccessor umbracoContextAccessor
        )
        {
            _searchIndexingQueueService = searchIndexingQueueService;
            _amazonServiceConfigurationOptions = amazonServiceConfigurationOptions;
            _logger = logger;
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public void Handle(IContent publishedEntity)
        {
            try
            {
                if (!_amazonServiceConfigurationOptions.Value.EnableIndexing)
                {
                    _logger.LogWarning("Amazon indexing disabled");
                    return;
                }

                _logger.LogInformation("Sending updated content to Amazon for indexing");

                if (!_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext) || umbracoContext is null)
                {
                    _logger.LogError("Could not get Umbraco Context");
                }

                string leftPartUrl = umbracoContext!.OriginalRequestUrl.GetLeftPart(UriPartial.Authority);

                _logger.LogInformation($"Preparing to send index fields for {publishedEntity.Name}");
                var contentBuilder = new StringBuilder();

                var fieldsToIndex = publishedEntity.Properties.Where(p => _amazonServiceConfigurationOptions.Value.IndexFields.Contains(p.Alias));

                foreach (var contentField in fieldsToIndex)
                {
                    var contentFieldValues = contentField.Values;

                    if (contentFieldValues != null && contentFieldValues.Any())
                    {
                        foreach (var contentFieldValue in contentFieldValues)
                        {
                            if (contentFieldValue != null && contentFieldValue.PublishedValue != null)
                            {
                                var contentFieldValueString = contentFieldValue.PublishedValue.ToString();

                                _logger.LogInformation($"Alias: {contentField.Alias}; Value: {contentFieldValueString}");

                                //Check if it has a value and append it
                                if (string.IsNullOrEmpty(contentFieldValueString) == false)
                                {
                                    if (contentFieldValueString.DetectIsJson() && contentFieldValueString.TryParseJson(out BlocklistJsonModel bljm))
                                    {
                                        _logger.LogInformation("Value is JSON");
                                        var processedJsonValue = ProcessJsonValue(bljm);
                                        _logger.LogInformation($"Parsed value is: {processedJsonValue}");

                                        if (string.IsNullOrEmpty(processedJsonValue) == false)
                                        {
                                            contentBuilder.AppendLine(processedJsonValue);
                                        }
                                    }
                                    else
                                    {
                                        _logger.LogInformation("Value is not JSON");
                                        var sanitisedValue = contentFieldValueString.StripHtml().Trim();
                                        _logger.LogInformation($"Sanitised value is: {sanitisedValue}");

                                        if (string.IsNullOrWhiteSpace(sanitisedValue) == false)
                                        {
                                            contentBuilder.AppendLine(sanitisedValue);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                string content = contentBuilder.ToString().Trim();
                _logger.LogInformation($"Content to index: {content}");

                var document = new SearchIndexDocumentModel()
                {
                    NodeId = publishedEntity.Id,
                    Site = _site,
                    Published = publishedEntity.PublishDate ?? DateTime.Now,
                    Title = publishedEntity.Name ?? "",
                    Url = leftPartUrl + umbracoContext!.Content!.GetById(publishedEntity.Id)!.Url(),
                    Content = content
                };

                _logger.LogInformation("Beginning upsert");
                _searchIndexingQueueService.QueueUpsert(document);
                _logger.LogInformation("Upsert complete");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in ContentIndexService Handle function");
            }
        }

        private static List<string> nestedIndexFields = new List<string> { "headline", "title", "description", "content" };

        private string ProcessJsonValue(BlocklistJsonModel bljm)
        {
            var processedValue = new StringBuilder();
            var objType = bljm.ContentData.GetType();

            foreach (var contentItem in bljm.ContentData)
            {
                if (contentItem.GetType() == typeof(JObject))
                {
                    var ci = contentItem as JObject;
                    foreach (var field in nestedIndexFields)
                    {
                        var contentItemField = ci![field];
                        if (
                            contentItemField is not null &&
                            contentItemField is JValue &&
                            (contentItemField as JValue)!.Value is string &&
                            !string.IsNullOrWhiteSpace((contentItemField as JValue)!.Value as string)
                        )
                        {
                            var sanitisedValue = ((contentItemField as JValue)!.Value! as string)!.StripHtml().Trim();

                            if (string.IsNullOrWhiteSpace(sanitisedValue) == false)
                            {
                                processedValue.AppendLine(sanitisedValue);
                            }
                        }
                    }
                }
            }

            return processedValue.ToString().Trim();
        }
    }
}
