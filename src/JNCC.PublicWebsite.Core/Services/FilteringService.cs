﻿using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Interfaces.Providers;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Providers;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JNCC.PublicWebsite.Core.Services
{
    internal abstract class FilteringService<TModel, TViewModel, TRoot> where TModel : FilteringModel
                                                                        where TViewModel : FilteringViewModel
    {
        public abstract TViewModel GetFilteringViewModel(TModel filteringModel, TRoot root);

        protected IReadOnlyDictionary<string, bool> GetFilters(IEnumerable<int> allFilters, IEnumerable<int> selectedFilters)
        {
            if (ExistenceUtility.IsNullOrEmpty(allFilters))
            {
                return null;
            }

            if (ExistenceUtility.IsNullOrEmpty(selectedFilters))
            {
                return allFilters.AllToString()
                                 .ToDictionary(x => x, x => false);
            }

            return allFilters.ToDictionary(x => x.ToString(), x => selectedFilters.Contains(x));
        }

        protected IReadOnlyDictionary<string, bool> GetFilters(IEnumerable<string> allFilters, IEnumerable<string> selectedFilters)
        {
            if (ExistenceUtility.IsNullOrEmpty(allFilters))
            {
                return null;
            }

            if (ExistenceUtility.IsNullOrEmpty(selectedFilters))
            {
                return allFilters.ToDictionary(x => x, x => false);
            }

            return allFilters.ToDictionary(x => x, x => selectedFilters.Contains(x, StringComparer.OrdinalIgnoreCase));
        }
    }
}