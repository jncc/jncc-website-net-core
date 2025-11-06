using Umbraco.Cms.Core.Models;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IMediaIndexService
    {
        void Handle(IMedia publishedEntity, string fileName);
    }
}
