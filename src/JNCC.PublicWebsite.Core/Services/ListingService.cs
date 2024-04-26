﻿using JNCC.PublicWebsite.Core.Models;
using System.Collections.Specialized;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.Services
{
    internal abstract class ListingService<TParent, TChild, TViewModel, TFilteringModel> where TParent : IPublishedContent
                                                                                             where TChild : IPublishedContent
                                                                                             where TFilteringModel : FilteringModel
    {
        public Paged​Result<TViewModel> GetViewModels(TParent parent, TFilteringModel filteringModel)
        {
            var children = GetOrderedChildren(parent, filteringModel);
            var numberOfItemsPerPage = GetItemsPerPage(parent);
            var results = new Paged​Result<TViewModel>(children.LongCount(), filteringModel.PageNumber, numberOfItemsPerPage);

            var viewModels = children.Skip(results.GetSkipSize())
                                     .Take(numberOfItemsPerPage)
                                     .Select(ToViewModel);

            results.Items = viewModels;

            return results;
        }

        public abstract NameValueCollection ConvertFiltersToNameValueCollection(TFilteringModel filteringModel);

        protected abstract int GetItemsPerPage(TParent parent);

        protected abstract IOrderedEnumerable<TChild> GetOrderedChildren(TParent parent, TFilteringModel filteringModel);

        protected abstract TViewModel ToViewModel(TChild content);



    }
}