using JNCC.PublicWebsite.Core.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents.Header
{

    [ViewComponent(Name = "EditPageBar")]
    public class EditPageBarViewComponent : ViewComponent
    {
        private readonly IConfiguration _configuration;

        public EditPageBarViewComponent(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(EditPageBarViewComponent));
        }

        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var enableEditPageBar = _configuration.GetValue<bool>(AppSettings.EnableEditPageBar);
            var editPageUrl = string.Empty;

            if (enableEditPageBar == true)
            {
                editPageUrl = string.Format("/umbraco/#/content/content/edit/{0}", model.Id);
            }

            return View("~/Views/Partials/Header/EditPageBar.cshtml", editPageUrl);
        }
    }
}
