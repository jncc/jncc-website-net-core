using JNCC.PublicWebsite.Core.Interfaces.Services;
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
        private readonly IContentIndexService _contentIndexService;
        private readonly IContentRemoveIndexService _contentRemoveIndexService;

        private readonly ILogger<ContentCacheRefresherNotificationHandler> _logger;
        private readonly IContentService _contentService;

        private readonly AppCaches _appCaches;

        public ContentCacheRefresherNotificationHandler(
            IContentIndexService contentIndexService,
            IContentRemoveIndexService contentRemoveIndexService,
            ILogger<ContentCacheRefresherNotificationHandler> logger,
            IContentService contentService,
            AppCaches appCaches)
        {
            _contentIndexService = contentIndexService;
            _contentRemoveIndexService = contentRemoveIndexService;
            _logger = logger;
            _contentService = contentService;
            _appCaches = appCaches;
        }

        public void Handle(ContentCacheRefresherNotification notification)
        {
            // The runtime cache must be cleared for every notification, whatever the message
            // type and even if indexing throws. The cached content listings have no expiry, so
            // anything that skips the clear leaves them stale until the application restarts.
            try
            {
                UpdateSearchIndex(notification);
            }
            finally
            {
                _logger.LogInformation($"Content cache refresher notification handled. Clearing runtime cache...");
                _appCaches.RuntimeCache.Clear();
                _logger.LogInformation($"Runtime cache cleared");
            }
        }

        private void UpdateSearchIndex(ContentCacheRefresherNotification notification)
        {
            // We only want to handle the RefreshByPayload message type.
            if (notification.MessageType != MessageType.RefreshByPayload)
            {
                _logger.LogInformation($"Unhandled message type: {notification.MessageType.ToString()}");
                return;
            }

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
                    continue;
                }

                if (!contentItem.Edited && contentItem.Published && contentItem.PublishedState == PublishedState.Published && !contentItem.Trashed)
                {
                    _logger.LogInformation($"Published (update index)");
                    _contentIndexService.Handle(contentItem);
                    continue;
                }
                if (
                    (contentItem.PublishedState == PublishedState.Unpublished && !contentItem.Trashed) ||
                    contentItem.Trashed
                )
                {
                    _logger.LogInformation($"Unpublished (Try remove from index)");
                    _contentRemoveIndexService.Handle(contentItem);
                    continue;
                }
                _logger.LogInformation($"No index change needed");
            }
        }
    }
}
