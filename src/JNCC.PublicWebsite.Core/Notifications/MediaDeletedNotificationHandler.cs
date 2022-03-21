using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Options;
using JNCC.PublicWebsite.Core.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Notifications
{
    public class MediaDeletedNotificationHandler : INotificationHandler<MediaDeletedNotification>
    {
        private readonly ILogger<MediaSavedNotificationHandler> _logger;

        private readonly ISearchIndexingQueueService _searchIndexingQueueService;

        private readonly IOptions<AmazonServiceConfigurationOptions> _amazonServiceConfigurationOptions;

        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        private const string _site = SearchIndexingSites.Website;


        public MediaDeletedNotificationHandler(ISearchIndexingQueueService searchIndexingQueueService, IOptions<AmazonServiceConfigurationOptions> amazonServiceConfigurationOptions, ILogger<MediaSavedNotificationHandler> logger, IUmbracoContextAccessor umbracoContextAccessor)
        {
            _searchIndexingQueueService = searchIndexingQueueService;
            _amazonServiceConfigurationOptions = amazonServiceConfigurationOptions;
            _logger = logger;
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public void Handle(MediaDeletedNotification notification)
        {
            if (!_amazonServiceConfigurationOptions.Value.EnableIndexing)
            {
                _logger.LogWarning("Amazon indexing disabled");
            }

            _logger.LogWarning("Media saved notification");

            _umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext);

            foreach (var entity in notification.DeletedEntities)
            {
               var mediaNode = umbracoContext.Content.GetById(entity.Id);

                var file = mediaNode.Value<string>("umbracoFile");

                if (!file.IsNullOrWhiteSpace() && file.ToLower().Contains("pdf"))
                {
                    var document = new SearchIndexDocumentModel()
                    {
                        NodeId = mediaNode.Id,
                        Site = _site,
                        Published = DateTime.Parse(mediaNode.UpdateDate.ToString()),
                        Title = mediaNode.Name
                    };                   

                    _searchIndexingQueueService.QueueDelete(document);
                }
                
            }
        }
    }
}
