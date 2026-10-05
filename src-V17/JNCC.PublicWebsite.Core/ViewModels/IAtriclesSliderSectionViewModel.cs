using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public interface IArticlesSliderSectionViewModel
    {
        bool ShowBackground { get; set; }
        bool ShowTimelineArrows { get; set; }
        IHtmlEncodedString Content { get; set; }
        IEnumerable<ScienceSliderSchemaViewModel> SliderItems { get; set; }
    }
}
