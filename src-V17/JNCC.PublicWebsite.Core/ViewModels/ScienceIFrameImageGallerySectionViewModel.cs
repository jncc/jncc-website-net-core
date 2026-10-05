namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceIFrameImageGallerySectionViewModel : ScienceIFrameSectionViewModel, IScienceIFrameImageGallerySectionViewModel
    {
        public string Key { get; set; }
        public IEnumerable<ImageGalleryItemViewModel> Images { get; set; }
    }
}
