namespace JNCC.PublicWebsite.Core.ViewModels
{
    public class ArticlesContentViewModel
    {
        public IEnumerable<ArticlesSectionViewModel> SubSections { get; set; }

        public bool ShowTableOfContents { get; set; }
    }
}