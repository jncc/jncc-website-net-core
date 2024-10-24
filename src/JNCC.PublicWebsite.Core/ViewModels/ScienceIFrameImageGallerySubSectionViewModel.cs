namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceIFrameImageGallerySubSectionViewModel : ScienceIFrameSubSectionViewModel, IScienceIFrameImageGallerySectionViewModel
    {
        public string Key { get; set; }
        public IEnumerable<ImageGalleryItemViewModel> Images { get; set; }
    }
}