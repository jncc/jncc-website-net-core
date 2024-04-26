using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "LatestNewsAndSocialFeed")]
    public class LatestNewsAndSocialFeedViewComponent : ViewComponent
    {
        private readonly ILatestNewsSectionService _latestNewsSectionService;
        public LatestNewsAndSocialFeedViewComponent(ILatestNewsSectionService latestNewsSectionService)
        {
            _latestNewsSectionService = latestNewsSectionService ?? throw new ArgumentNullException(nameof(latestNewsSectionService));
        }

        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var homePage = model.Root<HomePage>();
            var viewModel = _latestNewsSectionService.GetViewModel(homePage);

            return View("~/Views/Partials/LatestNewsAndSocialFeed.cshtml", viewModel);
        }
    }
}