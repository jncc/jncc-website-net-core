using Umbraco.Cms.Web.Common.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class CustomErrorPageController : RenderController
    {
        public CustomErrorPageController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
        { }

        public override IActionResult Index()
        {
            var statusCodePagesFeature = HttpContext.Features.Get<IStatusCodePagesFeature>();

            if (statusCodePagesFeature is not null)
            {
                statusCodePagesFeature.Enabled = false;
            }

            Response.StatusCode = 404;

            return CurrentTemplate(CurrentPage);
        }
    }
}