using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "Resources")]
    public class ResourcesViewComponent : ViewComponent
    {
        private readonly IHomePageService _homePageService;

        public ResourcesViewComponent(IHomePageService homePageService)
        {
            _homePageService = homePageService ?? throw new ArgumentNullException(nameof(homePageService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var viewModel = _homePageService.GetResources(model as HomePage);

            return View("~/Views/Partials/Home/Resources.cshtml", viewModel);
        }
    }
}