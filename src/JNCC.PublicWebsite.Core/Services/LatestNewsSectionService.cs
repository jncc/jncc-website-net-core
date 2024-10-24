using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class LatestNewsSectionService : ILatestNewsSectionService
    {
        private const int NumberOfLatestNewsItems = 3;

        public LatestNewsSectionViewModel GetViewModel(HomePage model)
        {
            if (model == null)
            {
                return new LatestNewsSectionViewModel
                {
                    LatestNews = Enumerable.Empty<LatestNewsItemViewModel>(),
                    SocialFeed = new SocialFeedViewModel()
                };
            }

            return new LatestNewsSectionViewModel
            {
                LatestNews = GetLatestNewsItems(model),
                SocialFeed = GetSocialFeed(model)
            };
        }

        private SocialFeedViewModel GetSocialFeed(HomePage content)
        {
            var viewModel = new SocialFeedViewModel();

            if (content.ShowSocialFeed == false)
            {
                return viewModel;
            }

            viewModel.TwitterFeedUrl = content.TwitterFeedUrl;
            viewModel.NumberOfTweets = (int)content.NumberOfTweets;

            return viewModel;
        }

        private IEnumerable<LatestNewsItemViewModel> GetLatestNewsItems(HomePage content)
        {
            var viewModels = new List<LatestNewsItemViewModel>();

            if (content.ShowLatestNews == false)
            {
                return viewModels;
            }

            var latestNewsItems = content
                .FirstChild<NewsAndInsightsLandingPage>()?
                .Children()?
                .OfType<ArticlePage>()
                .OrderByDescending(x => x.PublishDate)
                .Take(NumberOfLatestNewsItems);

            if (ExistenceUtility.IsNullOrEmpty(latestNewsItems))
            {
                return viewModels;
            }

            foreach (var newsItem in latestNewsItems)
            {
                var viewModel = new LatestNewsItemViewModel
                {
                    Title = string.IsNullOrWhiteSpace(newsItem.Headline) ? newsItem.Name : newsItem.Headline,
                    PublishDate = newsItem.PublishDate,
                    Description = newsItem.LandingPageContent,
                    Url = newsItem.Url(),
                    ArticleType = newsItem.ArticleType
                };

                if (newsItem.HeroImage?.Content is Image heroImage)
                {
                    viewModel.ImageUrl = heroImage.GetCropUrl(ImageCropAliases.ListingThumbnail);
                    viewModel.ImageAltText = heroImage.AltText.IsNullOrWhiteSpace() ? newsItem.Headline : heroImage.AltText;
                    viewModel.ImageTitleText = heroImage.TitleText;
                }

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}
