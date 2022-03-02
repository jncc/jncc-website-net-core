using System;
using JNCC.PublicWebsite.Core.Interfaces.Providers;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Providers;
using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "Filtering")]
    public class FilteringViewComponent : ViewComponent
    {
        private readonly INewsAndInsightsLandingService _newsAndInsightsLandingService;
        private readonly INewsAndInsightsLandingFilteringService _newsAndInsightsLandingFilteringService;
        private readonly IQueryService _queryService;

        public FilteringViewComponent(INewsAndInsightsLandingService newsAndInsightsLandingService, INewsAndInsightsLandingFilteringService newsAndInsightsLandingFilteringService, IQueryService queryService)
        {
            _newsAndInsightsLandingService = newsAndInsightsLandingService ?? throw new ArgumentNullException(nameof(newsAndInsightsLandingService));
            _newsAndInsightsLandingFilteringService = newsAndInsightsLandingFilteringService ?? throw new ArgumentNullException(nameof(newsAndInsightsLandingFilteringService));
            _queryService = queryService ?? throw new ArgumentNullException(nameof(queryService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            if (model is NewsAndInsightsLandingPage == false)
            {
                return null;
            }

            var filterModel = _queryService.GetFilterModel(Request);
            var viewModel = _newsAndInsightsLandingFilteringService.GetFilteringViewModel(filterModel, model); ;

            return View("~/Views/Partials/NewsAndInsightsLanding/Filtering.cshtml", viewModel);
        }
    }
}