using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    public class ContentRemoveIndexService : IContentRemoveIndexService
    {
        private readonly ISearchIndexingQueueService _searchIndexingQueueService;
        private readonly IOptions<AmazonServiceConfigurationOptions> _amazonServiceConfigurationOptions;
        private readonly ILogger<ContentRemoveIndexService> _logger;
        private const string _site = SearchIndexingSites.Website;


        public ContentRemoveIndexService(
            ISearchIndexingQueueService searchIndexingQueueService,
            IOptions<AmazonServiceConfigurationOptions> amazonServiceConfigurationOptions,
            ILogger<ContentRemoveIndexService> logger)
        {
            _searchIndexingQueueService = searchIndexingQueueService;
            _amazonServiceConfigurationOptions = amazonServiceConfigurationOptions;
            _logger = logger;
        }

        public void Handle(IContent publishedEntity)
        {
            if (!_amazonServiceConfigurationOptions.Value.EnableIndexing)
            {
                _logger.LogWarning("Amazon indexing disabled");
            }
            _logger.LogWarning("Handling delete to Amazon");

            var document = new SearchIndexDocumentModel()
            {
                NodeId = publishedEntity.Id,
                Site = _site,
                Published = DateTime.Parse(publishedEntity.DeleteDate?.ToString() ?? publishedEntity.CreateDate.ToString()), //this date isn't actaully necessary to remove the item
                Title = publishedEntity.Name
            };

            _searchIndexingQueueService.QueueDelete(document);
        }
    }
}
