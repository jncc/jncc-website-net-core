using Umbraco.Cms.Core.Models;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IContentRemoveIndexService
    {
        void Handle(IContent publishedEntity);
    }
}
