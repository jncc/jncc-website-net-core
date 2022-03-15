using JNCC.PublicWebsite.Core.Utilities;
using System.Collections.Generic;
using JNCC.PublicWebsite.Core.Models;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceSidebarViewModel : BasicSidebarViewModel
    {
        public IEnumerable<MainNavigationItemViewModel> Categories { get; set; }

        public IEnumerable<MainNavigationItemViewModel> RelatedCategories { get; set; }

        public bool HasCategories
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(Categories) == false;
            }
        }

        public string CurrentPageUrl { get; set; }
    }
}