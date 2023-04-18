using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public class FeaturedResourcesViewModel
    {
        public string Title { get; set; }

        public IEnumerable<FeaturedResourceViewModel> FeaturedResources { get; set; } = new List<FeaturedResourceViewModel>();
    }
}
