using JNCC.PublicWebsite.Core.Models;

namespace JNCC.PublicWebsite.Core.Interfaces.Providers
{
    internal interface IScienceDetailsPageProvider
    {
        IEnumerable<ScienceDetailsPage> GetByCategory(ScienceCategoryPage scienceCategoryPage);
    }
}
