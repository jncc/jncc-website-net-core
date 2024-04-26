using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface ICategorisedFooterLinksService
    {
        IEnumerable<CategorisedFooterLinksViewModel> GetViewModels(IEnumerable<CategorisedFooterLinksSchema> schemas);
    }
}
