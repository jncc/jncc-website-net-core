using JNCC.PublicWebsite.Core.Models;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    internal interface IDataHubRawQueryService
    {
        SearchModel GetByRawQuery(string rawQuery, int numberOfItems);
    }
}
