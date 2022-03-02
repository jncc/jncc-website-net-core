using System;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "Carousel")]
    public class CarouselViewComponent : ViewComponent
    {
        private readonly IHomePageService _homePageService;

        public CarouselViewComponent(IHomePageService homePageService)
        {
            _homePageService = homePageService ?? throw new ArgumentNullException(nameof(homePageService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var viewModel = _homePageService.GetCarouselViewModel(model as HomePage);

            return View("~/Views/Partials/Home/Carousel.cshtml", viewModel);
        }
    }
}