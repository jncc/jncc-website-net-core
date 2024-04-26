using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceCategoryImageCodeSubSectionViewModel: ScienceCategorySubSectionViewModel, IScienceCategoryImageCodeSectionViewModel
    {
        public string ImageCode { get; set; }
        public string ImagePosition { get; set; }
        public IHtmlEncodedString Content { get; set; }
    }
}
