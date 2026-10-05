using JNCC.PublicWebsite.Core.Models;

namespace JNCC.PublicWebsite.Core.Interfaces.Providers
{
    internal interface ISimpleSciencePageCategoriesProvider
    {
        IEnumerable<ScienceCategoryPage> GetCategories(SimpleScienceDetailsPage content);
    }
}