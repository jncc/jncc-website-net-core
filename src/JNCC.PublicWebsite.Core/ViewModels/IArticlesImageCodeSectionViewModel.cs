﻿using System.Collections.Generic;
using System.Web;
using Umbraco.Cms.Core.Strings;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public interface IArticlesImageCodeSectionViewModel
    {
        string ImageCode { get; set; }

        string ImagePosition { get; set; }

        IHtmlEncodedString Content { get; set; }
    }
}
