using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "HeadIncludes")]
    public class HeadIncludesViewComponent : ViewComponent
    {
        private readonly IPageIncludesService _pageIncludesService;

        public HeadIncludesViewComponent(IPageIncludesService pageIncludesService)
        {
            _pageIncludesService = pageIncludesService ?? throw new ArgumentNullException(nameof(pageIncludesService));
        }

        public HtmlString Invoke(IPublishedContent model)
        {
            return new HtmlString(_pageIncludesService.GetHeadIncludes(model.Root() as IGlobalIncludesComposition, model as IPageSpecificIncludesComposition));
        }
    }
}

