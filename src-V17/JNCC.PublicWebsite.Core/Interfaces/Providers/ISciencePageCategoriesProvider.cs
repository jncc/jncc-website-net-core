using JNCC.PublicWebsite.Core.Models;

namespace JNCC.PublicWebsite.Core.Interfaces.Providers
{
    internal interface ISciencePageCategoriesProvider
    {
        IEnumerable<ScienceCategoryPage> GetCategories(ScienceDetailsPage content);
    }
}