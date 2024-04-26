using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "ResourcesCollections")]
    public class ResourcesCollectionsViewComponent : ViewComponent
    {
        private readonly IScienceLandingPageService _scienceLandingPageService;
        public ResourcesCollectionsViewComponent(IScienceLandingPageService scienceLandingPageService)
        {
            _scienceLandingPageService = scienceLandingPageService ?? throw new ArgumentNullException(nameof(scienceLandingPageService));
        }

        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var viewModel = _scienceLandingPageService.GetResourcesCollections(model as ScienceLandingPage);

            return View("~/Views/Partials/ScienceLanding/ResourcesCollections.cshtml", viewModel);
        }
    }
}