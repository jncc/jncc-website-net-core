using System;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "NoPageHeroHeadline")]
    public class NoPageHeroHeadlineViewComponent : ViewComponent
    {
        private readonly IPageHeroService _pageHeroService;

        public NoPageHeroHeadlineViewComponent(IPageHeroService pageHeroService)
        {
            _pageHeroService = pageHeroService ?? throw new ArgumentNullException(nameof(pageHeroService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var viewModel = _pageHeroService.RenderNoPageHeroHeadline(model);

            return View("~/Views/Partials/NoPageHeroHeadline.cshtml", viewModel);
        }
    }
}