using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using Microsoft.AspNetCore.Http;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IPageIncludesService
    {
        PageAttributesViewModel GetPageAttributesViewModel(IPageSpecificIncludesComposition pageSpecificIncludesComposition);

        string GetHeadIncludes(IGlobalIncludesComposition globalIncludes, IPageSpecificIncludesComposition pageSpecificIncludes, HttpContext context);

        string GetBodyIncludes(IGlobalIncludesComposition globalIncludes, IPageSpecificIncludesComposition pageSpecificIncludes, HttpContext context); 
    }
}