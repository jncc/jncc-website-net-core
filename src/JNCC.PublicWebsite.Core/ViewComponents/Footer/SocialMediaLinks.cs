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
    [ViewComponent(Name = "SocialMediaLinks")]
    public class SocialMediaLinksViewComponent : ViewComponent
    {
        private readonly ISocialMediaLinksService _socialMediaLinksService;

        public SocialMediaLinksViewComponent(ISocialMediaLinksService socialMediaLinksService)
        {
            _socialMediaLinksService = socialMediaLinksService ?? throw new ArgumentNullException(nameof(socialMediaLinksService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var homePage = model.Root() as HomePage;
            var links = _socialMediaLinksService.GetSocialMediaLinks(homePage.FooterSocialMediaLinks);

            return View("~/Views/Partials/Footer/SocialMediaLinks.cshtml", links);
        }
    }
}