using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Interfaces.Api;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;

namespace JNCC.PublicWebsite.Core.ContentFinders
{
    public class ResourceContentFinder : IContentFinder
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IResourceApi _resourceApi;
        private readonly IDocumentUrlService _documentUrlService;

        public ResourceContentFinder(IUmbracoContextAccessor umbracoContextAccessor, IHttpContextAccessor httpContext, IResourceApi resourceApi, IDocumentUrlService documentUrlService)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
            _httpContext = httpContext;
            _resourceApi = resourceApi;
            _documentUrlService = documentUrlService;
        }

        Task<bool> IContentFinder.TryFindContent(IPublishedRequestBuilder request)
        {
            var path = request.AbsolutePathDecoded;

            if (!_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext))
            {
                return Task.FromResult(false);
            }

            if (path.Contains('/'))
            {
                var pathSegments = path.Split("/");

                var resourceId = pathSegments.Last();

                var resourcePath = string.Join("/", pathSegments.Take(pathSegments.Length - 1));

                if (!string.IsNullOrEmpty(resourcePath))
                {
                    Guid? documentKey = _documentUrlService.GetDocumentKeyByRoute(
                        resourcePath,
                        request.Culture,
                        null,
                        umbracoContext.InPreviewMode);

                    IPublishedContent? content = null;

                    if (documentKey.HasValue)
                    {
                        content = umbracoContext.Content.GetById(umbracoContext.InPreviewMode, documentKey.Value);
                    }

                    //get the content one level up and see if it's the resources page
                    if (content != null && content.ContentType.Alias == ResourcesPage.ModelTypeAlias)
                    {
                        //now check the resource exists, if not we should 404
                        var resourceItem = _resourceApi.GetResource(resourceId).GetAwaiter().GetResult();

                        if (resourceItem != null)
                        {
                            _httpContext.HttpContext.Items[KeyNames.ResourceIdContextKey] = resourceItem;

                            request.SetPublishedContent(content);

                            return Task.FromResult(true);
                        }
                    }
                }
            }

            return Task.FromResult(false);
        }
    }
}
