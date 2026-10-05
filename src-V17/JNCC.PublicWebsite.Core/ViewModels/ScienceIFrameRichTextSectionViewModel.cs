using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceIFrameRichTextSectionViewModel : ScienceIFrameSectionViewModel, IScienceIFrameRichTextSectionViewModel
    {
        public IHtmlEncodedString Content { get; set; }
    }
}
