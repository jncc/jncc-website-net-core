namespace JNCC.PublicWebsite.Core.ViewModels
{
    public abstract class ArticlesSubSectionViewModel : ArticlesSectionViewModelBase
    {
        public IEnumerable<ArticlesSubSectionViewModel> SubSections { get; set; }
    }
}