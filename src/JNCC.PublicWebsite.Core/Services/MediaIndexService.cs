using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Options;
using JNCC.PublicWebsite.Core.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Umbraco.Cms.Core.Hosting;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Services
{
    internal class ImageSourceJsonModel
    {
        [JsonProperty(PropertyName = "src")]
        internal string Src { get; set; } = string.Empty;
    }

    public class MediaIndexService : IMediaIndexService
    {
        private readonly ISearchIndexingQueueService _searchIndexingQueueService;
        private readonly IOptions<AmazonServiceConfigurationOptions> _amazonServiceConfigurationOptions;
        private readonly ILogger<MediaIndexService> _logger;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;
        private const string _site = SearchIndexingSites.Website;

        public MediaIndexService(
            ISearchIndexingQueueService searchIndexingQueueService,
            IOptions<AmazonServiceConfigurationOptions> amazonServiceConfigurationOptions,
            ILogger<MediaIndexService> logger,
            IUmbracoContextAccessor umbracoContextAccessor,
            IHostingEnvironment hostingEnvironment
        )
        {
            _searchIndexingQueueService = searchIndexingQueueService;
            _amazonServiceConfigurationOptions = amazonServiceConfigurationOptions;
            _logger = logger;
            _umbracoContextAccessor = umbracoContextAccessor;
            _hostingEnvironment = hostingEnvironment;
        }

        public void Handle(IMedia publishedEntity, string fileName)
        {
            try
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

                _logger.LogInformation("Sending updated content to Amazon for indexing");

                if (!_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext) || umbracoContext is null)
                {
                    _logger.LogError("Could not get Umbraco Context");
                }

                string leftPartUrl = umbracoContext!.OriginalRequestUrl.GetLeftPart(UriPartial.Authority);

                _logger.LogInformation($"Preparing to send index fields for {publishedEntity.Name}");

                if (fileName.ToLower().EndsWith("pdf"))
                {
                    var mediaFullUrl = $"{leftPartUrl}{fileName}";

                    var fileInfo = new FileInfo(fileName);
                    // index the node

                    var fullPath = _hostingEnvironment.MapPathWebRoot(fileName);

                    var pdf = System.IO.File.ReadAllBytes(fullPath);
                    var pdfEncoded = Convert.ToBase64String(pdf);

                    var document = new SearchIndexDocumentModel()
                    {
                        NodeId = publishedEntity.Id,
                        Site = _site,
                        Published = publishedEntity.UpdateDate,
                        Title = publishedEntity.Name ?? "",
                        Url = mediaFullUrl,
                        Content = "Umbraco Media File",
                        FileBase64Encoded = pdfEncoded,
                        FileExtension = fileInfo.Extension,
                        FileSizeInBytes = pdf.Length
                    };

                    _logger.LogInformation("Beginning upsert");
                    _searchIndexingQueueService.QueueUpsert(document);
                    _logger.LogInformation("Upsert complete");
                }
                else
                {
                    _logger.LogInformation("No update to the amazon index as file type is not pdf");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in MediaIndexService Handle function");
            }
        }
    }
}
