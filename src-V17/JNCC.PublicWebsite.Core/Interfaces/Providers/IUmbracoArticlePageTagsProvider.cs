using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.Interfaces.Providers
{
    public interface IUmbracoArticlePageTagsProvider
    {
        IEnumerable<string> GetTagsByRoot(IPublishedContent root, string tagGroup);
    }
}
