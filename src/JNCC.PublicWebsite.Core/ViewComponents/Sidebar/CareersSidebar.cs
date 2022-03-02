using System;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "CareersSidebar")]
    public class CareersSidebarViewComponent : ViewComponent
    {
        private readonly ICareersSidebarService _careersSidebarService;

        public CareersSidebarViewComponent(ICareersSidebarService careersSidebarService)
        {
            _careersSidebarService = careersSidebarService ?? throw new ArgumentNullException(nameof(careersSidebarService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            if (model is CareersLandingPage == false)
            {
                return null;
            }

            var viewModel = _careersSidebarService.GetViewModel(model as CareersLandingPage);
            return View("~/Views/Partials/CareersSidebar.cshtml", viewModel);
        }
    }
}