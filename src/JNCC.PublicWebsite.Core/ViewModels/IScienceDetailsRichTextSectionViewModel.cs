using System.Web;
using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public interface IScienceDetailsRichTextSectionViewModel
    {
        IHtmlEncodedString Content { get; set; }
    }
}
