﻿using JNCC.PublicWebsite.Core.Interfaces.Providers;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ScienceSidebarService : SidebarServiceBase, IScienceSidebarService
    {
        private readonly ISciencePageCategoriesProvider _sciencePageCategoriesProvider;

        public ScienceSidebarService(INavigationItemService navigationItemService
            ,IDataHubRawQueryService dataHubRawQueryService
            , ISciencePageCategoriesProvider sciencePageCategoriesProvider) 
            : base(navigationItemService
                  , dataHubRawQueryService
                  )
        {
            _sciencePageCategoriesProvider = sciencePageCategoriesProvider ?? throw new ArgumentNullException(nameof(sciencePageCategoriesProvider));
        }

        public ScienceSidebarViewModel GetSidebarViewModel(ScienceDetailsPage model)
        {
            var viewModel = CreateViewModel<ScienceSidebarViewModel>(model);
            viewModel.Categories = GetCategoriesWithFeaturedPages(model);
            viewModel.RelatedCategories = GetRelatedCategoryPages(model);
            return viewModel;
        }


        public ScienceSidebarViewModel GetSidebarViewModel(ScienceCategoryPage model)
        {
            var viewModel = CreateViewModel<ScienceSidebarViewModel>(model);
            viewModel.Categories = GetCategoriesWithFeaturedPages(model);
            viewModel.RelatedCategories = GetRelatedCategoriesPages(model);
            return viewModel;
        }

        
        public SidebarViewModel GetSidebarViewModel(ScienceAtoZpage model)
        {
            return CreateViewModel<SidebarViewModel>(model);
        }

        private IEnumerable<MainNavigationItemViewModel> GetCategoriesWithFeaturedPages(ScienceDetailsPage model)
        {
            var categories = _sciencePageCategoriesProvider.GetCategories(model);

            return GetCategoriesWithFeaturedPages(categories);
        }

        private IEnumerable<MainNavigationItemViewModel> GetRelatedCategoriesPages(ScienceCategoryPage model)
        {
            var categories = GetRelatedCategoryPages(model);

            return categories;
        }

        private IEnumerable<MainNavigationItemViewModel> GetCategoriesWithFeaturedPages(ScienceCategoryPage model)
        {
            var category = GetCategoryWithFeaturedPages(model);

            return category.AsEnumerableOfOne();
        }

        private IEnumerable<MainNavigationItemViewModel> GetCategoriesWithFeaturedPages(IEnumerable<ScienceCategoryPage> categories)
        {
            if (ExistenceUtility.IsNullOrEmpty(categories))
            {
                return null;
            }

            var viewModels = new List<MainNavigationItemViewModel>();

            foreach (var category in categories)
            {
                var viewModel = GetCategoryWithFeaturedPages(category);
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        private MainNavigationItemViewModel GetCategoryWithFeaturedPages(ScienceCategoryPage category)
        {
            var viewModel = _navigationItemService.GetViewModel<MainNavigationItemViewModel>(category);
            viewModel.Children = _navigationItemService.GetViewModels<MainNavigationItemViewModel>(category.FeaturedPages);

            return viewModel;
        }

        private IEnumerable<MainNavigationItemViewModel> GetRelatedCategoryPages(ScienceDetailsPage category)
        {
            var viewModel =
                _navigationItemService.GetViewModels<MainNavigationItemViewModel>(category.RelatedCategories);

            return viewModel;
        }

        private IEnumerable<MainNavigationItemViewModel> GetRelatedCategoryPages(ScienceCategoryPage category)
        {
            var viewModel =
                _navigationItemService.GetViewModels<MainNavigationItemViewModel>(category.RelatedCategories);
                
            return viewModel;
        }
    }
}
