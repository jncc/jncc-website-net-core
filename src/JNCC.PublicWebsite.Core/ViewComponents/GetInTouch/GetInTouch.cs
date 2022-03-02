using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "GetInTouch")]
    public class GetInTouchViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            if (model is IGetInTouchComposition == false){ return null; }

            return View("~/Views/Partials/GetInTouch.cshtml", model);
        }
    }
}