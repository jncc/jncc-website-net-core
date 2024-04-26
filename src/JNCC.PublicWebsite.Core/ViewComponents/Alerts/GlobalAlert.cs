using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "GlobalAlert")]
    public class GlobalAlertViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var viewModel = model.Root() as HomePage;

            return View("~/Views/Partials/GlobalAlert.cshtml", viewModel.GlobalAlertContent);
        }
    }
}

