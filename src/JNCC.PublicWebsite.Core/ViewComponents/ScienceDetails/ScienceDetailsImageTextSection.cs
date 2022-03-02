using System;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "ScienceDetailsImageAndTextSection")]
    public class ScienceDetailsImageAndTextSectionViewComponent : ViewComponent
    {
        private readonly IScienceDetailsPageService _scienceDetailsPageService;
        public ScienceDetailsImageAndTextSectionViewComponent(IScienceDetailsPageService scienceDetailsPageService)
        {
            _scienceDetailsPageService = scienceDetailsPageService ?? throw new ArgumentNullException(nameof(scienceDetailsPageService));
        }

        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var scienceDetails = model as ScienceDetailsPage;
            var viewModel = _scienceDetailsPageService.GetImageTextSectionViewModels(scienceDetails.ImageAndTextSection);

            return View("~/Views/Partials/ScienceDetails/ImageAndTextSection.cshtml", viewModel);
        }
    }
}