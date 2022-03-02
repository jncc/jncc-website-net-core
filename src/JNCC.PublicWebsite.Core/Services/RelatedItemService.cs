using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using System;
using System.Linq;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class RelatedItemsService : IRelatedItemService
    {
        private const int MaximumRelatedItems = 3;
        private const int MaximumNumberOfSearchResults = 6;
        private readonly ISearchQueryService _searchQueryService;

        //private readonly ISeoMetaDataService _seoMetaDataService;
        //private readonly UmbracoHelper _umbracoHelper;

        public RelatedItemsService(
            ISearchQueryService searchQueryService
            )
        {
            _searchQueryService = searchQueryService ?? throw new ArgumentNullException(nameof(RelatedItemsService));
            //_seoMetaDataService = new SeoMetaDataService();
            //_relatedItemsService = new RelatedItemsService(_seoMetaDataService, _searchQueryService, Umbraco);
        }

        public IEnumerable<RelatedItemViewModel> RenderRelatedItems(IPublishedContent model)
        {
            if (model is IRelatedItemsComposition == false)
            {
                return null;
            }

            var viewModels = GetViewModels(model as IRelatedItemsComposition, model.Root() as HomePage);

            if (ExistenceUtility.IsNullOrEmpty(viewModels))
            {
                return null;
            }

            return viewModels;
        }

        public IEnumerable<RelatedItemViewModel> GetViewModels(IRelatedItemsComposition composition, HomePage homePage)
        {
            var viewModels = new List<RelatedItemViewModel>();

            if (composition.ShowRelatedItems == false)
            {
                return viewModels;
            }
            var pickedRelatedItemViewModels = GetPickedRelatedItemViewModels(composition.RelatedItems, homePage);
            viewModels.AddRange(pickedRelatedItemViewModels);

            if (viewModels.Count < MaximumRelatedItems)
            {
                var excludedNodeIds = GetNodeIdsToExcludeFromSearchResults(composition);
                var searchQuery = GetSearchQuery(composition);
                var numberOfItemsToTake = MaximumRelatedItems - viewModels.Count();
                var searchedRelatedItemsViewModels = GetSearchQueryRelatedItemViewModels(searchQuery, MaximumNumberOfSearchResults, numberOfItemsToTake, excludedNodeIds, homePage);
                viewModels.AddRange(searchedRelatedItemsViewModels);
            }

            return viewModels;
        }

        private IEnumerable<string> GetNodeIdsToExcludeFromSearchResults(IRelatedItemsComposition composition)
        {
            var nodeIds = new List<string>
            {
                composition.Id.ToString()
            };

            if (ExistenceUtility.IsNullOrEmpty(composition.RelatedItems))
            {
                return nodeIds;
            }

            var pickedRelatedItemIds = composition.RelatedItems.Select(x => x.Id.ToString());
            nodeIds.AddRange(pickedRelatedItemIds);

            return nodeIds;
        }

        private string GetSearchQuery(IRelatedItemsComposition composition)
        {
            var searchTerm = composition.RelatedItemsSearchQuery;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return composition.GetHeadline();
            }

            return searchTerm;
        }

        private IEnumerable<RelatedItemViewModel> GetPickedRelatedItemViewModels(IEnumerable<IPublishedContent> relatedItems, HomePage homePage)
        {
            var viewModels = new List<RelatedItemViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(relatedItems))
            {
                return viewModels;
            }

            foreach (var item in relatedItems)
            {
                var viewModel = GetViewModel(item, homePage);

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        private IEnumerable<RelatedItemViewModel> GetSearchQueryRelatedItemViewModels(string searchQuery, int numberOfItemsToSearchFor, int numberOfItemsToTake, IEnumerable<string> excludedNodeIds, HomePage homePage)
        {
            var viewModels = new List<RelatedItemViewModel>();
            var searchResults = _searchQueryService.Query(searchQuery, numberOfItemsToSearchFor, 0, SearchIndexingSites.Website);
            
            if(searchResults != null && (searchResults.Hits?.Results?.Any() ?? false))
            {
                var filteredSearchResults = searchResults.Hits.Results.Where(x => excludedNodeIds.Contains(x.Id) == false)?
                                                                  .Take(numberOfItemsToTake)?.ToList();
                if(filteredSearchResults?.Any() ?? false)
                {
                    foreach (var result in filteredSearchResults)
                    {
                        //var content = _umbracoHelper.TypedContent(result.Id);

                        //if (content != null)
                        //{
                        //    var viewModel = GetViewModel(content, homePage);

                        //    viewModels.Add(viewModel);
                        //}
                    }
                }
            }
            return viewModels;
        }

        private RelatedItemViewModel GetViewModel(IPublishedContent item, HomePage homePage)
        {
            var viewModel = new RelatedItemViewModel()
            {
                //Content = _seoMetaDataService.GetSeoDescription(item),
                Title = item.GetHeadline(),
                Url = item.Url()
            };

            if (item is IPageHeroComposition)
            {
                var hero = (item as IPageHeroComposition);
                var heroImage = hero.HeroImage;

                if (heroImage != null)
                {
                    viewModel.ImageUrl = hero.HeroImage.GetCropUrl(Constants.ImageCropAliases.ListingThumbnail);
                }
            }

            if (string.IsNullOrWhiteSpace(viewModel.ImageUrl) && homePage.RelatedItemsFallbackImage != null)
            {
                viewModel.ImageUrl = homePage.RelatedItemsFallbackImage.GetCropUrl(Constants.ImageCropAliases.ListingThumbnail);
            }

            return viewModel;
        }
    }
}
