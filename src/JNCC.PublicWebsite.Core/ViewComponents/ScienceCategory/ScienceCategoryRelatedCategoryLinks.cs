using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "ScienceCategoryRelatedCategoryLinks")]
    public class ScienceCategoryRelatedCategoryLinksViewComponent : ViewComponent
    {
        private readonly IScienceCategoryPageService _scienceCategoryPageService;
        public ScienceCategoryRelatedCategoryLinksViewComponent(IScienceCategoryPageService scienceCategoryPageService)
        {
            _scienceCategoryPageService = scienceCategoryPageService ?? throw new ArgumentNullException(nameof(scienceCategoryPageService));
        }

        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var viewModel = _scienceCategoryPageService.GetRelatedCategories(model as ScienceCategoryPage);

            return View("~/Views/Partials/ScienceCategory/RelatedCategoriesLinks.cshtml", viewModel);
        }
    }
}