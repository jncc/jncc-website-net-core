using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.Utilities;
using JNCC.PublicWebsite.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<CalloutCardViewModel> GetCalloutCards(IEnumerable<CalloutCardSchema> cards)
        {
            var viewModels = new List<CalloutCardViewModel>();

            if (ExistenceUtility.IsNullOrEmpty(cards))
            {
                return viewModels;
            }

            foreach (var card in cards)
            {
                var viewModel = new CalloutCardViewModel()
                {
                    Title = card.Title,
                    Content = card.Content,
                    ReadMoreButton = _navigationItemService.GetViewModel(card.ReadMoreButton)
                };

                if (card.Image?.First() is ContentImageSchema imageSchema)
                {
                    if(imageSchema.Image?.Content is Image image)
                    {
                        viewModel.Image = new ImageViewModel()
                        {
                            Url = image.Url(),
                            AlternativeText = image.AltText.IsNullOrWhiteSpace() ? image.AltText : image.Name,
                            TitleText = image.TitleText,
                        };
                    }
                };

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}
