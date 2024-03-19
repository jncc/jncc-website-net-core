using System.Web;
using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceDetailsRichTextSubSectionViewModel : ScienceDetailsSubSectionViewModel, IScienceDetailsRichTextSectionViewModel
    {
        public IHtmlEncodedString Content { get; set; }
    }
}