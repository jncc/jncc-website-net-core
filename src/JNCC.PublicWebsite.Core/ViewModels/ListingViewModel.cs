﻿using System.Collections.Specialized;
using Umbraco.Cms.Core.Models;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public abstract class ListingViewModel<T>
    {
        public PagedResult<T> Items { get; set; }
        public NameValueCollection Filters { get; set; }
    }
}