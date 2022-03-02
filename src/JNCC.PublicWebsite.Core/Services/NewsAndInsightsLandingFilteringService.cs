using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Interfaces.Providers;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class NewsAndInsightsLandingFilteringService : FilteringService<NewsAndInsightsLandingFilteringModel, NewsAndInsightsLandingFilteringViewModel, IPublishedContent>, INewsAndInsightsLandingFilteringService
    {
        private readonly IUmbracoArticlePageTagsProvider _umbracoArticlePageTagsProvider;
        private readonly IUmbracoArticleTypesProvider _umbracoArticleTypesProvider;
        private readonly IUmbracoArticleYearsProvider _umbracoArticleYearsProvider;

        public NewsAndInsightsLandingFilteringService(IUmbracoArticlePageTagsProvider umbracoArticlePageTagsProvider, IUmbracoArticleTypesProvider umbracoArticleTypesProvider, 
            IUmbracoArticleYearsProvider umbracoArticleYearsProvider) 
            : base()
        {
            _umbracoArticlePageTagsProvider = umbracoArticlePageTagsProvider;
            _umbracoArticleTypesProvider = umbracoArticleTypesProvider;
            _umbracoArticleYearsProvider = umbracoArticleYearsProvider;
        }

        public override NewsAndInsightsLandingFilteringViewModel GetFilteringViewModel(NewsAndInsightsLandingFilteringModel filteringModel, IPublishedContent root)
        {
            
            var allTeams = _umbracoArticlePageTagsProvider.GetTagsByRoot(root, TagGroups.Teams);

            var viewModel = new NewsAndInsightsLandingFilteringViewModel()
            {
                Teams = new FilterGroupViewModel()
                {
                    Title = "Team",
                    Group = FilterNames.Teams,
                    Values = GetFilters(allTeams, filteringModel.Teams)
                },
                ArticleTypes = new FilterGroupViewModel()
                {
                    Title = "Article Type",
                    Group = FilterNames.ArticleTypes,
                    Values = GetFilters(_umbracoArticleTypesProvider.GetAllByRoot(root), filteringModel.ArticleTypes)
                },
                Years = new FilterGroupViewModel()
                {
                    Title = "Year",
                    Group = FilterNames.Years,
                    Values = GetFilters(_umbracoArticleYearsProvider.GetAllByRoot(root), filteringModel.Years)
                }
            };

            if (string.IsNullOrWhiteSpace(filteringModel.SearchTerm) == false)
            {
                viewModel.SearchTerm = filteringModel.SearchTerm;
            }

            return viewModel;
        }
    }
}
