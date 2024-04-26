using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "IFrameSections")]
    public class IFrameSectionsViewComponent : ViewComponent
    {
        private readonly IIFramePageService _iFramePageService;

        public IFrameSectionsViewComponent(IIFramePageService iFramePageService)
        {
            _iFramePageService = iFramePageService ?? throw new ArgumentNullException(nameof(iFramePageService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var iFramePage = model as IFramePage;

            var viewModel = _iFramePageService.GetSectionViewModels(iFramePage.FallbackContent);

            return View("~/Views/Partials/IFrame/Sections.cshtml", viewModel);
        }
    }
}