using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "ScienceAtoZPageSidebar")]
    public class ScienceAtoZPageSidebarViewComponent : ViewComponent
    {
        private readonly IScienceSidebarService _scienceSidebarService;

        public ScienceAtoZPageSidebarViewComponent(IScienceSidebarService scienceSidebarService)
        {
            _scienceSidebarService = scienceSidebarService ?? throw new ArgumentNullException(nameof(scienceSidebarService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            if (model is ScienceAtoZpage == false)
            {
                return null;
            }

            var viewModel = _scienceSidebarService.GetSidebarViewModel(model as ScienceAtoZpage);

            return View("~/Views/Partials/Sidebar.cshtml", viewModel);
        }
    }
}