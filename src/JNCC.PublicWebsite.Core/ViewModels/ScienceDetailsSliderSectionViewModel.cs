﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceDetailsSliderSectionViewModel : ScienceDetailsSectionViewModel, IScienceDetailsSliderSectionViewModel
    {
        public bool ShowBackground { get; set; }
        public bool ShowTimelineArrows { get; set; }
        public IHtmlEncodedString Content { get; set; }
        public IEnumerable<ScienceSliderSchemaViewModel> SliderItems { get; set; }
    }
}
