using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Cms.Core.Models.Blocks;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface ICategorisedFooterLinksService
    {
        IEnumerable<CategorisedFooterLinksViewModel> GetViewModels(BlockListModel schemas);
    }
}
