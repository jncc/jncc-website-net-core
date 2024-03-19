using System;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "Listing")]
    public class ListingViewComponent : ViewComponent
    {
        private readonly INewsAndInsightsLandingService _newsAndInsightsLandingService;
        private readonly IQueryService _queryService;

        public ListingViewComponent(INewsAndInsightsLandingService newsAndInsightsLandingService, IQueryService queryService)
        {
            _newsAndInsightsLandingService = newsAndInsightsLandingService ?? throw new ArgumentNullException(nameof(newsAndInsightsLandingService));
            _queryService = queryService ?? throw new ArgumentNullException(nameof(queryService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            if (model is NewsAndInsightsLandingPage == false)
            {
                return null;
            }

            var filterModel = _queryService.GetFilterModel(Request);
            var viewModel = new NewsAndInsightsLandingListingViewModel
            {
                Items = _newsAndInsightsLandingService.GetViewModels(model as NewsAndInsightsLandingPage, filterModel),
                Filters = _newsAndInsightsLandingService.ConvertFiltersToNameValueCollection(filterModel)
            };

            return View("~/Views/Partials/NewsAndInsightsLanding/Listing.cshtml", viewModel);
        }
    }
}