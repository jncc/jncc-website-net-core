using JNCC.PublicWebsite.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace JNCC.PublicWebsite.Core.Extensions
{
    internal static class PageHeroExtensions
    {
        public static string GetHeadline(this IPublishedContent content)
        {
            if (content is IPageHeroComposition)
            {
                return GetHeadline(content as IPageHeroComposition);
            }

            return content.Name;
        }

        public static string GetHeadline(this IPageHeroComposition content)
        {
            var names = new string[]
            {
                content.Headline,
                content.Name
            };

            return names.FirstOrDefault(x => string.IsNullOrWhiteSpace(x) == false);
        }
    }
}
