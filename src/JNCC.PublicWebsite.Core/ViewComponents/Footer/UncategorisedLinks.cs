using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "UncategorisedLinks")]
    public class UncategorisedLinksViewComponent : ViewComponent
    {
        private readonly INavigationItemService _navigationItemService;

        public UncategorisedLinksViewComponent(INavigationItemService navigationItemService)
        {
            _navigationItemService = navigationItemService ?? throw new ArgumentNullException(nameof(navigationItemService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var homePage = model.Root() as HomePage;
            var links = _navigationItemService.GetViewModels(homePage?.FooterUncategorisedLinks);

            return View("~/Views/Partials/Footer/UncategorisedLinks.cshtml", links);
        }
    }
}