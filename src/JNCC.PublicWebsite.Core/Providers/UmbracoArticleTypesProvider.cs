using JNCC.PublicWebsite.Core.Interfaces.Providers;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal sealed class UmbracoArticleTypesProvider : UmbracoPagesProvider<ArticlePage>, IUmbracoArticleTypesProvider
    {
        private readonly AppCaches _appCaches;
        public UmbracoArticleTypesProvider(AppCaches appCaches) : base(appCaches)
        {
            _appCaches = appCaches ?? throw new ArgumentNullException(nameof(appCaches));
        }
        public IEnumerable<string> GetAllByRoot(IPublishedContent root)
        {
            var articlePages = GetContentPages(root);

            if (ExistenceUtility.IsNullOrEmpty(articlePages))
            {
                return Enumerable.Empty<string>();
            }

            return articlePages.Select(x => x.ArticleType)
                               .Distinct()
                               .OrderBy(x => x);
        }
    }
}
