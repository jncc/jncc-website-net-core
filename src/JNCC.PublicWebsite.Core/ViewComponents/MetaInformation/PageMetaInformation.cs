using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "PageMetaInformation")]
    public class PageMetaInformationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPublishedContent model)
        {

            var viewModel = (model as IPageMetaInformationComposition)?.GetPublishedDateOrDefault();

            return View("~/Views/Partials/PageMetaInformation.cshtml", viewModel);
        }
    }
}