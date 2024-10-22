using Microsoft.AspNetCore.Html;
using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceLatestUpdatedPageItemViewModel
    {
        public string Title { get; set; }
        public IHtmlContent Content { get; set; }
        public NavigationItemViewModel ReadMoreLink { get; set; }
    }
}