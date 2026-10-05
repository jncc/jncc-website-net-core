using Umbraco.Cms.Web.Common.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Web;
using Microsoft.AspNetCore.Mvc;
using JNCC.PublicWebsite.Core.Models;

namespace JNCC.PublicWebsite.Core.Controllers.RenderMvcControllers
{
    public sealed class RedirectorController : RenderController
    {
        public RedirectorController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
        { }

        public override IActionResult Index()
        {
            var redirector = CurrentPage as Redirector;

            if(redirector != null && redirector?.Redirect != null)
            {
                return RedirectPermanent(redirector?.Redirect?.Url);
            }
            else
            {
                return null;
            }
        }
    }
}