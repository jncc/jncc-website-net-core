using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class CalloutCardViewModel
    {
        public string Title { get; set; } = string.Empty;
        public IHtmlEncodedString Content { get; set; } = new HtmlEncodedString("");
        public NavigationItemViewModel ReadMoreButton { get; set; } = new NavigationItemViewModel();
        public ImageViewModel? Image { get; set; }
    }
}