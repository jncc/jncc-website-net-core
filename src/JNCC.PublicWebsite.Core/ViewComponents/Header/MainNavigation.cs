using System;
using System.Collections.Generic;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "MainNavigation")]
    public class MainNavigationViewComponent : ViewComponent
    {
        private readonly IPageHeroService _pageHeroService;
        private readonly IMainNavigationService _mainNavigationService;

        public MainNavigationViewComponent(IPageHeroService pageHeroService, IMainNavigationService mainNavigationService)
        {
            _pageHeroService = pageHeroService ?? throw new ArgumentNullException(nameof(pageHeroService));
            _mainNavigationService = mainNavigationService ?? throw new ArgumentNullException(nameof(mainNavigationService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var viewModel = new MainNavigationViewModel
            {
                Items = _mainNavigationService.GetRootMenuItems(model.Root(), model),
                HasPageHero = _pageHeroService.HasPageHero(model),
                Content = model,
            };

            return View("~/Views/Partials/Header/MainNavigation.cshtml", viewModel);
        }
    }
}
