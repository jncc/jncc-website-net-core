using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IMainNavigationService
    {
        IEnumerable<MainNavigationItemViewModel> GetRootMenuItems(IPublishedContent root, IPublishedContent currentPage);
    }
}
