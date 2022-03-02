using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using JNCC.PublicWebsite.Core.Interfaces.Providers;
using JNCC.PublicWebsite.Core.Providers;

namespace JNCC.PublicWebsite.Core.Composers
{
    public class WebComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            //Services
            builder.Services.AddSingleton<IHomePageService, HomePageService>();
            builder.Services.AddSingleton<IScienceDetailsPageProvider, UmbracoScienceDetailsPageProvider>();
            builder.Services.AddSingleton<ISciencePageCategoriesProvider, UmbracoSciencePageCategoriesProvider>();
            builder.Services.AddSingleton<IUmbracoArticleYearsProvider, UmbracoArticleYearsProvider>();
            builder.Services.AddSingleton<IUmbracoArticleTypesProvider, UmbracoArticleTypesProvider>();
            builder.Services.AddSingleton<IUmbracoArticlePageTagsProvider, UmbracoArticlePageTagsProvider>();
        }
    }
}