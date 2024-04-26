using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceIFrameImageRichTextSubSectionViewModel: ScienceIFrameSubSectionViewModel, IScienceIFrameImageRichTextSectionViewModel
    {
        public ImageViewModel Image { get; set; }
        public string ImagePosition { get; set; }
        public IHtmlEncodedString Content { get; set; }
    }
}
