using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{

        [ViewComponent(Name = "VerticalAccordionMenu")]
        public class VerticalAccordionMenuViewComponent : ViewComponent
        {
            private readonly IVerticalAccordionMenuService _verticalAccordionMenuService;
            public VerticalAccordionMenuViewComponent(IVerticalAccordionMenuService verticalAccordionMenuService)
            {
                _verticalAccordionMenuService = verticalAccordionMenuService ?? throw new ArgumentNullException(nameof(verticalAccordionMenuService));
            }

            public IViewComponentResult Invoke(IPublishedContent model)
            {
                var scienceLandingPage = model as ScienceLandingPage;
                var viewModel = _verticalAccordionMenuService.GetAccordionItems(scienceLandingPage.Accordion);
                return View("~/Views/Partials/Accordion.cshtml", viewModel);
         
            }
        }
    
}
