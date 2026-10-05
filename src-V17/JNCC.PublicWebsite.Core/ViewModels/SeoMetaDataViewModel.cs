namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class SeoMetaDataViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Keywords { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool NoIndex { get; set; }
    }
}
