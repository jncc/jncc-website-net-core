using System.Web;
using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ArticlesRichTextSectionViewModel : ArticlesSectionViewModel, IArticlesRichTextSectionViewModel
    {
        public IHtmlEncodedString Content { get; set; }
    }
}
