using Umbraco.Cms.Core.Models.PublishedContent;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IBreadcrumbsService
    {
        BreadcrumbsViewModel RenderBreadcrumbs(IPublishedContent model);
    }
}
