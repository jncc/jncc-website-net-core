using JNCC.PublicWebsite.Core.Models.Custom;

namespace JNCC.PublicWebsite.Core.Interfaces.Api
{
    public interface IResourceApi
    {
        Task<List<ResourceSitemap>> GetSitemapResources();

        Task<ResourceModel> GetResource(string id);
    }
}
