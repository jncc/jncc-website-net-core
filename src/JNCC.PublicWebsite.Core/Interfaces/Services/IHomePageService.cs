using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IHomePageService
    {
        IEnumerable<ResourceItemViewModel> GetResourcesItems(HomePage content);
        CarouselViewModel GetCarouselViewModel(IPageHeroCarouselComposition content);
        ResourcesViewModel GetResources(HomePage content);
    }
}
