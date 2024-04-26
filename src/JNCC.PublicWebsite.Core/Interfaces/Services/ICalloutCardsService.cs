using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface ICalloutCardsService
    {
        IEnumerable<CalloutCardViewModel> GetCalloutCards(IEnumerable<CalloutCardSchema> cards);
    }
}
