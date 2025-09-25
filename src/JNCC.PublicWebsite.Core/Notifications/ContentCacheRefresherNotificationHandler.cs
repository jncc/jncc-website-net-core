using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Sync;

namespace JNCC.PublicWebsite.Core.Notifications
{
    public class ContentCacheRefresherNotificationHandler : INotificationHandler<ContentCacheRefresherNotification>
    {
        private readonly ILogger<ContentCacheRefresherNotificationHandler> _logger;
        private readonly IContentService _contentService;

        public ContentCacheRefresherNotificationHandler(
            ILogger<ContentCacheRefresherNotificationHandler> logger,
            IContentService contentService)
        {
            _logger = logger;
            _contentService = contentService;
        }

        public void Handle(ContentCacheRefresherNotification notification)
        {
            // We only want to handle the RefreshByPayload message type.
            if (notification.MessageType != MessageType.RefreshByPayload)
            {
                _logger.LogInformation($"Unhandled message type: {notification.MessageType.ToString()}");
                return;
            }

            //(notification.MessageType == MessageType.RefreshByPayload)
            //{
            //    Create
            //    Save
            //    Save&Publish
            //}

            if (notification.MessageObject is not ContentCacheRefresher.JsonPayload[])
            {
                return;
            }
            // Cast the payload to the expected type.
            var payload = (ContentCacheRefresher.JsonPayload[])notification.MessageObject;

            // Handle each content item in the payload.
            foreach (ContentCacheRefresher.JsonPayload item in payload)
            {
                _logger.LogInformation($"Message type: {item.ChangeTypes.ToString()}");
                // Retrieve the content item.
                var contentItemId = item.Id;
                IContent? contentItem = _contentService.GetById(contentItemId);
                if (contentItem is null)
                {
                    _logger.LogWarning(
                        "ContentCacheRefresherNotification handled for type {MessageType} but content item with Id {Id} could not be found.",
                        notification.MessageType,
                        contentItemId);
                    return;
                }

                // Do something with the content item. Here we'll just log some details.
                _logger.LogInformation(
                    "ContentCacheRefresherNotification handled for type {MessageType} and id {Id}. " +
                    "Key: {Key}, Name: {Name}",
                    notification.MessageType,
                    contentItemId,
                    contentItem.Key,
                    contentItem.Name);
            }
        }
    }
}
