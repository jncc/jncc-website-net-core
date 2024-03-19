using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.Models.Custom
{
    public class VirtualResourceModel : ResourcesPage
    {
        // The ProductPage model accepts an IPublishedContent item as a constructor
        public VirtualResourceModel(IPublishedContent content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }

        public ResourceModel ResourceToDisplay { get; set; }
    }
}
