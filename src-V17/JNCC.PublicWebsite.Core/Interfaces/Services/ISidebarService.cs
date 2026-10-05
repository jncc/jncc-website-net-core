using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface ISidebarService
    {
        SidebarViewModel GetViewModel(ISidebarComposition composition);

        SidebarViewModel GetViewModelForArticlePage(ArticlePage composition);
    }
}
