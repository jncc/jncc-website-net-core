using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public interface IScienceDetailsImageCodeSectionViewModel
    {
        string ImageCode { get; set; }

        string ImagePosition { get; set; }

        IHtmlEncodedString Content { get; set; }
    }
}
