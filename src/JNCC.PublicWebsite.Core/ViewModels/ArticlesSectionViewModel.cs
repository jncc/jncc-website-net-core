namespace JNCC.PublicWebsite.Core.ViewModels
{
    public abstract class ArticlesSectionViewModel : ArticlesSectionViewModelBase
    {
        public IEnumerable<ArticlesSubSectionViewModel> SubSections { get; set; }
    }
}