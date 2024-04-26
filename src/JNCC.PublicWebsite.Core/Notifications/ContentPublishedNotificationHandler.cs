using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Options;
using JNCC.PublicWebsite.Core.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using JNCC.PublicWebsite.Core.Utilities;
using Newtonsoft.Json.Linq;

namespace JNCC.PublicWebsite.Core.Notifications
{
    public class ContentPublishedNotificationHandler : INotificationHandler<ContentPublishedNotification>
    {
        private readonly ISearchIndexingQueueService _searchIndexingQueueService;
        private readonly IOptions<AmazonServiceConfigurationOptions> _amazonServiceConfigurationOptions;
        private readonly ILogger<ContentPublishedNotificationHandler> _logger;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private const string _site = SearchIndexingSites.Website;


        public ContentPublishedNotificationHandler(ISearchIndexingQueueService searchIndexingQueueService, IOptions<AmazonServiceConfigurationOptions> amazonServiceConfigurationOptions, ILogger<ContentPublishedNotificationHandler> logger, IUmbracoContextAccessor umbracoContextAccessor)
        {
            _searchIndexingQueueService = searchIndexingQueueService;
            _amazonServiceConfigurationOptions = amazonServiceConfigurationOptions;
            _logger = logger;
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public void Handle(ContentPublishedNotification notification)
        {
            if (!_amazonServiceConfigurationOptions.Value.EnableIndexing)
            {
                _logger.LogWarning("Amazon indexing disabled");
            }
            else
            {
                _logger.LogWarning("Handling send to Amazon");

                _umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext);

                string leftPartUrl = umbracoContext.OriginalRequestUrl.GetLeftPart(UriPartial.Authority);

                foreach (var entity in notification.PublishedEntities)
                {
                    var contentBuilder = new StringBuilder();

                    var fieldsToIndex = entity.Properties.Where(p => _amazonServiceConfigurationOptions.Value.IndexFields.Contains(p.Alias));

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

                                    _logger.LogInformation("{{alias} - {value}", contentField.Alias, contentFieldValueString);

                                    //Check if it has a value and append it
                                    if (string.IsNullOrEmpty(contentFieldValueString) == false)
                                    {
                                        if (contentFieldValueString.DetectIsJson() && JsonUtility.TryParseJson(contentFieldValueString, out object parsedJson))
                                        {
                                            var processedJsonValue = ProcessJsonValue(parsedJson);

                                            if (string.IsNullOrEmpty(processedJsonValue) == false)
                                            {
                                                contentBuilder.AppendLine(processedJsonValue);
                                            }
                                        }
                                        else
                                        {
                                            var sanitisedValue = contentFieldValueString.StripHtml().Trim();

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

                    var document = new SearchIndexDocumentModel()
                    {
                        NodeId = entity.Id,
                        Site = _site,
                        Published = DateTime.Parse(entity.PublishDate.ToString()),
                        Title = entity.Name,
                        Url = leftPartUrl + umbracoContext.Content.GetById(entity.Id).Url(),
                        Content = content
                    };


                    _searchIndexingQueueService.QueueUpsert(document);
                }
            }

          
        }

        private string ProcessJsonValueSubSection(string value)
        {
            if (value.DetectIsJson() && JsonUtility.TryParseJson(value, out object parsedJson))
            {
               return ProcessJsonValue(parsedJson);
            }

            return string.Empty;
        }


        private string ProcessJsonValue(object obj)
        {
            var processedValue = new StringBuilder();
            var objType = obj.GetType();

            if (objType == typeof(JObject))
            {
                var jObj = obj as JObject;
                if (jObj != null)
                {
                    foreach (var field in _amazonServiceConfigurationOptions.Value.NestedIndexFields.Split(','))
                    {
                        var nestedField = jObj[field];
                        if (nestedField == null)
                        {
                            continue;
                        }

                        var valueType = nestedField.GetType();

                        if (valueType == typeof(JArray))
                        {
                            var jArr = nestedField as JArray;
                            if (jArr != null)
                            {
                                for (var i = 0; i < jArr.Count; i++)
                                {
                                    var item = jArr[i];

                                    foreach (var jField in _amazonServiceConfigurationOptions.Value.NestedIndexFields.Split(','))
                                    {
                                        var nestedJField = item[jField];

                                        if (nestedJField == null)
                                        {
                                            continue;
                                        }

                                        var valueJType = nestedField.GetType();
                                        var valueJ = nestedJField.Value<string>();

                                        if (jField == "contentData" || jField == "subSections")
                                        {
                                            var innerValue = ProcessJsonValueSubSection(valueJ);

                                            if (string.IsNullOrWhiteSpace(innerValue) == false)
                                            {
                                                processedValue.AppendLine(innerValue);
                                            }
                                        }
                                        else if(jField == "content")
                                        {
                                            var sanitisedValue = valueJ.StripHtml().Trim();

                                            if (string.IsNullOrWhiteSpace(sanitisedValue) == false)
                                            {
                                                processedValue.AppendLine(sanitisedValue);
                                            }
                                        }
                                        else
                                        {
                                            if (string.IsNullOrWhiteSpace(valueJ) == false)
                                            {
                                                processedValue.AppendLine(valueJ);
                                            }
                                        }
                                    }

                                }
                            }
                        }
                        else
                        {
                            var value = nestedField.Value<string>();

                            if (typeof(JContainer).IsAssignableFrom(valueType))
                            {
                                var innerValue = ProcessJsonValue(value);

                                if (string.IsNullOrWhiteSpace(innerValue) == false)
                                {
                                    processedValue.AppendLine(innerValue);
                                }
                            }
                            else
                            {
                                var sanitisedValue = value.StripHtml().Trim();

                                if (string.IsNullOrWhiteSpace(sanitisedValue) == false)
                                {
                                    processedValue.AppendLine(sanitisedValue);
                                }
                            }
                        }
                    }
                }
            }
            else if (objType == typeof(JArray))
            {
                var jArr = obj as JArray;
                if (jArr != null)
                {
                    for (var i = 0; i < jArr.Count; i++)
                    {
                        var item = jArr[i];

                        foreach (var field in _amazonServiceConfigurationOptions.Value.NestedIndexFields.Split(','))
                        {
                            var nestedField = item[field];

                            if (nestedField == null)
                            {
                                continue;
                            }

                            var valueType = nestedField.GetType();
                            var value = nestedField.Value<string>();

                            if (field == "contentData" || field == "subSections")
                            {
                                var innerValue = ProcessJsonValueSubSection(value);

                                if (string.IsNullOrWhiteSpace(innerValue) == false)
                                {
                                    processedValue.AppendLine(innerValue);
                                }
                            }
                            else if (field == "content")
                            {
                                var sanitisedValue = value.StripHtml().Trim();

                                if (string.IsNullOrWhiteSpace(sanitisedValue) == false)
                                {
                                    processedValue.AppendLine(sanitisedValue);
                                }
                            }
                            else
                            {
                                if (string.IsNullOrWhiteSpace(value) == false)
                                {
                                    processedValue.AppendLine(value);
                                }
                            }
                        }
                    }
                }
            }

            return processedValue.ToString().Trim();
        }
    }
}
