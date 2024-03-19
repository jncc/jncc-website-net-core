using System;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "Breadcrumbs")]
    public class BreadcrumbsViewComponent : ViewComponent
    {
        private readonly IBreadcrumbsService _breadcrumbsService;

        public BreadcrumbsViewComponent(IBreadcrumbsService breadcrumbsService)
        {
            _breadcrumbsService = breadcrumbsService ?? throw new ArgumentNullException(nameof(breadcrumbsService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var viewModel = _breadcrumbsService.RenderBreadcrumbs(model);

            return View("~/Views/Partials/Breadcrumbs.cshtml", viewModel);
        }
    }
}
