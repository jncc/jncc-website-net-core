using System;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "IFrameSourceUrl")]
    public class IFrameSourceUrlViewComponent : ViewComponent
    {
        private readonly IIFramePageService _iFramePageService;

        public IFrameSourceUrlViewComponent(IIFramePageService iFramePageService)
        {
            _iFramePageService = iFramePageService ?? throw new ArgumentNullException(nameof(iFramePageService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var viewModel = _iFramePageService.GetSourceUrl(model as IFramePage, Request.GetDisplayUrl());

            return View("~/Views/Partials/IFrame/SourceUrl.cshtml", viewModel);
        }
    }
}