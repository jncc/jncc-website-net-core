namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceDetailsImageGallerySubSectionViewModel : ScienceDetailsSubSectionViewModel, IScienceDetailsImageGallerySectionViewModel
    {
        public string Key { get; set; }
        public IEnumerable<ImageGalleryItemViewModel> Images { get; set; }
    }
}