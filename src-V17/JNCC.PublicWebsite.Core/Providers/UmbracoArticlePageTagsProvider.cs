using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Interfaces.Providers;
using JNCC.PublicWebsite.Core.Models;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal sealed class UmbracoArticlePageTagsProvider : UmbracoPagesProvider<ArticlePage>, 
        IUmbracoArticlePageTagsProvider
    {
        private readonly AppCaches _appCaches;
        public UmbracoArticlePageTagsProvider(AppCaches appCaches): base(appCaches)
        {
            _appCaches = appCaches ?? throw new ArgumentNullException(nameof(appCaches));
        }

        public IEnumerable<string> GetTagsByRoot(IPublishedContent root, string tagGroup)
        {
            var articlePages = GetContentPages(root);

            switch (tagGroup)
            {
                case TagGroups.Themes:
                    return articlePages.SelectMany(x => x.ArticleThemes)
                                       .Distinct()
                                       .OrderBy(x => x);
                default:
                    return Enumerable.Empty<string>();
            }
        }
    }
}
