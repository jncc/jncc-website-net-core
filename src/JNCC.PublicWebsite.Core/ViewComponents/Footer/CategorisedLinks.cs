using System;
using System.Collections.Generic;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "CategorisedLinks")]
    public class CategorisedLinksViewComponent : ViewComponent
    {
        private readonly ICategorisedFooterLinksService _categorisedFooterLinksService;

        public CategorisedLinksViewComponent(ICategorisedFooterLinksService categorisedFooterLinksService)
        {
            _categorisedFooterLinksService = categorisedFooterLinksService ?? throw new ArgumentNullException(nameof(categorisedFooterLinksService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var homePage = model.Root() as HomePage;
            var categorisedLinks = _categorisedFooterLinksService.GetViewModels(homePage?.FooterCategorisedLinks);

            return View("~/Views/Partials/Footer/CategorisedLinks.cshtml", categorisedLinks);
        }
    }
}