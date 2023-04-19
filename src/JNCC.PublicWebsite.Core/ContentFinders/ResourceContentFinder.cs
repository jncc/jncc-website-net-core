using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Interfaces.Api;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;

namespace JNCC.PublicWebsite.Core.ContentFinders
{
    public class ResourceContentFinder : IContentFinder
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IResourceApi _resourceApi;

        public ResourceContentFinder(IUmbracoContextAccessor umbracoContextAccessor, IHttpContextAccessor httpContext, IResourceApi resourceApi)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
            _httpContext = httpContext;
            _resourceApi = resourceApi;
        }

        bool IContentFinder.TryFindContent(IPublishedRequestBuilder request)
        {
            var path = request.AbsolutePathDecoded;

            if (!_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext))
            {
                return false;
            }

            if (path.Contains("/"))
            {
                var pathSegments = path.Split("/");

                var resourceId = pathSegments.Last();

                var resourcePath = string.Join("/", pathSegments.Take(pathSegments.Length - 1));

                if (!string.IsNullOrEmpty(resourcePath))
                {
                    //get the content one level up and see if it's the resources page
                    var content = umbracoContext.Content.GetByRoute(resourcePath);

                    if (content != null && content.ContentType.Alias == ResourcesPage.ModelTypeAlias)
                    {
                        //now check the resource exists, if not we should 404
                        var resourceItem = _resourceApi.GetResource(resourceId).GetAwaiter().GetResult();

                        if (resourceItem != null)
                        {
                            _httpContext.HttpContext.Items[KeyNames.ResourceIdContextKey] = resourceItem;

                            request.SetPublishedContent(content);

                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
