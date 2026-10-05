using JNCC.PublicWebsite.Core.Interfaces.Providers;
using JNCC.PublicWebsite.Core.Models;
using Umbraco.Cms.Core.Cache;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal sealed class UmbracoSciencePageCategoriesProvider : UmbracoPagesProvider<ScienceDetailsPage, ScienceCategoryPage>, ISciencePageCategoriesProvider
    {
        public UmbracoSciencePageCategoriesProvider(AppCaches appCaches) : base(appCaches)
        {
        }

        public IEnumerable<ScienceCategoryPage> GetCategories(ScienceDetailsPage content)
        {
            var pages = GetContentPages(content);
            if (pages is IEnumerable<ScienceCategoryPage> categoryPages)
            {
                return categoryPages;
            }

            return null;
        }

        protected override IEnumerable<ScienceCategoryPage> GetContentPagesForCaching(ScienceDetailsPage root)
        {
            var pages = root.Categories?.Select(x => x as ScienceCategoryPage);

            return pages;
        }
    }
}
