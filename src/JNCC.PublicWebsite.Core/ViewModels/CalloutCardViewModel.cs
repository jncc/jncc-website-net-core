using System.Web;
using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class CalloutCardViewModel
    {
        public string Title { get; set; }
        public IHtmlEncodedString Content { get; set; }
        public NavigationItemViewModel ReadMoreButton { get; set; }
        public ImageViewModel Image { get; set; }
    }
}