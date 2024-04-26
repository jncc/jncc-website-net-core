using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface INewsAndInsightsLandingFilteringService
    {
        NewsAndInsightsLandingFilteringViewModel GetFilteringViewModel(NewsAndInsightsLandingFilteringModel filteringModel, IPublishedContent root);

    }
}
