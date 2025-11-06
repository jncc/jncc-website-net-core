using DocumentFormat.OpenXml.Office2010.PowerPoint;
using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.Utilities;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Sync;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Notifications
{
    public class MediaCacheRefresherNotificationHandler : INotificationHandler<MediaCacheRefresherNotification>
    {
        private readonly IMediaIndexService _mediaIndexService;
        private readonly IMediaRemoveIndexService _mediaRemoveIndexService;

        private readonly ILogger<MediaCacheRefresherNotificationHandler> _logger;
        private readonly IMediaService _mediaService;

        private const string _site = SearchIndexingSites.Website;

        public MediaCacheRefresherNotificationHandler(
            IMediaIndexService mediaIndexService,
            IMediaRemoveIndexService mediaRemoveIndexService,
            ILogger<MediaCacheRefresherNotificationHandler> logger,
            IMediaService mediaService)
        {
            _mediaIndexService = mediaIndexService;
            _mediaRemoveIndexService = mediaRemoveIndexService;
            _logger = logger;
            _mediaService = mediaService;
        }

        public void Handle(MediaCacheRefresherNotification notification)
        {
            // We only want to handle the RefreshByPayload message type.
            if (notification.MessageType != MessageType.RefreshByPayload)
            {
                _logger.LogInformation($"Unhandled message type: {notification.MessageType.ToString()}");
                return;
            }

            if (notification.MessageObject is not MediaCacheRefresher.JsonPayload[])
            {
                return;
            }
            // Cast the payload to the expected type.
            var payload = (MediaCacheRefresher.JsonPayload[])notification.MessageObject;

            // Handle each media item in the payload.
            foreach (MediaCacheRefresher.JsonPayload item in payload)
            {
                _logger.LogInformation($"Message type: {item.ChangeTypes.ToString()}");
                // Retrieve the media item.
                var mediaItemId = item.Id;
                IMedia? mediaItem = _mediaService.GetById(mediaItemId);
                if (mediaItem is null)
                {
                    _logger.LogWarning(
                        "MediaCacheRefresherNotification handled for type {MessageType} but media item with Id {Id} could not be found.",
                        notification.MessageType,
                        mediaItemId);
                    return;
                }

                var fileName = mediaItem.GetValue<string>("umbracoFile");
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    if (
                        fileName.DetectIsJson() &&
                        fileName.TryParseJson(out ImageSourceJsonModel isjm)
                    )
                    {
                        fileName = isjm.Src;
                    }
                }

                if (!mediaItem.Trashed)
                {
                    _logger.LogInformation($"Published (update index)");
                    _mediaIndexService.Handle(mediaItem, fileName);
                    return;
                }
                if (mediaItem.Trashed)
                {
                    _logger.LogInformation($"Unpublished (Try remove from index)");
                    _mediaRemoveIndexService.Handle(mediaItem, fileName);
                    return;
                }
                _logger.LogInformation($"No index change needed");
            }
        }
    }
}
