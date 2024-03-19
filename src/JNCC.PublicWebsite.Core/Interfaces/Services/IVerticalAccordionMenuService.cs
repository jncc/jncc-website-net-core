using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Cms.Core.Models.Blocks;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IVerticalAccordionMenuService
    {
        IEnumerable<VerticalAccordionMenuViewModel> GetAccordionItems(BlockListModel accordion);
    }
}
