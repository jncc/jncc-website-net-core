namespace JNCC.PublicWebsite.Core.ViewModels
{
    public interface IScienceDetailsImageGallerySectionViewModel
    {
        string Key { get; set; }
        IEnumerable<ImageGalleryItemViewModel> Images { get; set; }
    }
}