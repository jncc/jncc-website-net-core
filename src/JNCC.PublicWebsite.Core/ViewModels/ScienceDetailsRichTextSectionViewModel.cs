using System.Web;
using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceDetailsRichTextSectionViewModel : ScienceDetailsSectionViewModel, IScienceDetailsRichTextSectionViewModel
    {
        public IHtmlEncodedString Content { get; set; }
    }
}
