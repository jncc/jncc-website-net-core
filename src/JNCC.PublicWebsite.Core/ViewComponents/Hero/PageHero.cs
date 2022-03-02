using System;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "PageHero")]
    public class PageHeroViewComponent : ViewComponent
    {
        private readonly IPageHeroService _pageHeroService;

        public PageHeroViewComponent(IPageHeroService pageHeroService)
        {
            _pageHeroService = pageHeroService ?? throw new ArgumentNullException(nameof(pageHeroService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var viewModel = _pageHeroService.RenderPageHero(model);

            return View("~/Views/Partials/PageHero.cshtml", viewModel);
        }
    }
}