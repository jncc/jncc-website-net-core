using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceIFrameImageRichTextSubSectionViewModel: ScienceIFrameSubSectionViewModel, IScienceIFrameImageRichTextSectionViewModel
    {
        public ImageViewModel Image { get; set; }
        public string ImagePosition { get; set; } = string.Empty;
        public IHtmlEncodedString Content { get; set; }
    }
}
