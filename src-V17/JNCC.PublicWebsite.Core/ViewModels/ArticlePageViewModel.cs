using JNCC.PublicWebsite.Core.Models;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ArticlePageViewModel
    {
        public string LandingPageUrl { get; set; } = string.Empty;
        public ArticlePage ArticlePage { get; set; }
        public BreadcrumbsViewModel Breadcrumb { get; set; }
        public PageHeroViewModel PageHero { get; set; }
        public NoPageHeroHeadlineViewModel NoPageHeroHeadline { get; set; }
    }
}
