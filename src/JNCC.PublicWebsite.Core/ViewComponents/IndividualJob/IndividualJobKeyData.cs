using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "IndividualJobKeyData")]
    public class IndividualJobKeyDataViewComponent : ViewComponent
    {
        private readonly IIndividualJobPageService _individualJobPageService;

        public IndividualJobKeyDataViewComponent(IIndividualJobPageService individualJobPageService)
        {
            _individualJobPageService = individualJobPageService ?? throw new ArgumentNullException(nameof(individualJobPageService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var viewModel = _individualJobPageService.GetKeyData(model as IndividualJobPage);

            return View("~/Views/Partials/IndividualJob/KeyData.cshtml", viewModel);
        }
    }
}