using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.Interfaces.Providers;
using Umbraco.Cms.Core.Cache;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Providers
{
    internal sealed class UmbracoScienceDetailsPageProvider : UmbracoPagesProvider<ScienceCategoryPage, ScienceDetailsPage>, IScienceDetailsPageProvider
    {

        public UmbracoScienceDetailsPageProvider(AppCaches appCaches) : base(appCaches)
        { }

        public IEnumerable<ScienceDetailsPage> GetByCategory(ScienceCategoryPage scienceCategoryPage)
        {
            var pages = GetContentPages(scienceCategoryPage);
            if(pages is IEnumerable<ScienceDetailsPage> categoryPages)
            {
                return pages;
            }

            return null;
        }

        protected override IEnumerable<ScienceDetailsPage> GetContentPagesForCaching(ScienceCategoryPage root)
        {
            ScienceLandingPage scienceLanding = root.Parent<ScienceLandingPage>();

            var allScienceDetailPages = scienceLanding.Children()?
                                                   .OfType<ScienceDetailsPage>()
                                                   .ToArray();

            return allScienceDetailPages.Where(x => ExistenceUtility.IsNullOrEmpty(x.Categories) == false && x.Categories.Any(y => y.Id == root.Id));
        }
    }
}
