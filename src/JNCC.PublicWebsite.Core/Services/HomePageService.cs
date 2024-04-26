using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class HomePageService : IHomePageService
    {
        private readonly INavigationItemService _navigationItemService;

        public HomePageService(INavigationItemService navigationItemService)
        {
            _navigationItemService = navigationItemService ?? throw new ArgumentNullException(nameof(navigationItemService));
        }

        public ResourcesViewModel GetResources(HomePage content)
        {
            ResourcesViewModel viewModel = new ResourcesViewModel()
            {
                ResourceTitle = content.ResourcesTitle,
                Resources = GetResourcesItems(content),
            };
            return viewModel;
        }

        public IEnumerable<ResourceItemViewModel> GetResourcesItems(HomePage content)
        {
            var viewModels = new List<ResourceItemViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(content.ResourcesItems))
            {
                return viewModels;
            }

            foreach (var item in content.ResourcesItems)
            {
                var viewModel = new ResourceItemViewModel()
                {
                    Content = item.Content,
                    ReadMoreButton = _navigationItemService.GetViewModel(item.Link)
                };

                if (item.Image != null)
                {
                    viewModel.ImageUrl = item.Image.Url();
                }

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public CarouselViewModel GetCarouselViewModel(IPageHeroCarouselComposition content)
        {
            if (ExistenceUtility.IsNullOrEmpty(content.HeroImages))
            {
                return null;
            }

            var viewModel = new CarouselViewModel();

            viewModel.Headline = content.Headline;
            viewModel.Text = content.HeroContent;

            List<ImageViewModel> images = new List<ImageViewModel>();

            foreach (var image in content.HeroImages)
            {
                if(image?.Content is Image heroImage)
                {
                    ImageViewModel imageModel = new ImageViewModel()
                    {
                        Url = heroImage.Url(),
                        AlternativeText = heroImage.AltText,
                        TitleText = heroImage.TitleText,
                    };
                    images.Add(imageModel);
                }
            }
            viewModel.Images = images;

            return viewModel;
        }
    }
}
