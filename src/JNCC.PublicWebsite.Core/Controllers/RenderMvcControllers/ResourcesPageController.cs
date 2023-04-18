using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Interfaces.Api;
using JNCC.PublicWebsite.Core.Models.Custom;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public class ResourcesPageController: RenderController
    {
        private readonly IVariationContextAccessor _variationContextAccessor;
        private readonly ServiceContext _serviceContext;

        public ResourcesPageController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor, IVariationContextAccessor variationContextAccessor, ServiceContext context)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _variationContextAccessor = variationContextAccessor;
            _serviceContext = context;
        }

        public override IActionResult Index()
        {
            var resourceId = HttpContext.Items[KeyNames.ResourceIdContextKey];

            //check if we're rendering a resource, and if so, get the details of the resource
            if (HttpContext.Items[KeyNames.ResourceIdContextKey] != null)
            {
                var resourceItem = HttpContext.Items[KeyNames.ResourceIdContextKey] as ResourceModel;

                if (resourceItem != null)
                {
                    var model = new VirtualResourceModel(CurrentPage, new PublishedValueFallback(_serviceContext, _variationContextAccessor));

                    model.ResourceToDisplay = resourceItem;

                    return View("ResourceDetailPage", model);
                }
            }

            return CurrentTemplate(CurrentPage);
        }
    }
}
