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
    public class MediaSavedNotificationHandler : INotificationHandler<MediaSavedNotification>
    {
        private readonly ILogger<MediaSavedNotificationHandler> _logger;

        private readonly ISearchIndexingQueueService _searchIndexingQueueService;

        private readonly IOptions<AmazonServiceConfigurationOptions> _amazonServiceConfigurationOptions;

        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        private const string _site = SearchIndexingSites.Website;


        public MediaSavedNotificationHandler(ISearchIndexingQueueService searchIndexingQueueService, IOptions<AmazonServiceConfigurationOptions> amazonServiceConfigurationOptions, ILogger<MediaSavedNotificationHandler> logger, IUmbracoContextAccessor umbracoContextAccessor)
        {
            _searchIndexingQueueService = searchIndexingQueueService;
            _amazonServiceConfigurationOptions = amazonServiceConfigurationOptions;
            _logger = logger;
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public void Handle(MediaSavedNotification notification)
        {
            if (!_amazonServiceConfigurationOptions.Value.EnableIndexing)
            {
                _logger.LogWarning("Amazon indexing disabled");
            }

            _logger.LogWarning("Media saved notification");

            _umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext);

            string leftPartUrl = umbracoContext.OriginalRequestUrl.GetLeftPart(UriPartial.Authority);


            foreach (var entity in notification.SavedEntities)
            {
               var mediaNode = umbracoContext.Content.GetById(entity.Id);

                var file = mediaNode.Value<string>("umbracoFile");

                var mediaFullUrl = leftPartUrl + mediaNode.Url();

                if (!file.IsNullOrWhiteSpace() && file.ToLower().Contains("pdf"))
                {
                    var document = new SearchIndexDocumentModel()
                    {
                        NodeId = mediaNode.Id,
                        Site = _site,
                        Published = DateTime.Parse(mediaNode.UpdateDate.ToString()),
                        Title = mediaNode.Name
                    };

                    var fileInfo = new FileInfo(mediaFullUrl);                    
                    // index the node

                    var pdf = System.IO.File.ReadAllBytes(mediaFullUrl);
                    var pdfEncoded = Convert.ToBase64String(pdf);

                    document.Url = mediaFullUrl;
                    document.Content = "Umbraco Media File";
                    document.FileBase64Encoded = pdfEncoded;
                    document.FileExtension = fileInfo.Extension;
                    document.FileSizeInBytes = fileInfo.Length;

                    _searchIndexingQueueService.QueueUpsert(document);
                }
                
            }
        }
    }
}
