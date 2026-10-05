namespace JNCC.PublicWebsite.Core.ViewModels
{
    public class ScienceSliderSchemaViewModel
    {
        public ImageViewModel Image { get; set; }
        public NavigationItemViewModel ImageLink { get; set; }
        public bool ImagePosition { get; set; }
        public string Heading { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public bool ImageTextSection { get; set; }
    }
}
