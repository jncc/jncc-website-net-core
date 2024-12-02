using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using Microsoft.AspNetCore.Html;
using System.Collections.Specialized;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Web.Common.Mvc;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class NewsAndInsightsLandingService : INewsAndInsightsLandingService
    {
        private const int ItemContentLength = 150;
        private readonly HtmlStringUtilities _htmlStringUtilities;
        public NewsAndInsightsLandingService()
        {
            _htmlStringUtilities = new HtmlStringUtilities();
        }

        public NameValueCollection ConvertFiltersToNameValueCollection(NewsAndInsightsLandingFilteringModel filteringModel)
        {
            var collection = new NameValueCollection();

            if (ExistenceUtility.IsNullOrEmpty(filteringModel.ArticleTypes) == false)
            {
                foreach (var value in filteringModel.ArticleTypes)
                {
                    collection.Add(FilterNames.ArticleTypes, value);
                }
            }

            if (ExistenceUtility.IsNullOrEmpty(filteringModel.Teams) == false)
            {
                foreach (var value in filteringModel.Teams)
                {
                    collection.Add(FilterNames.Teams, value);
                }
            }

            if (string.IsNullOrWhiteSpace(filteringModel.SearchTerm) == false)
            {
                collection.Add(FilterNames.SearchTerm, filteringModel.SearchTerm);
            }

            if (ExistenceUtility.IsNullOrEmpty(filteringModel.Years) == false)
            {
                foreach (var value in filteringModel.Years.AllToString())
                {
                    collection.Add(FilterNames.Years, value);
                }
            }

            if (ExistenceUtility.IsNullOrEmpty(filteringModel.Themes) == false)
            {
                foreach (var value in filteringModel.Themes)
                {
                    collection.Add(FilterNames.Themes, value);
                }
            }

            return collection;
        }

        public int GetItemsPerPage(NewsAndInsightsLandingPage parent)
        {
            return (int)parent.ArticlesPerPage;
        }

        public IOrderedEnumerable<ArticlePage> GetOrderedChildren(NewsAndInsightsLandingPage parent, NewsAndInsightsLandingFilteringModel filteringModel)
        {
            var allChildren = parent.Children<ArticlePage>();
            var conditions = new List<Func<ArticlePage, bool>>();

            if (ExistenceUtility.IsNullOrEmpty(filteringModel.Years) == false)
            {
                conditions.Add(x => filteringModel.Years.Contains(x.PublishDate.Year));
            }

            if (ExistenceUtility.IsNullOrEmpty(filteringModel.ArticleTypes) == false)
            {
                conditions.Add(x => filteringModel.ArticleTypes.Contains(x.ArticleType));
            }

            //if (ExistenceUtility.IsNullOrEmpty(filteringModel.Teams) == false)
            //{
            //    conditions.Add(x => filteringModel.Teams.Any(y => x.ArticleTeams.Contains(y, StringComparer.OrdinalIgnoreCase)));
            //}

            if (ExistenceUtility.IsNullOrEmpty(filteringModel.Themes) == false)
            {
                conditions.Add(x => filteringModel.Themes.Any(y => x.ArticleThemes.Contains(y, StringComparer.OrdinalIgnoreCase)));
            }

            if (string.IsNullOrEmpty(filteringModel.SearchTerm) == false)
            {
                conditions.Add(x => x.Name.InvariantContains(filteringModel.SearchTerm)
                                 || x.Headline.InvariantContains(filteringModel.SearchTerm)
                                 || (ExistenceUtility.IsNullOrWhiteSpace(x.LandingPageContent) == false && x.LandingPageContent.ToString().InvariantContains(filteringModel.SearchTerm))
                                 || (ExistenceUtility.IsNullOrWhiteSpace(x.MainContent) == false && x.MainContent.ToString().InvariantContains(filteringModel.SearchTerm))
                              );
            }

            var actualChildren = ExistenceUtility.IsNullOrEmpty(conditions) ? allChildren : allChildren.Where(x => conditions.All(y => y.Invoke(x)));

            return actualChildren.OrderByDescending(x => x.PublishDate)
                                 .ThenByFirstAvailableProperty(x => new string[] { x.Headline, x.Name });
        }

        public ArticleListingViewModel ToViewModel(ArticlePage content)
        {
            var viewModel = new ArticleListingViewModel
            {
                Title = string.IsNullOrWhiteSpace(content.Headline) ? content.Name : content.Headline,
                PublishDate = content.PublishDate,
                Description = new HtmlString(content.LandingPageContent.ToString()),
                //Description = _htmlStringUtilities.Truncate(content.LandingPageContent.ToString(), ItemContentLength, true, false),
                Url = content.Url(),
                ArticleType = content.ArticleType,
            };

            if (content.HeroImage?.Content is Image heroImage && heroImage != null)
            {
                viewModel.ImageUrl = heroImage.GetCropUrl(ImageCropAliases.ListingThumbnail2);
                viewModel.ImageAltText = heroImage.AltText.IsNullOrWhiteSpace() ? viewModel.Title : heroImage.AltText;
                viewModel.ImageTitleText = heroImage.TitleText;
            }

            return viewModel;
        }

        public Paged​Result<ArticleListingViewModel> GetViewModels(NewsAndInsightsLandingPage parent, NewsAndInsightsLandingFilteringModel filteringModel)
        {
            var children = GetOrderedChildren(parent, filteringModel);
            var numberOfItemsPerPage = GetItemsPerPage(parent);
            var pageNumber = filteringModel.PageNumber == 0 ? 1 : filteringModel.PageNumber;
            var results = new Paged​Result<ArticleListingViewModel>(children.LongCount(), pageNumber, numberOfItemsPerPage);

            var viewModels = children.Skip(results.GetSkipSize())
                                     .Take(numberOfItemsPerPage)
                                     .Select(ToViewModel);

            results.Items = viewModels;

            return results;
        }
    }
}
