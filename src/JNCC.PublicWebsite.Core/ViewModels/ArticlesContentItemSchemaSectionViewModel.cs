using JNCC.PublicWebsite.Core.Models;
using System.Web;
using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ArticlesContentVideoSchemaSectionViewModel : ArticlesSectionViewModel
    {
        public ContentVideoSchema Content { get; set; }
    }
}
