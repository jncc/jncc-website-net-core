namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class CategorisedPagesViewModel
    {
        public string Heading { get; set; }
        public IReadOnlyDictionary<char, IEnumerable<NavigationItemViewModel>> Pages { get; set; }
    }
}
