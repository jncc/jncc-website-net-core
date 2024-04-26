using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Cms.Core.Models.Blocks;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IIFramePageService
    {
        string GetSourceUrl(IFramePage model, string currentUrl);
        MainNavigationViewModel GetNavigation(IFramePage model);
        IEnumerable<ScienceIFrameSectionViewModel> GetSectionViewModels(BlockListModel mainContent);
    }
}
