using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "ScienceDetailsSections")]
    public class ScienceDetailsSectionsViewComponent : ViewComponent
    {
        private readonly IScienceDetailsPageService _scienceDetailsPageService;
        public ScienceDetailsSectionsViewComponent(IScienceDetailsPageService scienceDetailsPageService)
        {
            _scienceDetailsPageService = scienceDetailsPageService ?? throw new ArgumentNullException(nameof(scienceDetailsPageService));
        }

        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var scienceDetails = model as ScienceDetailsPage;
            var viewModel = _scienceDetailsPageService.GetSectionViewModels(scienceDetails.MainContent);

            return View("~/Views/Partials/ScienceDetails/Sections.cshtml", viewModel);
        }
    }
}