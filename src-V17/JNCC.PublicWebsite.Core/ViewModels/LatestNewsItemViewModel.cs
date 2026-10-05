using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class LatestNewsItemViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string ImageAltText { get; set; } = string.Empty;
        public string ImageTitleText { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; }
        public IHtmlEncodedString Description { get; set; }
        public string Url { get; set; } = string.Empty;
        public string ArticleType { get; set; } = string.Empty;
    }
}