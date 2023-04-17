using JNCC.PublicWebsite.Core.Models.Custom;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JNCC.PublicWebsite.Core.Interfaces.Api
{
    public interface IResourceApi
    {
        Task<List<ResourceModel>> GetAllResources();

        Task<ResourceModel> GetResource(string id);
    }
}
