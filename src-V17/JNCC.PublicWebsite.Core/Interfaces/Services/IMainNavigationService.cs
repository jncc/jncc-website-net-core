using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IMainNavigationService
    {
        IEnumerable<MainNavigationItemViewModel> GetRootMenuItems(IPublishedContent root, IPublishedContent currentPage);
    }
}
