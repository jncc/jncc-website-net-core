using System.Collections.Generic;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface INavigationItemService
    {
        IEnumerable<NavigationItemViewModel> GetViewModels(IEnumerable<Link> links);
        IEnumerable<T> GetViewModels<T>(IEnumerable<Link> links) where T : NavigationItemViewModel, new();
        NavigationItemViewModel GetViewModel(Link link);
        T GetViewModel<T>(Link link) where T : NavigationItemViewModel, new();
        T GetViewModel<T>(IPublishedContent content) where T : NavigationItemViewModel, new();
        IEnumerable<T> GetViewModels<T>(IEnumerable<IPublishedContent> contents) where T : NavigationItemViewModel, new();
        NavigationItemViewModel GetViewModel(IPublishedContent content);
        IEnumerable<NavigationItemViewModel> GetViewModels(IEnumerable<IPublishedContent> contents);
    }
}
