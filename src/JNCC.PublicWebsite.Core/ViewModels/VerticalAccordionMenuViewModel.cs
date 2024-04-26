using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class VerticalAccordionMenuViewModel
    {
        public string AccordionTitle { get; set; }

        public IEnumerable<IPublishedContent> Items { get; set; }
    }
}
