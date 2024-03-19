using System.Web;
using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceCategoryRichTextSectionViewModel : ScienceCategorySectionViewModel, IScienceCategoryRichTextSectionViewModel
    {
        public IHtmlEncodedString Content { get; set; }
    }
}
