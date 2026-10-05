using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    public class MediaRemoveIndexService : IMediaRemoveIndexService
    {
        private readonly ISearchIndexingQueueService _searchIndexingQueueService;
        private readonly IOptions<AmazonServiceConfigurationOptions> _amazonServiceConfigurationOptions;
        private readonly ILogger<MediaRemoveIndexService> _logger;
        private const string _site = SearchIndexingSites.Website;


        public MediaRemoveIndexService(
            ISearchIndexingQueueService searchIndexingQueueService,
            IOptions<AmazonServiceConfigurationOptions> amazonServiceConfigurationOptions,
            ILogger<MediaRemoveIndexService> logger)
        {
            _searchIndexingQueueService = searchIndexingQueueService;
            _amazonServiceConfigurationOptions = amazonServiceConfigurationOptions;
            _logger = logger;
        }

        public void Handle(IMedia publishedEntity, string fileName)
        {
            if (!_amazonServiceConfigurationOptions.Value.EnableIndexing)
            {
                _logger.LogWarning("Amazon indexing disabled");
                return;
            }

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                _logger.LogWarning("Unable to index: No file name provided");
                return;
            }

            _logger.LogWarning("Handling delete to Amazon");

            var document = new SearchIndexDocumentModel()
            {
                NodeId = publishedEntity.Id,
                Site = _site,
                Published = DateTime.Parse(publishedEntity.DeleteDate?.ToString() ?? publishedEntity.CreateDate.ToString()), //this date isn't actaully necessary to remove the item
                Title = publishedEntity.Name
            };


            if (fileName.ToLower().EndsWith("pdf"))
            {
                _searchIndexingQueueService.QueueDelete(document);
                return;
            }

            _logger.LogInformation("No update to the amazon index as file type is not pdf");
        }
    }
}
