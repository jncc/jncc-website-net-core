using Microsoft.AspNetCore.Mvc;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "Search")]
    public class SearchViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Partials/Header/Search.cshtml");
        }
    }
}