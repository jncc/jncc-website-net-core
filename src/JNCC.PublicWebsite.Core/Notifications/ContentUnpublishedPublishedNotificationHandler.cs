using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Options;
using JNCC.PublicWebsite.Core.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace JNCC.PublicWebsite.Core.Notifications
{
    public class ContentUnpublishedPublishedNotificationHandler : INotificationHandler<ContentUnpublishedNotification>
    {
        private readonly ISearchIndexingQueueService _searchIndexingQueueService;

        private readonly IOptions<AmazonServiceConfigurationOptions> _amazonServiceConfigurationOptions;

        private readonly ILogger<ContentPublishedNotificationHandler> _logger;

        private const string _site = SearchIndexingSites.Website;


        public ContentUnpublishedPublishedNotificationHandler(ISearchIndexingQueueService searchIndexingQueueService, IOptions<AmazonServiceConfigurationOptions> amazonServiceConfigurationOptions, ILogger<ContentPublishedNotificationHandler> logger)
        {
            _searchIndexingQueueService = searchIndexingQueueService;
            _amazonServiceConfigurationOptions = amazonServiceConfigurationOptions;
            _logger = logger;
        }

        public void Handle(ContentUnpublishedNotification notification)
        {
            if (!_amazonServiceConfigurationOptions.Value.EnableIndexing)
            {
                _logger.LogWarning("Amazon indexing disabled");
            }

            _logger.LogWarning("Handling delete to Amazon");

            foreach (var entity in notification.UnpublishedEntities)
            {
                var document = new SearchIndexDocumentModel()
                {
                    NodeId = entity.Id,
                    Site = _site,
                    Published = DateTime.Parse(entity.PublishDate.ToString()),
                    Title = entity.Name                    
                };

                _searchIndexingQueueService.QueueDelete(document);
            }
        }  
    }
}
