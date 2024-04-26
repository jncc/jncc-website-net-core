﻿using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public interface IScienceDetailsImageRichTextSectionViewModel
    {
        ImageViewModel Image { get; set; }

        string ImagePosition { get; set; }

        IHtmlEncodedString Content { get; set; }
    }
}
