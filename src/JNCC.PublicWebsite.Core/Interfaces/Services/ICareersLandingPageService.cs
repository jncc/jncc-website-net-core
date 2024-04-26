using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface ICareersLandingPageService
    {
        IEnumerable<CareersListItemViewModel> GetCareers(CareersLandingPage model);
    }
}
