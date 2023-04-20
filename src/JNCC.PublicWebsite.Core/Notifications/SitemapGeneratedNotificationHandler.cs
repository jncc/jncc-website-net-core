using SEOChecker.Core.Notifications;
using System;
using Umbraco.Cms.Core.Events;

namespace JNCC.PublicWebsite.Core.Notifications
{
    public class SitemapGeneratedNotificationHandler : INotificationHandler<XmlSitemapGeneratedNotification>
    {
        public void Handle(XmlSitemapGeneratedNotification notification)
        {
            //TODO: WE NEED TO PIPE IN THE VIRTUAL PAGE SITEMAP ENTRIES HERE!!!

            //example of how to add new sitemap items
            notification.Entries.Add(new SEOChecker.Core.XmlSitemap.SitemapEntry() { SitemapPrio = "0.5", Url = "http://www.tim.com", UpdateDate = DateTime.Now });
            notification.Entries.Add(new SEOChecker.Core.XmlSitemap.SitemapEntry() { SitemapPrio = "0.5", Url = "http://www.tim2.com", UpdateDate = DateTime.Now.AddDays(-1) });
        }
    }
}
