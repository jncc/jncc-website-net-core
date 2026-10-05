using Umbraco.Cms.Core.Models;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public class JobItemViewModel
    {
        public string Url { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime ClosingDate { get; set; }
    }
}
