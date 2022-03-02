using JNCC.PublicWebsite.Core.Models;
using JNCC.PublicWebsite.Core.ViewModels;
using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Interfaces.Services
{
    public interface IIndividualJobPageService
    {
        public IEnumerable<AccordionItemViewModel> GetTabbedContent(IndividualJobPage model);
        public IReadOnlyDictionary<string, string> GetKeyData(IndividualJobPage model);
    }
}
