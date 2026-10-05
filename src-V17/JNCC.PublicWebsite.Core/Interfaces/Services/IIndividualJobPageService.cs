using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IIndividualJobPageService
    {
        public IEnumerable<AccordionItemViewModel> GetTabbedContent(IndividualJobPage model);
        public IReadOnlyDictionary<string, string> GetKeyData(IndividualJobPage model);
    }
}
