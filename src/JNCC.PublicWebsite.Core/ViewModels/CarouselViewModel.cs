using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class CarouselViewModel
    {
        public string Headline { get; set; } = "";
        public IHtmlEncodedString Text { get; set; } = new HtmlEncodedString("");
        public IEnumerable<ImageViewModel> Images { get; set; } = new List<ImageViewModel>();
    }
}