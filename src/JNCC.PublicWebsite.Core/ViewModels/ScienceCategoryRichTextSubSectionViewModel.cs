using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceCategoryRichTextSubSectionViewModel : ScienceCategorySubSectionViewModel, IScienceCategoryRichTextSectionViewModel
    {
        public IHtmlEncodedString Content { get; set; }
    }
}