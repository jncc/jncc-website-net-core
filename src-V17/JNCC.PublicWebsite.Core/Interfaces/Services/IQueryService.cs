using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Http;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IQueryService
    {
        NewsAndInsightsLandingFilteringModel GetFilterModel(HttpRequest request);
    }
}
