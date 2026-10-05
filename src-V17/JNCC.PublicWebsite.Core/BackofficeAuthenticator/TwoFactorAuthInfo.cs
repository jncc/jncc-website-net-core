using System.Runtime.Serialization;
using Umbraco.Cms.Core.Security;

namespace JNCC.PublicWebsite.Core.BackofficeAuthenticator
{
    [DataContract]
    public class TwoFactorAuthInfo: ISetupTwoFactorModel
    {
        [DataMember(Name = "qrCodeSetupImageUrl")]
        public string QrCodeSetupImageUrl { get; set; } = string.Empty;

        [DataMember(Name = "secret")]
        public string Secret { get; set; } = string.Empty;
    }
}
