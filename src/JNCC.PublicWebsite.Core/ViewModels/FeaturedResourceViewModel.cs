using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public class FeaturedResourceViewModel
    {
        public string Headline { get; set; }

        public IHtmlEncodedString Content { get; set; }

        public NavigationItemViewModel ReadMoreButton { get; set; }
    }
}
