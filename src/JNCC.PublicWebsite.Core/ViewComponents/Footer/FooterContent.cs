using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "FooterContent")]
    public class FooterContentViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var homePage = model.Root() as HomePage;
            
            return View("~/Views/Partials/Footer/Content.cshtml", homePage.FooterContent);
        }
    }
}