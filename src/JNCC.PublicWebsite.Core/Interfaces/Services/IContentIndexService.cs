using Umbraco.Cms.Core.Models;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IContentIndexService
    {
        void Handle(IContent publishedEntity);
    }
}
