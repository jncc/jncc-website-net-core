using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IScienceSidebarService
    {
        ScienceSidebarViewModel GetSidebarViewModel(ScienceDetailsPage model);

        ScienceSidebarViewModel GetSidebarViewModel(ScienceCategoryPage model);

        SidebarViewModel GetSidebarViewModel(ScienceAtoZpage model);
    }
}

