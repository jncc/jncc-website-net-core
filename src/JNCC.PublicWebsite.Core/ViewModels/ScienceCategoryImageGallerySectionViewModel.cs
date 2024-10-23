namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceCategoryImageGallerySectionViewModel : ScienceCategorySectionViewModel, IScienceCategoryImageGallerySectionViewModel
    {
        public string Key { get; set; }
        public IEnumerable<ImageGalleryItemViewModel> Images { get; set; }
    }
}
