using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Models.Custom;
using JNCC.PublicWebsite.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.ViewComponents
{
    [ViewComponent(Name = "SeoMetadata")]
    public class SeoMetadataViewComponent : ViewComponent
    {
        private readonly ISeoMetaDataService _seoMetaDataService;

        public SeoMetadataViewComponent(ISeoMetaDataService seoMetaDataService)
        {
            _seoMetaDataService = seoMetaDataService ?? throw new ArgumentNullException(nameof(seoMetaDataService));
        }
        public IViewComponentResult Invoke(IPublishedContent model)
        {
            SeoMetaDataViewModel viewModel;

            if (model is ISeoComposition)
            {
                viewModel = _seoMetaDataService.GetViewModelFromSeoSettings(model as ISeoComposition);
            }
            else
            {
                viewModel = _seoMetaDataService.GetViewModel(model);
            }

            //special case for virtual resource pages
            if (model is VirtualResourceModel virtualResourcePage)
            {
                viewModel.Keywords = "";
                viewModel.Title = $"{virtualResourcePage.ResourceToDisplay.MetaData.Title} | JNCC Resource Hub";
                viewModel.Description = virtualResourcePage.ResourceToDisplay.MetaData.Abstract.Truncate(200);
            }

            return View("~/Views/Partials/SeoMetaData.cshtml", viewModel);
        }
    }
}

