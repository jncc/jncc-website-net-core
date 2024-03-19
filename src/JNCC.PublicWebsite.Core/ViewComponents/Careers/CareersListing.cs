using System;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "CareersListing")]
    public class CareersListingViewComponent : ViewComponent
    {
        private readonly ICareersLandingPageService _careersLandingPageService;

        public CareersListingViewComponent(ICareersLandingPageService careersLandingPageService)
        {
            _careersLandingPageService = careersLandingPageService ?? throw new ArgumentNullException(nameof(careersLandingPageService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var viewModel = _careersLandingPageService.GetCareers(model as CareersLandingPage);

            return View("~/Views/Partials/Careers/CareersListing.cshtml", viewModel);
        }
    }
}
