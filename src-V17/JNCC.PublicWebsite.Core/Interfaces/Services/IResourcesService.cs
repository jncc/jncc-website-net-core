using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IResourcesService
    {
        IEnumerable<FeaturedResourceViewModel> GetFeaturedResources(ResourcesPage content);
    }
}
