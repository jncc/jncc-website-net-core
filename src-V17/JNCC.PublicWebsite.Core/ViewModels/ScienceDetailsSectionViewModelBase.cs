namespace JNCC.PublicWebsite.Core.ViewModels
{
    public abstract class ScienceDetailsSectionViewModelBase
    {
        public string HtmlId { get; set; } = string.Empty;
        public string Headline { get; set; } = string.Empty;
        public string PartialViewName { get; set; } = string.Empty;
        public bool HideHeadline { get; set; }
    }
}