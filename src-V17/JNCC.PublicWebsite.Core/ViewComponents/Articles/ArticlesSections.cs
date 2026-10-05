using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "ArticlesSections")]
    public class ArticlesSectionsViewComponent : ViewComponent
    {
        private readonly IArticlesPageService _articlesPageService;
        public ArticlesSectionsViewComponent(IArticlesPageService articlesPageService)
        {
            _articlesPageService = articlesPageService ?? throw new ArgumentNullException(nameof(articlesPageService));
        }

        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var article = model as ArticlePage;

            ArticlesContentViewModel viewModel = new ArticlesContentViewModel()
            {
                SubSections = _articlesPageService.GetSectionViewModels(article.MainContentBlocks),
                ShowTableOfContents = article.ShowTableOfContents,
            };

            return View("~/Views/Partials/Articles/Sections.cshtml", viewModel);
        }
    }
}