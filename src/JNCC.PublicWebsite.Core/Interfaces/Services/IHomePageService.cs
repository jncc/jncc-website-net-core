using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IHomePageService
    {
        IEnumerable<ResourceItemViewModel> GetResourcesItems(HomePage content);
        CarouselViewModel GetCarouselViewModel(IPageHeroCarouselComposition content);
        ResourcesViewModel GetResources(HomePage content);
    }
}
