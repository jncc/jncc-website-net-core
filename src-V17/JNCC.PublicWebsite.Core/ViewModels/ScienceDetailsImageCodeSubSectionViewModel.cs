using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceDetailsImageCodeSubSectionViewModel : ScienceDetailsSubSectionViewModel, IScienceDetailsImageCodeSectionViewModel
    {
        public string ImageCode { get; set; }
        public string ImagePosition { get; set; }
        public IHtmlEncodedString Content { get; set; }
    }
}
