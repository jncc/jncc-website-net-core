using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Cms.Core.Models;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class SocialMediaLinksService : ISocialMediaLinksService
    {
        private readonly INavigationItemService _navigationItemService;

        public SocialMediaLinksService(INavigationItemService navigationItemService)
        {
            _navigationItemService = navigationItemService ?? throw new ArgumentNullException(nameof(navigationItemService));
        }

        public IEnumerable<SocialMediaNavigationItemViewModel> GetSocialMediaLinks(IEnumerable<Link> relatedLinks)
        {
            var links = new List<SocialMediaNavigationItemViewModel>();

            if (relatedLinks == null)
            {
                return links;
            }

            foreach (var relatedLink in relatedLinks)
            {
                var link = _navigationItemService.GetViewModel<SocialMediaNavigationItemViewModel>(relatedLink);
                link.IconClassSuffix = relatedLink.Name.ToLower();
                if (link.IconClassSuffix == "twitter") { link.IconClassSuffix = "x"; }

                links.Add(link);
            }

            return links;
        }
    }
}
