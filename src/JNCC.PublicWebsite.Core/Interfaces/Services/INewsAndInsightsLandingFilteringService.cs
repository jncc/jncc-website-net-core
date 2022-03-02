using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Specialized;
using System.Linq;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface INewsAndInsightsLandingFilteringService
    {
        NewsAndInsightsLandingFilteringViewModel GetFilteringViewModel(NewsAndInsightsLandingFilteringModel filteringModel, IPublishedContent root);

    }
}
