using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Strings;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class CalloutCardsService : ICalloutCardsService
    {
        private readonly INavigationItemService _navigationItemService;

        public CalloutCardsService(INavigationItemService navigationItemService)
        {
            _navigationItemService = navigationItemService ?? throw new ArgumentNullException(nameof(navigationItemService));
        }

        public IEnumerable<CalloutCardViewModel> GetCalloutCards(BlockListModel? cards)
        {
            if (cards is null)
            {
                return new List<CalloutCardViewModel>();
            }

            var viewModels = new List<CalloutCardViewModel>();

            foreach (var card in cards)
            {
                if (card?.Content is not CalloutCardSchema calloutCard)
                {
                    continue;
                }
                var viewModel = new CalloutCardViewModel()
                {
                    Title = calloutCard.Title ?? "",
                    Content = calloutCard.Content ?? new HtmlEncodedString(""),
                    ReadMoreButton = _navigationItemService.GetViewModel<NavigationItemViewModel>(calloutCard.ReadMoreButton)
                };

                if (calloutCard.Image?.FirstOrDefault()?.Content is ContentImageSchema imageSchema)
                {
                    if (imageSchema.Image?.Content is Image image)
                    {
                        viewModel.Image = new ImageViewModel()
                        {
                            Url = image.Url(),
                            AlternativeText = image.AltText?.Trim() ?? image.Name ?? "",
                            TitleText = image.TitleText ?? "",
                        };
                    }
                }
                ;

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}
