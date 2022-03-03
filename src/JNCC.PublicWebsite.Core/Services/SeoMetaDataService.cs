using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class SeoMetaDataService : ISeoMetaDataService
    {
        public SeoMetaDataViewModel GetViewModel(IPublishedContent content)
        {
            return new SeoMetaDataViewModel
            {
                Title = content.Name
            };
        }


        public SeoMetaDataViewModel GetViewModelFromSeoSettings(ISeoComposition composition)
        {
            var settings = composition.SeoSettings;
            var Title = "";

            if (settings == null)
            {
                return GetViewModel(composition);
            }

            if (string.IsNullOrWhiteSpace(settings.Title) == false)
            {
                Title = settings.Title;
            }
            else if (composition.Name.ToLower() == "home")
            {
                Title = "JNCC - Adviser to Government on Nature Conservation";
            }
            else
            {
                Title = composition.Name + " | JNCC - Adviser to Government on Nature Conservation";
            }

            return new SeoMetaDataViewModel
            {
                Title = Title,
                Description = settings.Description,
                Keywords = settings.Keywords,
                NoIndex = composition.NoIndex,
            };
        }
    }
}
