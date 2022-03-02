using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using JNCC.PublicWebsite.Core.Extensions;
using JNCC.PublicWebsite.Core.Utilities;

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

            //TODO: Decide whether to remove this if not using Articulate
            //if (publishedContent is ArticulatePost)
            //{
            //    return GetBlogPostViewModel(publishedContent as ArticulatePost);
            //}
            //else
            //{
            //    var blogRoot = publishedContent.AncestorOrSelf<ArticulateModel>();

            //    if (blogRoot != null)
            //    {
            //        return GetBlogViewModel(blogRoot);
            //    }
            //}

            return null;
        }

        //TODO: Decide whether to remove this if not using Articulate
        //private PageHeroViewModel GetBlogViewModel(ArticulateModel articulate)
        //{
        //    if (articulate.BlogBanner == null)
        //    {
        //        return null;
        //    }

        //    return new PageHeroViewModel()
        //    {
        //        Headline = articulate.Name,
        //        ImageUrl = articulate.GetCropUrl("blogBanner", "wide"),
        //        //ImageCopyrightText = ?,
        //    };
        //}

        //private PageHeroViewModel GetBlogPostViewModel(ArticulatePost articulatePost)
        //{
        //    if (articulatePost.PostImage == null)
        //    {
        //        return null;
        //    }

        //    return new PageHeroViewModel()
        //    {
        //        Headline = articulatePost.Name,
        //        ImageUrl = articulatePost.GetCropUrl("postImage", "wide")
        //        //ImageCopyrightText = ?;
        //    };
        //}

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
                ImageUrl = pageHeroComposition.HeroImage.Url(),
                ImageCopyrightText = pageHeroComposition.HeroImage.Value<string>("titleText"),
            };
        }

        public bool HasPageHero(IPublishedContent currentPage)
        {
            var isPageHeroComposition = currentPage is IPageHeroComposition;
            var isPageHeroCarouselComposition = currentPage is IPageHeroCarouselComposition;
           
            //TODO: Decide whether to remove this if not using Articulate
            //var isArticulatePost = currentPage is ArticulatePost;
            //var blogRoot = currentPage.AncestorOrSelf<ArticulateModel>();
            //var hasBlogRoot = blogRoot != null;

            if (isPageHeroComposition == false && isPageHeroCarouselComposition == false)
            {
                return false;
            }

            if (isPageHeroComposition)
            {
                return (currentPage as IPageHeroComposition).HasPageHeroImage();
            }

            //if (isArticulatePost)
            //{
            //    var post = (currentPage as ArticulatePost);
            //    var postImage = post.PostImage;

            //    if (postImage == null)
            //    {
            //        return false;
            //    }

            //    return postImage.HasCrop("wide");
            //}
            //else if (hasBlogRoot)
            //{
            //    var blogBanner = blogRoot.BlogBanner;

            //    if (blogBanner == null)
            //    {
            //        return false;
            //    }

            //    return blogBanner.HasCrop("wide");
            //}

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
