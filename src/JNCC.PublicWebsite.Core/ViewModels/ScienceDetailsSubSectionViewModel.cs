using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public abstract class ScienceDetailsSubSectionViewModel : ScienceDetailsSectionViewModelBase
    {
        public IEnumerable<ScienceDetailsSubSectionViewModel> SubSections { get; set; }
    }
}