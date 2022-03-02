using System;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "ScienceLandingLatestUpdates")]
    public class ScienceLandingLatestUpdatesViewComponent : ViewComponent
    {
        private readonly IScienceLandingPageService _scienceLandingPageService;
        public ScienceLandingLatestUpdatesViewComponent(IScienceLandingPageService scienceLandingPageService)
        {
            _scienceLandingPageService = scienceLandingPageService ?? throw new ArgumentNullException(nameof(scienceLandingPageService));
        }

        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var viewModel = _scienceLandingPageService.GetLatestUpdates(model as ScienceLandingPage);

            return View("~/Views/Partials/ScienceLanding/LatestUpdates.cshtml", viewModel);
        }
    }
}