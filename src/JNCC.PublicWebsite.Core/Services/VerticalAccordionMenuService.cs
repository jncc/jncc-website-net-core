using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class VerticalAccordionMenuService : IVerticalAccordionMenuService
    {


        public IEnumerable<VerticalAccordionMenuViewModel> GetAccordionItems(BlockListModel accordion)
        {
            var viewModels = new List<VerticalAccordionMenuViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(accordion))
            {
                return viewModels;
            }

            foreach (var item in accordion)
            {
                VerticalAccordionMenuViewModel viewModel = null;

                switch (item.Content)
                {
                    case VerticalAccordionMenu vertcialAccordion:
                        viewModel = CreateVerticalAccordionMenu(vertcialAccordion);
                        break;
                }

                if(viewModel != null)
                {
                    viewModels.Add(viewModel);
                }
            }

            return viewModels;
        }

        public VerticalAccordionMenuViewModel CreateVerticalAccordionMenu(VerticalAccordionMenu vertcialAccordion)
        {
            var model = new VerticalAccordionMenuViewModel();
            model.AccordionTitle = vertcialAccordion.AccordionTitle;
            model.Items = vertcialAccordion.Items;

            return model;
        }
    }
}
