using JNCC.PublicWebsite.Core.Utilities;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public abstract class BasicSidebarViewModel
    {
        public string CurrentPageUrl { get; set; }
        public NavigationItemViewModel PrimaryCallToActionButton { get; set; }
        public bool HasPrimaryCallToActionButton
        {
            get
            {
                return PrimaryCallToActionButton != null;
            }
        }

        public IEnumerable<NavigationItemViewModel> DataHubLinks { get; set; }
        public bool HasDataHubLinks
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(DataHubLinks) == false;
            }
        }

        public IEnumerable<NavigationItemViewModel> SeeAlsoLinks { get; set; }
        public bool HasSeeAlsoLinks
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(SeeAlsoLinks) == false;
            }
        }

        public IEnumerable<NavigationItemViewModel> ElsewhereOnOurWebsiteLinks { get; set; }
        public bool HasElsewhereOnOurWebsiteLinks
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(ElsewhereOnOurWebsiteLinks) == false;
            }
        }

        public IEnumerable<NavigationItemViewModel> OtherWebsitesLinks { get; set; }
        public bool HasOtherWebsitesLinks
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(OtherWebsitesLinks) == false;
            }
        }
        public string SiblingLinksTitle { get; set; }
        public IEnumerable<NavigationItemViewModel> SiblingPageLinks { get; set; }
        public bool HasSiblingPageLinks
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(SiblingPageLinks) == false;
            }
        }
        public string ChildPageLinksTitle { get; set; }
        public IEnumerable<NavigationItemViewModel> ChildPageLinks { get; set; }
        public bool HasChildPageLinks
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(ChildPageLinks) == false;
            }
        }
        public IEnumerable<NavigationItemViewModel> ParentLink { get; set; }
        public bool HasParentLink
        {
            get
            {
                return ExistenceUtility.IsNullOrEmpty(ParentLink) == false;
            }
        }
    }
}