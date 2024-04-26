namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ArticlesImageGallerySectionViewModel : ArticlesSectionViewModel, IArticlesImageGallerySectionViewModel
    {
        public IEnumerable<ImageGalleryItemViewModel> Images { get; set; }
    }
}
