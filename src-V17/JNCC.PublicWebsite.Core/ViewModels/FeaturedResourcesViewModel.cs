namespace JNCC.PublicWebsite.Core.ViewModels
{
    public class FeaturedResourcesViewModel
    {
        public string Title { get; set; } = string.Empty;

        public IEnumerable<FeaturedResourceViewModel> FeaturedResources { get; set; } = new List<FeaturedResourceViewModel>();

        public int ItemsPerColumn
        {
            get
            {
                return FeaturedResources.Any() ? (int)Math.Round((decimal)FeaturedResources.Count() / 2, MidpointRounding.AwayFromZero) : 0;
            }
        }
    }
}
