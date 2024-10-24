namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceCategoryImageGallerySubSectionViewModel : ScienceCategorySubSectionViewModel, IScienceCategoryImageGallerySectionViewModel
    {   
        public string Key { get; set; }
        public IEnumerable<ImageGalleryItemViewModel> Images { get; set; }
    }
}