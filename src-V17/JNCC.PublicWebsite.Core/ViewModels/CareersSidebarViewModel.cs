using JNCC.PublicWebsite.Core.Utilities;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class CareersSidebarViewModel : BasicSidebarViewModel
    {
        public IEnumerable<JobItemViewModel> LatestJobs { get; set; }
        public bool HasLatestJobs
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(LatestJobs) == false;
            }
        }
        public string AlsoInLinksTitle { get; set; }
    }
}
