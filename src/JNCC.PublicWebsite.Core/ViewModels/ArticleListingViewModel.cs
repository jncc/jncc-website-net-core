using Microsoft.AspNetCore.Html;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ArticleListingViewModel
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAltText { get; set; }
        public string ImageTitleText { get; set; }
        public DateTime PublishDate { get; set; }
        public IHtmlContent Description { get; set; }
        public string Url { get; set; }
        public string ArticleType { get; set; }
    }
}
