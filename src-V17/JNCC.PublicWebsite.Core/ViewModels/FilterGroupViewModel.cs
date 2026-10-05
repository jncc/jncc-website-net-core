using JNCC.PublicWebsite.Core.Utilities;

namespace JNCC.PublicWebsite.Core.ViewModels
{
    public sealed class FilterGroupViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public IReadOnlyDictionary<string, bool> Values { get; set; }
        public bool HasValues { get { return ExistenceUtility.IsNullOrEmpty(Values) == false; } }
    }
}
