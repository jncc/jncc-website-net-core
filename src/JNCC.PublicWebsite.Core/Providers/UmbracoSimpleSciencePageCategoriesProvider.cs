using JNCC.PublicWebsite.Core.Interfaces.Providers;
using JNCC.PublicWebsite.Core.Models;
using Umbraco.Cms.Core.Cache;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal sealed class UmbracoSimpleSciencePageCategoriesProvider : UmbracoPagesProvider<SimpleScienceDetailsPage, ScienceCategoryPage>, ISimpleSciencePageCategoriesProvider
    {
        public UmbracoSimpleSciencePageCategoriesProvider(AppCaches appCaches) : base(appCaches)
        {
        }

        public IEnumerable<ScienceCategoryPage> GetCategories(SimpleScienceDetailsPage content)
        {
            var pages = GetContentPages(content);
            if (pages is IEnumerable<ScienceCategoryPage> categoryPages)
            {
                return categoryPages;
            }

            return null;
        }

        protected override IEnumerable<ScienceCategoryPage> GetContentPagesForCaching(SimpleScienceDetailsPage root)
        {
            var pages = root.Categories?.Select(x => x as ScienceCategoryPage);

            return pages;
        }
    }
}
