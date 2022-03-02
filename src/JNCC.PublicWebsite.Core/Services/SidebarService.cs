using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class SidebarService : SidebarServiceBase, ISidebarService
    {
        private const int _sectionRootLevel = 2;

        public SidebarService(INavigationItemService navigationItemService
            , IDataHubRawQueryService dataHubRawQueryService
            ) : base(navigationItemService
                , dataHubRawQueryService
                )
        {
        }

        public SidebarViewModel GetViewModel(ISidebarComposition composition)
        {
            var sectionRoot = GetSectionAncestor(composition);

            var viewModel = CreateViewModel<SidebarViewModel>(composition);
            viewModel.InThisSectionLinks = GetInThisSectionLinks(composition);

            if (sectionRoot != null && sectionRoot.IsNotEqual(composition))
            {
                viewModel.AlsoInLinksTitle = GetAlsoInLinksTitle(sectionRoot);
                viewModel.AlsoInLinks = GetAlsoInLinks(sectionRoot);
            }

            return viewModel;
        }

        private IPublishedContent GetSectionAncestor(ISidebarComposition composition)
        {
            return composition.Ancestors()
                              .OrderBy(x => x.Level)
                              .FirstOrDefault(x => x.Level == _sectionRootLevel);
        }

        public SidebarViewModel GetViewModelForArticlePage(ArticlePage composition)
        {
            var viewModel = new SidebarViewModel()
            {
                PrimaryCallToActionButton = _navigationItemService.GetViewModel(composition.SidebarPrimaryCallToActionButton),
                SeeAlsoLinks = _navigationItemService.GetViewModels(composition.SidebarSeeAlsoLinks)
            };

            return viewModel;
        }

        private IEnumerable<NavigationItemViewModel> GetInThisSectionLinks(ISidebarComposition composition)
        {
            return _navigationItemService.GetViewModels(composition.VisibleChildren());
        }

        private string GetAlsoInLinksTitle(IPublishedContent root)
        {
            return string.Format("Also in {0}", root.Name);
        }

        private IEnumerable<NavigationItemViewModel> GetAlsoInLinks(IPublishedContent root)
        {
            return _navigationItemService.GetViewModels(root.VisibleChildren());
        }
    }
}
