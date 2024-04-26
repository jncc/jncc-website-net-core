using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "AtoZCategorisedPages")]
    public class AtoZCategorisedPagesViewComponent : ViewComponent
    {
        private readonly IScienceAtoZPageService _scienceAtoZPageService;
        public AtoZCategorisedPagesViewComponent(IScienceAtoZPageService scienceAtoZPageService)
        {
            _scienceAtoZPageService = scienceAtoZPageService ?? throw new ArgumentNullException(nameof(scienceAtoZPageService));
        }

        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var viewModel = _scienceAtoZPageService.GetCategorisedPages(model as ScienceAtoZpage);

            return View("~/Views/Partials/Science/CategorisedPages.cshtml", viewModel);
        }
    }
}