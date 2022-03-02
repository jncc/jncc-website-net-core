using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface ISocialMediaLinksService
    {
        IEnumerable<SocialMediaNavigationItemViewModel> GetSocialMediaLinks(IEnumerable<Link> relatedLinks);
    }
}
