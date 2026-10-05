using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ArticlesSliderSectionViewModel : ArticlesSectionViewModel, IArticlesSliderSectionViewModel
    {
        public bool ShowBackground { get; set; }
        public bool ShowTimelineArrows { get; set; }
        public IHtmlEncodedString Content { get; set; }
        public IEnumerable<ScienceSliderSchemaViewModel> SliderItems { get; set; }
    }
}
