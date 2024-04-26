using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.Interfaces.Providers
{
    public interface IUmbracoArticleTypesProvider
    {
        IEnumerable<string> GetAllByRoot(IPublishedContent root);
    }
}
