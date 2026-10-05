using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Security;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.BackofficeAuthenticator
{
    public class UmbracoBackofficeUserAppAuthenticatorComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            var identityBuilder = new BackOfficeIdentityBuilder(builder.Services);

            identityBuilder.AddTwoFactorProvider<UmbracoBackofficeUserAppAuthenticator>(UmbracoBackofficeUserAppAuthenticator.Name);
        }
    }
}
