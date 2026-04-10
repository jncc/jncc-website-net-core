using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public class FeaturedResourceViewModel
    {
        public string Headline { get; set; } = "";

        public IHtmlEncodedString Content { get; set; } = new HtmlEncodedString("");

        public NavigationItemViewModel ReadMoreButton { get; set; } = new NavigationItemViewModel();
    }
}
