using JNCC.PublicWebsite.Core.Interfaces.Providers;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal sealed class UmbracoArticleYearsProvider : UmbracoPagesProvider<ArticlePage>, IUmbracoArticleYearsProvider
    {
        private readonly AppCaches _appCaches;
        public UmbracoArticleYearsProvider(AppCaches appCaches) : base(appCaches)
        {
            _appCaches = appCaches ?? throw new ArgumentNullException(nameof(appCaches));
        }

        private IEnumerable<int> GetArticleYears(IPublishedContent root)
        {
            var articlePages = GetContentPages(root);

            if (ExistenceUtility.IsNullOrEmpty(articlePages))
            {
                return Enumerable.Empty<int>();
            }

            return articlePages.Select(x => x.PublishDate.Year).Distinct();
        }

        public IEnumerable<int> GetAllByRoot(IPublishedContent root)
        {
            return GetArticleYears(root).OrderBy(x => x);
        }

        public IEnumerable<int> GetAllByRootDescending(IPublishedContent root)
        {
            return GetArticleYears(root).OrderByDescending(x => x);
        }
    }
}
