using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Services;
using JNCC.PublicWebsite.Core.ViewModels;
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
            if (model is ScienceDetailsPage scienceDetailsPage)
            {
                var sdpViewModel = _scienceSidebarService.GetSidebarViewModel(scienceDetailsPage);
                sdpViewModel.CurrentPageUrl = model.Url();
                sdpViewModel.CurrentPageContentTypeAlias = model.ContentType.Alias;
                return View(
                    "~/Views/Partials/ScienceSidebar.cshtml",
                    sdpViewModel
                );

            }
            else if (model is SimpleScienceDetailsPage simpleScienceDetailsPage)
            {
                var ssdpViewModel = _scienceSidebarService.GetSidebarViewModel(simpleScienceDetailsPage);
                ssdpViewModel.CurrentPageUrl = model.Url();
                ssdpViewModel.CurrentPageContentTypeAlias = model.ContentType.Alias;
                return View(
                    "~/Views/Partials/ScienceSidebar.cshtml",
                    ssdpViewModel
                );
            }

            return View("~/Views/Partials/ScienceSidebar.cshtml", new ScienceSidebarViewModel());
        }
    }
}