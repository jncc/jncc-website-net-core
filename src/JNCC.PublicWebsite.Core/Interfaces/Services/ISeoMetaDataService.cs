using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface ISeoMetaDataService
    {
        public SeoMetaDataViewModel GetViewModel(IPublishedContent content);
        public SeoMetaDataViewModel GetViewModelFromSeoSettings(ISeoComposition composition);
    }
}
