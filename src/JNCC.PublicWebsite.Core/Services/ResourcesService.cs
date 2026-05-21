using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Cms.Core.Strings;

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
            if (content.FeaturedResources is null)
            {
                return new List<FeaturedResourceViewModel>();
            }

            var model = new List<FeaturedResourceViewModel>();

            foreach (var resource in content.FeaturedResources)
            {
                if (resource.Content is FeaturedResourceSchema resourceSchema)
                {
                    model.Add(GetViewModel(resourceSchema));
                }
            }

            return model;
        }

        private FeaturedResourceViewModel GetViewModel(FeaturedResourceSchema resource)
        {
            return new FeaturedResourceViewModel()
            {
                Headline = resource.Headline ?? "",
                Content = resource.Content ?? new HtmlEncodedString(""),
                ReadMoreButton = _navigationItemService.GetViewModel(resource.Link)
            };
        }
    }
}
