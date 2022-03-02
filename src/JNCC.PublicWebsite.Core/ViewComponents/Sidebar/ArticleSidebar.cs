using System;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "ArticleSidebar")]
    public class ArticleSidebarViewComponent : ViewComponent
    {
        private readonly ISidebarService _sidebarService;

        public ArticleSidebarViewComponent(ISidebarService sidebarService)
        {
            _sidebarService = sidebarService ?? throw new ArgumentNullException(nameof(sidebarService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            if (model is ArticlePage == false)
            {
                return null;
            }

            var viewModel = _sidebarService.GetViewModelForArticlePage(model as ArticlePage);

            viewModel.CurrentPageUrl = model.Url();

            return View("~/Views/Partials/Sidebar.cshtml", viewModel);
        }
    }
}