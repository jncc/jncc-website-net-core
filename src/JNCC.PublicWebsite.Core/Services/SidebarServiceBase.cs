using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Services
{
    internal abstract class SidebarServiceBase
    {
        private const int _maxNumberOfResults = 5;
        protected readonly INavigationItemService _navigationItemService;

        public SidebarServiceBase(INavigationItemService navigationItemService)
        {
            _navigationItemService = navigationItemService;
        }

        protected T CreateViewModel<T>(ISidebarComposition composition) where T : BasicSidebarViewModel, new()
        {
            return new T
            {
                PrimaryCallToActionButton = _navigationItemService.GetViewModel(composition.SidebarPrimaryCallToActionButton),
                OtherWebsitesLinks = _navigationItemService.GetViewModels(composition.SidebarOtherWebsites),
                SeeAlsoLinks = _navigationItemService.GetViewModels(composition.SidebarSeeAlsoLinks)
            };
        }

    }
}