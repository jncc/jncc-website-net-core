using System;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "ScienceCategoryImageTextSection")]
    public class ScienceCategoryImageTextSectionViewComponent : ViewComponent
    {
        private readonly IScienceCategoryPageService _scienceCategoryPageService;
        public ScienceCategoryImageTextSectionViewComponent(IScienceCategoryPageService scienceCategoryPageService)
        {
            _scienceCategoryPageService = scienceCategoryPageService ?? throw new ArgumentNullException(nameof(scienceCategoryPageService));
        }

        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var scienceCategory = model as ScienceCategoryPage;
            var viewModel = _scienceCategoryPageService.GetImageTextSectionViewModels(scienceCategory.ImageAndTextSection);

            return View("~/Views/Partials/ScienceCategory/ImageAndTextSection.cshtml", viewModel);
        }
    }
}