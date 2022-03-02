using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class QueryService : IQueryService
    {
        public NewsAndInsightsLandingFilteringModel GetFilterModel(HttpRequest request)
        {
            NewsAndInsightsLandingFilteringModel filterModel = new NewsAndInsightsLandingFilteringModel()
            {
                SearchTerm = GetSearchTerm(request),
                ArticleTypes = GetArticleTypes(request),
                Teams = GetTeams(request),
                Years = GetYears(request),
                PageNumber = GetPageNumber(request),
            };
            return filterModel; 
        }

        private string GetSearchTerm(HttpRequest request)
        {
            request.Query.TryGetValue(Constants.FilterNames.SearchTerm, out var searchTerm);
            return searchTerm;
        }

        private string[] GetArticleTypes(HttpRequest request)
        {
            request.Query.TryGetValue(Constants.FilterNames.ArticleTypes, out var articleTypes);
            return articleTypes.ToArray();
        }

        private string[] GetTeams(HttpRequest request)
        {
            request.Query.TryGetValue(Constants.FilterNames.Teams, out var teams);
            return teams.ToArray();
        }

        private int[] GetYears(HttpRequest request)
        {
            request.Query.TryGetValue(Constants.FilterNames.Years, out var years);
            return years.Select(x => int.Parse(x)).ToArray();
        }
        private int GetPageNumber(HttpRequest request)
        {
            request.Query.TryGetValue(Constants.FilterNames.PageNumber, out var pageNumber);
            int.TryParse(pageNumber, out var page);
            return page;
        }
    }
}
