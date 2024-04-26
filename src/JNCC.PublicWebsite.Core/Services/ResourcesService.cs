using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Services
{
    public class ResourcesService : IResourcesService
    {
        private readonly INavigationItemService _navigationItemService;

        public ResourcesService(INavigationItemService navigationItemService)
        {
            _navigationItemService = navigationItemService;
        }

        public IEnumerable<FeaturedResourceViewModel> GetFeaturedResources(ResourcesPage content)
        {
            var model = new List<FeaturedResourceViewModel>();

            foreach (var resource in content.FeaturedResources)
            {
                model.Add(GetViewModel(resource));
            }

            return model;
        }

        private FeaturedResourceViewModel GetViewModel(FeaturedResourceSchema resource)
        {
            return new FeaturedResourceViewModel()
            {
                Headline = resource.Headline,
                Content = resource.Content,
                ReadMoreButton = _navigationItemService.GetViewModel(resource.Link)
            };
        }
    }
}
