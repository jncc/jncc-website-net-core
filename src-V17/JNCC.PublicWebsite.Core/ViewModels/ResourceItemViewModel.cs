using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ResourceItemViewModel
    {
        public string ImageUrl { get; set; } = "";
        public IHtmlEncodedString Content { get; set; } = new HtmlEncodedString("");
        public NavigationItemViewModel ReadMoreButton { get; set; } = new NavigationItemViewModel();
    }
}