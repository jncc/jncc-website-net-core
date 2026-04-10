using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Cms.Core.Strings;
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
                ResourceTitle = content.ResourcesTitle ?? "",
                Resources = GetResourcesItems(content),
            };
            return viewModel;
        }

        public IEnumerable<ResourceItemViewModel> GetResourcesItems(HomePage content)
        {
            if (content?.ResourcesItems is null)
            {
                return new List<ResourceItemViewModel>();
            }

            var viewModels = new List<ResourceItemViewModel>();

            foreach (var item in content.ResourcesItems)
            {
                if (item.Content is not ResourceItemSchema resourceItem)
                {
                    continue;
                }

                var viewModel = new ResourceItemViewModel()
                {
                    Content = resourceItem.Content ?? new HtmlEncodedString(""),
                    ReadMoreButton = _navigationItemService.GetViewModel(resourceItem.Link)
                };

                if (resourceItem.Image is not null)
                {
                    viewModel.ImageUrl = resourceItem.Image.GetCropUrl("ResourcesListing") ?? "";
                }

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        public CarouselViewModel? GetCarouselViewModel(IPageHeroCarouselComposition content)
        {
            if (ExistenceUtility.IsNullOrEmpty(content.HeroImages))
            {
                return null;
            }

            var viewModel = new CarouselViewModel()
            {
                Headline = content.Headline ?? "",
                Text = content.HeroContent ?? new HtmlEncodedString(""),
            };

            List<ImageViewModel> images = new List<ImageViewModel>();

            foreach (var image in content.HeroImages!)
            {
                if (image?.Content is Image heroImage)
                {
                    ImageViewModel imageModel = new ImageViewModel()
                    {
                        Url = heroImage.Url(),
                        AlternativeText = heroImage.AltText ?? "",
                        TitleText = heroImage.TitleText ?? "",
                    };
                    images.Add(imageModel);
                }
            }
            viewModel.Images = images;

            return viewModel;
        }
    }
}
