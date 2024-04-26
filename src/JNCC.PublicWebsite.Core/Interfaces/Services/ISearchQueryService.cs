using JNCC.PublicWebsite.Core.Models;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    internal interface ISearchQueryService
    {
        SearchModel Query(string query, int size, int start, string siteFilter);

        Task<SearchModel> QueryAsync(string query, int size, int start, string siteFilter);
    }
}