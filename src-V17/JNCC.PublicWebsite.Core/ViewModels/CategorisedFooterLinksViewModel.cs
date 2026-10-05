namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class CategorisedFooterLinksViewModel
    {
        public string Heading { get; set; } = string.Empty;
        public IEnumerable<NavigationItemViewModel> Links { get; set; }
    }
}
