using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceIFrameImageRichTextSectionViewModel : ScienceIFrameSectionViewModel, IScienceIFrameImageRichTextSectionViewModel
    {
        public ImageViewModel Image { get; set; }
        public string ImagePosition { get; set; }
        public IHtmlEncodedString Content { get; set; }
    }
}
