using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceIFrameImageCodeSectionViewModel : ScienceIFrameSectionViewModel, IScienceIFrameImageCodeSectionViewModel
    {
        public string ImageCode { get; set; }
        public string ImagePosition { get; set; }
        public IHtmlEncodedString Content { get; set; }
    }
}
