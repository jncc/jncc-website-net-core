using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.Models.Custom;

namespace JNCC.PublicWebsite.Core.Services
{
    public class PageHeroService : IPageHeroService
    {
        public PageHeroViewModel RenderPageHero(IPublishedContent model)
        {
            return GetViewModel(model);
        }

        public NoPageHeroHeadlineViewModel RenderNoPageHeroHeadline(IPublishedContent model)
        {
            if (HasPageHero(model))
            {
                return null;
            }

            return GetNoPageHeroHeadlineViewModel(model);
        }

        public PageHeroViewModel GetViewModel(IPublishedContent publishedContent)
        {
            if (publishedContent is IPageHeroComposition)
            {
                return GetPageHeroViewModel(publishedContent as IPageHeroComposition);
            }

            return null;
        }

        private PageHeroViewModel GetPageHeroViewModel(IPageHeroComposition pageHeroComposition)
        {
            if (pageHeroComposition.HeroImage == null)
            {
                return null;
            }

            var headline = string.IsNullOrWhiteSpace(pageHeroComposition.Headline) == false ?
                             pageHeroComposition.Headline
                           : pageHeroComposition.Name;

            return new PageHeroViewModel()
            {
                Headline = headline,
                ImageUrl = pageHeroComposition.HeroImage.GetCropUrl(),
                ImageCopyrightText = pageHeroComposition.HeroImage.Value<string>("titleText"),
                ShowResourceSearch = pageHeroComposition is ResourcesPage
            };
        }

        public bool HasPageHero(IPublishedContent currentPage)
        {
            var isPageHeroComposition = currentPage is IPageHeroComposition;
            var isPageHeroCarouselComposition = currentPage is IPageHeroCarouselComposition;
           
            if (currentPage is VirtualResourceModel)
            {
                return false;
            }

            if (isPageHeroComposition == false && isPageHeroCarouselComposition == false)
            {
                return false;
            }

            if (isPageHeroComposition)
            {
                return (currentPage as IPageHeroComposition).HasPageHeroImage();
            }

            if (isPageHeroCarouselComposition)
            {
                return ExistenceUtility.IsNullOrEmpty((currentPage as IPageHeroCarouselComposition).HeroImages) == false;
            }

            return false;
        }

        public NoPageHeroHeadlineViewModel GetNoPageHeroHeadlineViewModel(IPublishedContent currentPage)
        {
            if (currentPage is IPageHeroComposition)
            {
                var headline = (currentPage as IPageHeroComposition).Headline;

                if (string.IsNullOrWhiteSpace(headline) == false)
                {
                    return new NoPageHeroHeadlineViewModel()
                    {
                        Headline = headline
                    };
                }
            }

            return new NoPageHeroHeadlineViewModel()
            {
                Headline = currentPage.Name
            };
        }
    }
}
