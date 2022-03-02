using System;
using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "ScienceCategoryCategorisedPages")]
    public class ScienceCategoryCategorisedPagesViewComponent : ViewComponent
    {
        private readonly IScienceCategoryPageService _scienceCategoryPageService;
        public ScienceCategoryCategorisedPagesViewComponent(IScienceCategoryPageService scienceCategoryPageService)
        {
            _scienceCategoryPageService = scienceCategoryPageService ?? throw new ArgumentNullException(nameof(scienceCategoryPageService));
        }

        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var viewModel = new CategorisedPagesViewModel()
            {
                Heading = model.GetHeadline(),
                Pages = _scienceCategoryPageService.GetCategorisedPages(model as ScienceCategoryPage)
            };

            return View("~/Views/Partials/ScienceCategory/CategorisedPages.cshtml", viewModel);
        }
    }
}