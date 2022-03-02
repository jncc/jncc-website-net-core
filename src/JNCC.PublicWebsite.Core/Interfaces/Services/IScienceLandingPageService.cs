using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IScienceLandingPageService
    {
        ScienceLatestUpdatesSectionViewModel GetLatestUpdates(ScienceLandingPage model);
        IEnumerable<ResourcesCollectionViewModel> GetResourcesCollections(ScienceLandingPage model);
    }
}
