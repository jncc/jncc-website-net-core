using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class MainNavigationViewModel
    {
        public IEnumerable<MainNavigationItemViewModel> Items { get; set; }
        public bool HasPageHero { get; set; }
        public IPublishedContent Content { get; set; }
    }
}
