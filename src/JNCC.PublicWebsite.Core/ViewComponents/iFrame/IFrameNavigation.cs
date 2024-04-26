using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "IFrameNavigation")]
    public class IFrameNavigationViewComponent : ViewComponent
    {
        private readonly IIFramePageService _iFramePageService;

        public IFrameNavigationViewComponent(IIFramePageService iFramePageService)
        {
            _iFramePageService = iFramePageService ?? throw new ArgumentNullException(nameof(iFramePageService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var viewModel = _iFramePageService.GetNavigation(model as IFramePage);

            return View("~/Views/Partials/Header/MainNavigation.cshtml", viewModel);
        }
    }
}