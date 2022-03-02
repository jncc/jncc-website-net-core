using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Specialized;
using System.Linq;
using Umbraco.Cms.Core.Models;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface INewsAndInsightsLandingService
    {
        NameValueCollection ConvertFiltersToNameValueCollection(NewsAndInsightsLandingFilteringModel filteringModel);
        int GetItemsPerPage(NewsAndInsightsLandingPage parent);
        IOrderedEnumerable<ArticlePage> GetOrderedChildren(NewsAndInsightsLandingPage parent, NewsAndInsightsLandingFilteringModel filteringModel);
        ArticleListingViewModel ToViewModel(ArticlePage content);
        Paged​Result<ArticleListingViewModel> GetViewModels(NewsAndInsightsLandingPage parent, NewsAndInsightsLandingFilteringModel filteringModel);
    }
}
