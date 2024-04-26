using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "Sidebar")]
    public class SidebarViewComponent : ViewComponent
    {
        private readonly ISidebarService _sidebarService;

        public SidebarViewComponent(ISidebarService sidebarService)
        {
            _sidebarService = sidebarService ?? throw new ArgumentNullException(nameof(sidebarService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            if (model is ISidebarComposition == false)
            {
                return null;
            }

            var viewModel = _sidebarService.GetViewModel(model as ISidebarComposition);

            viewModel.CurrentPageUrl = model.Url();

            return View("~/Views/Partials/Sidebar.cshtml", viewModel);
        }
    }
}