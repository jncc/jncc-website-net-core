using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ArticlesRichTextSubSectionViewModel : ArticlesSubSectionViewModel, IArticlesRichTextSectionViewModel
    {
        public IHtmlEncodedString Content { get; set; }
    }
}