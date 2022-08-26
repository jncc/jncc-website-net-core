using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Extensions;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Web.Common.Mvc;
using Umbraco.Cms.Core.Strings;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using System;
using System.Text.RegularExpressions;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ScienceLandingPageService : IScienceLandingPageService
    {
        private const int NumberOfLatestUpdatesItems = 3;
        private const int LatestUpdateItemContentLength = 75;

        private readonly HtmlStringUtilities _htmlStringUtilities;
        private readonly ICalloutCardsService _calloutCardsService;
        private readonly INavigationItemService _navigationItemService;
        public ScienceLandingPageService(ICalloutCardsService calloutCardsService, INavigationItemService navigationItemService)
        {
            _htmlStringUtilities = new HtmlStringUtilities();
            _calloutCardsService = calloutCardsService ?? throw new ArgumentNullException(nameof(calloutCardsService));
            _navigationItemService = navigationItemService ?? throw new ArgumentNullException(nameof(navigationItemService));
        }

        public ScienceLatestUpdatesSectionViewModel GetLatestUpdates(ScienceLandingPage model)
        {
            var viewModel = new ScienceLatestUpdatesSectionViewModel()
            {
                Pages = GetLatestUpdatedPages(model),
                AToZPageLink = _navigationItemService.GetViewModel(model.AToZpageLink)
            };

            return viewModel;
        }

        private IEnumerable<ScienceLatestUpdatedPageItemViewModel> GetLatestUpdatedPages(ScienceLandingPage model)
        {
            var viewModels = new List<ScienceLatestUpdatedPageItemViewModel>();
            var pages = model.Children<ScienceDetailsPage>();

            if (ExistenceUtility.IsNullOrEmpty(pages))
            {
                return viewModels;
            }

            List<ScienceDetailsPage> latestPages;

            if (model.LatestUpdates != null && model.LatestUpdates.Any())
            {
                //use overridden latest updates
                var selectedPages = model.LatestUpdates.OfType<ScienceDetailsPage>();
                switch (model.LatestUpdates.Count())
                {
                    case 2:
                        latestPages = selectedPages.OrderByDescending(x => x.UpdateDate)
                            .Take(2)
                            .ToList();

                        var onePage = pages.OrderByDescending(x => x.UpdateDate)
                            .Take(1)
                            .ToList();
                        latestPages.AddRange(onePage);
                        break;
                    case 1:
                        latestPages = selectedPages.OrderByDescending(x => x.UpdateDate)
                            .Take(1)
                            .ToList();

                        var twoPages = pages.OrderByDescending(x => x.UpdateDate)
                            .Take(2)
                            .ToList();
                        latestPages.AddRange(twoPages);
                        break;
                    default:
                        latestPages = selectedPages.OrderByDescending(x => x.UpdateDate)
                            .Take(NumberOfLatestUpdatesItems)
                            .ToList();
                        break;
                }
            }
            else
            {
                //use auto top 3
                latestPages = pages.OrderByDescending(x => x.UpdateDate)
                    .Take(NumberOfLatestUpdatesItems)
                    .ToList();
            }
            
            foreach (var page in latestPages)
            {
                var viewModel = new ScienceLatestUpdatedPageItemViewModel()
                {
                    Title = page.GetHeadline(),
                    ReadMoreLink = _navigationItemService.GetViewModel(page)
                };

                if (ExistenceUtility.IsNullOrWhiteSpace(page.Preamble) == false)
                {

                    viewModel.Content = _htmlStringUtilities.Truncate(page.Preamble.ToString(), LatestUpdateItemContentLength, true, false);
                }

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public IEnumerable<ResourcesCollectionViewModel> GetResourcesCollections(ScienceLandingPage model)
        {
            var viewModels = new List<ResourcesCollectionViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(model.ResourcesCollections))
            {
                return viewModels;
            }

            foreach (var collection in model.ResourcesCollections)
            {
                if (collection.Content is ResourcesCollectionSchema resourceItem)
                {
                    var viewModel = new ResourcesCollectionViewModel()
                    {
                        Title = resourceItem.Title,
                        Resources = _calloutCardsService.GetCalloutCards(resourceItem.Resources),
                        ReadMoreLink = GetViewModelReadMoreLink(resourceItem)
                    };

                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
        }

        private NavigationItemViewModel GetViewModelReadMoreLink(ResourcesCollectionSchema collection)
        {
            if (collection.MainCategoryPage == null)
            {
                return null;
            }

            var mainCategoryPageContent = collection.MainCategoryPage.FirstChildOfType("scienceCategoryPage") as ScienceCategoryPage;

            if (mainCategoryPageContent == null)
            {
                return null;
            }

            return new NavigationItemViewModel()
            {
                Text = string.Format("All References Resources in {0}", mainCategoryPageContent.GetHeadline()),
                Url = mainCategoryPageContent.Url(),
            };


        }
    }
}
