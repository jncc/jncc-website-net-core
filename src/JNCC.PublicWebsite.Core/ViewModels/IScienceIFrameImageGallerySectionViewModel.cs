namespace JNCC.PublicWebsite.Core.ViewModels
{
    public interface IScienceIFrameImageGallerySectionViewModel
    {
        string Key { get; set; }
        IEnumerable<ImageGalleryItemViewModel> Images { get; set; }
    }
}