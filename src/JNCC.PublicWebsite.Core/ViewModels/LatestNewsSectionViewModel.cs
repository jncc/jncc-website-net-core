namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class LatestNewsSectionViewModel
    {
        public IEnumerable<LatestNewsItemViewModel> LatestNews { get; set; }
        public SocialFeedViewModel SocialFeed { get; set; }
    }
}