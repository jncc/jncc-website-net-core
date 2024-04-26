using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Cms.Core.Models;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface ISocialMediaLinksService
    {
        IEnumerable<SocialMediaNavigationItemViewModel> GetSocialMediaLinks(IEnumerable<Link> relatedLinks);
    }
}
