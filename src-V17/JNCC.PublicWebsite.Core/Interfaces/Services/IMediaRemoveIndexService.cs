using Umbraco.Cms.Core.Models;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IMediaRemoveIndexService
    {
        void Handle(IMedia publishedEntity, string fileName);
    }
}
