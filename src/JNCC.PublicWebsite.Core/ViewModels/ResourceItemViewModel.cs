using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ResourceItemViewModel
    {
        public string ImageUrl { get; set; }
        public IHtmlEncodedString Content { get; set; }
        public NavigationItemViewModel ReadMoreButton { get; set; }
    }
}