using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.ViewModels;
using JNCC.PublicWebsite.Core.Models.Custom;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class BreadcrumbsService : IBreadcrumbsService
    {
        private readonly INavigationItemService _navigationItemService;
        public BreadcrumbsService(INavigationItemService navigationItemService)
        {
            _navigationItemService = navigationItemService ?? throw new ArgumentNullException(nameof(navigationItemService));
        }

        public BreadcrumbsViewModel RenderBreadcrumbs(IPublishedContent model)
        {
            var visibleOrderedAncestors = model.Ancestors()
                                                     .Where(x => x.IsVisible())
                                                     .OrderBy(x => x.Level)
                                                     .ToList();

            var viewModel = new BreadcrumbsViewModel()
            {
                Ancestors = _navigationItemService.GetViewModels(visibleOrderedAncestors),
                CurrentPage = model.Name
            };

            //special case for virtual resource pages
            if (model is VirtualResourceModel virtualResourcePage)
            {
                if (virtualResourcePage.ResourceToDisplay != null)
                {
                    ((IList<NavigationItemViewModel>)viewModel.Ancestors).Add(new NavigationItemViewModel() { Text = virtualResourcePage.Name, Url = virtualResourcePage.Url() });
                    viewModel.CurrentPage = "";
                }
            }

            return viewModel;
        }
    }
}
