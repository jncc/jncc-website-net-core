using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Options;
using JNCC.PublicWebsite.Core.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Hosting;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.PublishedCache;
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
        private readonly IUmbracoContextFactory _umbracoContextFactory;
        private readonly IHostingEnvironment _hostingEnvironment;
        private const string _site = SearchIndexingSites.Website;


        public MediaSavedNotificationHandler(ISearchIndexingQueueService searchIndexingQueueService, IOptions<AmazonServiceConfigurationOptions> amazonServiceConfigurationOptions, ILogger<MediaSavedNotificationHandler> logger, IUmbracoContextAccessor umbracoContextAccessor, IUmbracoContextFactory umbracoContextFactory, IHostingEnvironment hostingEnvironment)
        {
            _searchIndexingQueueService = searchIndexingQueueService;
            _amazonServiceConfigurationOptions = amazonServiceConfigurationOptions;
            _logger = logger;
            _umbracoContextAccessor = umbracoContextAccessor;
            _umbracoContextFactory = umbracoContextFactory;
             _hostingEnvironment = hostingEnvironment;
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


            using (UmbracoContextReference umbracoContextReference = _umbracoContextFactory.EnsureUmbracoContext())
            {
                IPublishedMediaCache contentCache = umbracoContextReference.UmbracoContext.Media;

                foreach (var entity in notification.SavedEntities)
                {
                    var mediaNode = contentCache.GetById(entity.Id);

                    if (mediaNode != null)
                    {
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

                            var fileInfo = new FileInfo(mediaNode.Url());
                            // index the node

                            var fullPath = _hostingEnvironment.MapPathWebRoot(mediaNode.Url());

                            var pdf = System.IO.File.ReadAllBytes(fullPath);
                            var pdfEncoded = Convert.ToBase64String(pdf);

                            document.Url = mediaFullUrl;
                            document.Content = "Umbraco Media File";
                            document.FileBase64Encoded = pdfEncoded;
                            document.FileExtension = fileInfo.Extension;
                            document.FileSizeInBytes = pdf.Length;

                            _searchIndexingQueueService.QueueUpsert(document);
                        }
                    }
                    else
                    {
                        _logger.LogWarning("Failed to add media to Queue Upsert (ID: {0}, Name: {1}). Reason: Media is Null or WhiteSpace.", entity.Id, entity.Name);
                    }
                }
            }



        }
    }
}
