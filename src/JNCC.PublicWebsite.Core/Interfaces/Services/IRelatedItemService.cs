using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IRelatedItemService
    {
        IEnumerable<RelatedItemViewModel> RenderRelatedItems(IPublishedContent model);

        IEnumerable<RelatedItemViewModel> GetViewModels(IRelatedItemsComposition composition, HomePage homePage);
    }
}
