using System;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "ScienceDetailsPageSidebar")]
    public class ScienceDetailsPageSidebarViewComponent : ViewComponent
    {
        private readonly IScienceSidebarService _scienceSidebarService;

        public ScienceDetailsPageSidebarViewComponent(IScienceSidebarService scienceSidebarService)
        {
            _scienceSidebarService = scienceSidebarService ?? throw new ArgumentNullException(nameof(scienceSidebarService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            if (model is ScienceDetailsPage == false){ return null; }

            var viewModel = _scienceSidebarService.GetSidebarViewModel(model as ScienceDetailsPage);
            viewModel.CurrentPageUrl = model.Url();

            return View("~/Views/Partials/ScienceSidebar.cshtml", viewModel);
        }
    }
}