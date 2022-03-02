using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public interface IScienceDetailsSliderSectionViewModel
    {
        bool ShowBackground { get; set; }
        bool ShowTimelineArrows { get; set; }
        IHtmlEncodedString Content { get; set; }
        IEnumerable<ScienceSliderSchemaViewModel> SliderItems { get; set; }
    }
}
