using Umbraco.Cms.Core.Models.PublishedContent;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IPageHeroService
    {
        PageHeroViewModel RenderPageHero(IPublishedContent model);

        NoPageHeroHeadlineViewModel RenderNoPageHeroHeadline(IPublishedContent model);
        PageHeroViewModel GetViewModel(IPublishedContent publishedContent);
        bool HasPageHero(IPublishedContent currentPage);
        NoPageHeroHeadlineViewModel GetNoPageHeroHeadlineViewModel(IPublishedContent currentPage);
    }
}
