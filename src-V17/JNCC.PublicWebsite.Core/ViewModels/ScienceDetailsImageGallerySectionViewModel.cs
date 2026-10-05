namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceDetailsImageGallerySectionViewModel : ScienceDetailsSectionViewModel, IScienceDetailsImageGallerySectionViewModel
    {
        public string Key { get; set; }
        public IEnumerable<ImageGalleryItemViewModel> Images { get; set; }
    }
}
