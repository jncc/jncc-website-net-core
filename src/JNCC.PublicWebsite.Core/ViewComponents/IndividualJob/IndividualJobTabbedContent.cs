using System;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "IndividualJobTabbedContent")]
    public class IndividualJobTabbedContentViewComponent : ViewComponent
    {
        private readonly IIndividualJobPageService _individualJobPageService;

        public IndividualJobTabbedContentViewComponent(IIndividualJobPageService individualJobPageService)
        {
            _individualJobPageService = individualJobPageService ?? throw new ArgumentNullException(nameof(individualJobPageService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var viewModel = _individualJobPageService.GetTabbedContent(model as IndividualJobPage);

            return View("~/Views/Partials/IndividualJob/TabbedContent.cshtml", viewModel);
        }
    }
}