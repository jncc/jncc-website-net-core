namespace JNCC.PublicWebsite.Core.ViewModels
{
    public class FeaturedResourcesViewModel
    {
        public string Title { get; set; }

        public IEnumerable<FeaturedResourceViewModel> FeaturedResources { get; set; } = new List<FeaturedResourceViewModel>();

        public int ItemsPerColumn
        {
            get
            {
                return FeaturedResources.Count() > 0 ? (int)Math.Round((decimal)FeaturedResources.Count() / 2, MidpointRounding.AwayFromZero) : 0;
            }
        }
    }
}
