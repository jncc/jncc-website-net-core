﻿using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class ScienceIFrameRichTextSubSectionViewModel : ScienceIFrameSubSectionViewModel, IScienceIFrameRichTextSectionViewModel
    {
        public IHtmlEncodedString Content { get; set; }
    }
}