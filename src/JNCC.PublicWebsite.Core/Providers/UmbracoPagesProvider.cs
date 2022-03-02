using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal abstract class UmbracoPagesProvider<TRoot, TContent> where TRoot : class, IPublishedContent
                                                                  where TContent : class, IPublishedContent
    {
        private readonly AppCaches _appCaches;
        public UmbracoPagesProvider(AppCaches appCaches)
        {
            _appCaches = appCaches ?? throw new ArgumentNullException(nameof(appCaches));
        }

        public virtual IEnumerable<TContent> GetContentPages(TRoot root)
        {
            var cacheKey = string.Format("ContentPages_For_Root_{0}", root.Id);

            return _appCaches.RuntimeCache.GetCacheItem<IEnumerable<TContent>>(cacheKey, () => GetContentPagesForCaching(root));
        }

        protected virtual IEnumerable<TContent> GetContentPagesForCaching(TRoot root)
        {
            return root.Children<TContent>();
        }
    }

    internal abstract class UmbracoPagesProvider<TContent> : UmbracoPagesProvider<IPublishedContent, TContent> where TContent : class, IPublishedContent
    {
        public UmbracoPagesProvider(AppCaches cacheProvider) : base(cacheProvider)
        {
        }
    }
}