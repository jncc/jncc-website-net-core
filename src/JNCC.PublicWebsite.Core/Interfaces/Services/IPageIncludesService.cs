using Umbraco.Cms.Core.Models.PublishedContent;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IPageIncludesService
    {
        PageAttributesViewModel GetPageAttributesViewModel(IPageSpecificIncludesComposition pageSpecificIncludesComposition);

        string GetHeadIncludes(IGlobalIncludesComposition globalIncludes, IPageSpecificIncludesComposition pageSpecificIncludes);

        string GetBodyIncludes(IGlobalIncludesComposition globalIncludes, IPageSpecificIncludesComposition pageSpecificIncludes); 
    }
}