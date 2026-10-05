namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ResourcesCollectionViewModel
    {
        public string Title { get; set; } = string.Empty;
        public IEnumerable<CalloutCardViewModel> Resources { get; set; }
        public NavigationItemViewModel ReadMoreLink { get; set; }
    }
}