using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "ScienceDetailsCategories")]
    public class ScienceDetailsCategoriesViewComponent : ViewComponent
    {
        private readonly IScienceDetailsPageService _scienceDetailsPageService;
        public ScienceDetailsCategoriesViewComponent(IScienceDetailsPageService scienceDetailsPageService)
        {
            _scienceDetailsPageService = scienceDetailsPageService ?? throw new ArgumentNullException(nameof(scienceDetailsPageService));
        }

        public IViewComponentResult Invoke(IPublishedContent model)
        {
            if (model is ScienceDetailsPage scienceDetailsPage)
            {
                return View(
                    "~/Views/Partials/ScienceDetails/Categories.cshtml",
                    _scienceDetailsPageService.GetCategories(scienceDetailsPage)
                );
                
            }
            else if (model is SimpleScienceDetailsPage simpleScienceDetailsPage)
            {
                return View(
                    "~/Views/Partials/ScienceDetails/Categories.cshtml",
                    _scienceDetailsPageService.GetCategories(simpleScienceDetailsPage)
                );
            }

            return View("~/Views/Partials/ScienceDetails/Categories.cshtml", new List<ViewModels.NavigationItemViewModel>());
        }
    }
}