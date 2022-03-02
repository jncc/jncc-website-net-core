using System;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "RelatedItems")]
    public class RelatedItemsViewComponent : ViewComponent
    {
        private readonly IRelatedItemService _relatedItemsService;

        public RelatedItemsViewComponent(IRelatedItemService relatedItemService)
        {
            _relatedItemsService = relatedItemService ?? throw new ArgumentNullException(nameof(relatedItemService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            var viewModel = _relatedItemsService.RenderRelatedItems(model);

            return View("~/Views/Partials/RelatedItems.cshtml", viewModel);
        }
    }
}