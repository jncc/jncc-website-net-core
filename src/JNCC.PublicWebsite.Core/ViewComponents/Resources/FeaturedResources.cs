using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JNCC.PublicWebsite.Core.ViewComponents.Resources
{
    [ViewComponent(Name = "FeaturedResources")]
    public class FeaturedResourcesViewComponent: ViewComponent
    {
        private readonly IResourcesService _resourcesService;

        public FeaturedResourcesViewComponent(IResourcesService resourcesService)
        {
            _resourcesService = resourcesService;
        }

        public IViewComponentResult Invoke(ResourcesPage model)
        {
            if (model.FeaturedResources?.Any() == true)
            {
                var viewModel = new FeaturedResourcesViewModel()
                {
                    Title = model.FeaturedResourcesTitle,
                    FeaturedResources = _resourcesService.GetFeaturedResources(model)
                };

                return View("~/Views/Partials/Resources/FeaturedResources.cshtml", viewModel);
            }

            return Content(string.Empty);
        }
    }
}
