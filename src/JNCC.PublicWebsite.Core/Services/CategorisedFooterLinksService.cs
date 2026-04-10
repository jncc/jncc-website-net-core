using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Cms.Core.Models.Blocks;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class CategorisedFooterLinksService : ICategorisedFooterLinksService
    {
        private readonly INavigationItemService _navigationItemService;

        public CategorisedFooterLinksService(INavigationItemService navigationItemService)
        {
            _navigationItemService = navigationItemService ?? throw new ArgumentNullException(nameof(navigationItemService)); ;
        }

        public IEnumerable<CategorisedFooterLinksViewModel> GetViewModels(BlockListModel schemas)
        {
            if (schemas == null)
            {
                return new List<CategorisedFooterLinksViewModel>();
            }

            //IEnumerable<CategorisedFooterLinksSchema>
            var viewModels = new List<CategorisedFooterLinksViewModel>();

            foreach (var schema in schemas)
            {
                if (schema.Content is CategorisedFooterLinksSchema categorisedFooterLinks)
                {
                    var categoryViewModel = new CategorisedFooterLinksViewModel()
                    {
                        Heading = categorisedFooterLinks.Heading ?? "",
                        Links = _navigationItemService.GetViewModels(categorisedFooterLinks.Links)
                    };

                    viewModels.Add(categoryViewModel);
                }
            }

            return viewModels;
        }
    }
}
