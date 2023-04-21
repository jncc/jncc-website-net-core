using JNCC.PublicWebsite.Core.Interfaces.Api;
using SEOChecker.Core.Notifications;
using SEOChecker.Core.XmlSitemap;
using System.Linq;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Notifications
{
    public class SitemapGeneratedNotificationHandler : INotificationHandler<XmlSitemapGeneratedNotification>
    {
        private readonly IResourceApi _resourceApi;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public SitemapGeneratedNotificationHandler(IResourceApi resourceApi, IUmbracoContextAccessor umbracoContextAccessor)
        {
            _resourceApi = resourceApi;
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public void Handle(XmlSitemapGeneratedNotification notification)
        {
            var virtualPages = _resourceApi.GetSitemapResources().GetAwaiter().GetResult();

            if (virtualPages?.Any() == true)
            {
                _umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext);

                var resourcePage = umbracoContext.Content.GetSingleByXPath("root/homePage/resourcesPage [@isDoc][1]");

                if (resourcePage != null)
                {
                    var resourceUrl = resourcePage.Url(mode: UrlMode.Absolute);

                    notification.Entries.AddRange(virtualPages.Select(a => new SitemapEntry() { ChangeFrequency = "monthly", SitemapPrio = "0.5", UpdateDate = a.LastUpdated, Url = $"{resourceUrl}{a.Id}" }));
                }
            }
        }
    }
}
