using System;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "PageAttributes")]
    public class PageAttributesViewComponent : ViewComponent
    {
        private readonly IPageIncludesService _pageIncludesService;

        public PageAttributesViewComponent(IPageIncludesService pageIncludesService)
        {
            _pageIncludesService = pageIncludesService ?? throw new ArgumentNullException(nameof(pageIncludesService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            PageAttributesViewModel viewModel = new PageAttributesViewModel();

            if (model is IPageSpecificIncludesComposition)
            {
                viewModel = _pageIncludesService.GetPageAttributesViewModel(model as IPageSpecificIncludesComposition);
            }

            return View("~/Views/Partials/PageAttributes.cshtml", viewModel);
        }
    }
}