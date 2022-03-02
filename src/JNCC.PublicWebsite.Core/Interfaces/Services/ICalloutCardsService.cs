using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface ICalloutCardsService
    {
        IEnumerable<CalloutCardViewModel> GetCalloutCards(IEnumerable<CalloutCardSchema> cards);
    }
}
