using System;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "ScienceCategorySections")]
    public class ScienceCategorySectionsViewComponent : ViewComponent
    {
        private readonly IScienceCategoryPageService _scienceCategoryPageService;
        public ScienceCategorySectionsViewComponent(IScienceCategoryPageService scienceCategoryPageService)
        {
            _scienceCategoryPageService = scienceCategoryPageService ?? throw new ArgumentNullException(nameof(scienceCategoryPageService));
        }

        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var scienceCategory = model as ScienceCategoryPage;
            var viewModel = _scienceCategoryPageService.GetSectionViewModels(scienceCategory.MainContent);

            return View("~/Views/Partials/ScienceCategory/Sections.cshtml", viewModel);
        }
    }
}