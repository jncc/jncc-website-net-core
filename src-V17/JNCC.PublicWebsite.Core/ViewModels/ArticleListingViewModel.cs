using Microsoft.AspNetCore.Html;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ArticleListingViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string ImageAltText { get; set; } = string.Empty;
        public string ImageTitleText { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; }
        public IHtmlContent Description { get; set; }
        public string Url { get; set; } = string.Empty;
        public string ArticleType { get; set; } = string.Empty;
    }
}
