using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class ScienceAtoZPageService : IScienceAtoZPageService
    {
        public IReadOnlyDictionary<char, IEnumerable<NavigationItemViewModel>> GetCategorisedPages(ScienceAtoZpage model)
        {
            var scienceLandingPage = model.Parent<ScienceLandingPage>();

            if (scienceLandingPage == null)
            {
                return null;
            }

            var categorisablePages = scienceLandingPage.Children<IScienceCategorisablePage>();
            if (ExistenceUtility.IsNullOrEmpty(categorisablePages))
            {
                return null;
            }

            return categorisablePages.CategorisePages();
        }
    }
}
