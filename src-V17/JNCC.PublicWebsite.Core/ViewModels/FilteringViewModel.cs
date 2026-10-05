namespace JNCC.PublicWebsite.Core.ViewModels
{
    public abstract class FilteringViewModel
    {
        public string SearchTerm { get; set; } = string.Empty;
        public FilterGroupViewModel Teams { get; set; }
    }
}