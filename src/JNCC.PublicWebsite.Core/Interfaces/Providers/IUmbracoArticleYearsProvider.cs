using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.Interfaces.Providers
{
    public interface IUmbracoArticleYearsProvider
    {
        IEnumerable<int> GetAllByRoot(IPublishedContent root);
        IEnumerable<int> GetAllByRootDescending(IPublishedContent root);
    }
}
