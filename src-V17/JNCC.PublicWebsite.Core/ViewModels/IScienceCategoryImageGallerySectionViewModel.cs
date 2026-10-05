namespace JNCC.PublicWebsite.Core.ViewModels
{
    public interface IScienceCategoryImageGallerySectionViewModel
    {
        string Key { get; set; }
        IEnumerable<ImageGalleryItemViewModel> Images { get; set; }
    }
}