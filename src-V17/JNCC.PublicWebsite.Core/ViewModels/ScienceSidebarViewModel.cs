using JNCC.PublicWebsite.Core.Utilities;

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

        public new string CurrentPageUrl { get; set; } = string.Empty;
        public string CurrentPageContentTypeAlias { get; set; } = string.Empty;
    }
}