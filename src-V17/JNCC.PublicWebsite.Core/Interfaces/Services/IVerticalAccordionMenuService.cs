using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Cms.Core.Models.Blocks;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IVerticalAccordionMenuService
    {
        IEnumerable<VerticalAccordionMenuViewModel> GetAccordionItems(BlockListModel accordion);
    }
}
