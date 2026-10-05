using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "ScienceCategoryPageSidebar")]
    public class ScienceCategoryPageSidebarViewComponent : ViewComponent
    {
        private readonly IScienceSidebarService _scienceSidebarService;

        public ScienceCategoryPageSidebarViewComponent(IScienceSidebarService scienceSidebarService)
        {
            _scienceSidebarService = scienceSidebarService ?? throw new ArgumentNullException(nameof(scienceSidebarService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            if (model is ScienceCategoryPage == false)
            {
                return null;
            }

            var viewModel = _scienceSidebarService.GetSidebarViewModel(model as ScienceCategoryPage);

            return View("~/Views/Partials/ScienceSidebar.cshtml", viewModel);
        }
    }
}