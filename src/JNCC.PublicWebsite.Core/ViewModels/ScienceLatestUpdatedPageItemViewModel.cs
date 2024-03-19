using Microsoft.AspNetCore.Html;
using System.Web;
using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceLatestUpdatedPageItemViewModel
    {
        public string Title { get; set; }
        public IHtmlEncodedString Content { get; set; }
        public NavigationItemViewModel ReadMoreLink { get; set; }
    }
}