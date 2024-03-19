using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ResourcesViewModel
    {
        public IEnumerable<ResourceItemViewModel> Resources { get; set; }
        public string ResourceTitle { get; set; }
    }
}