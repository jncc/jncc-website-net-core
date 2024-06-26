﻿using JNCC.PublicWebsite.Core.Utilities;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class MainNavigationItemViewModel : NavigationItemViewModel
    {
        public bool IsActive { get; set; }
        public IEnumerable<MainNavigationItemViewModel> Children { get; set; }
        public bool HasChildren
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(Children) == false;
            }
        }
    }
}
