using System;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "CalloutCards")]
    public class CalloutCardsViewComponent : ViewComponent
    {
        private readonly ICalloutCardsService _calloutCardsService;
        public CalloutCardsViewComponent(ICalloutCardsService calloutCardsService)
        {
            _calloutCardsService = calloutCardsService ?? throw new ArgumentNullException(nameof(calloutCardsService));
        }

        public IViewComponentResult Invoke(IPublishedContent model)
        {
            if (model is ScienceLandingPage scienceLanding)
            {
                var viewModel = _calloutCardsService.GetCalloutCards(scienceLanding.CalloutCards);

                return View("~/Views/Partials/CalloutCards.cshtml", viewModel);
            }
            else
            {
                var homepage = model as HomePage;
                var viewModel = _calloutCardsService.GetCalloutCards(homepage?.CalloutCards);

                return View("~/Views/Partials/CalloutCards.cshtml", viewModel);
            }
        }
    }
}