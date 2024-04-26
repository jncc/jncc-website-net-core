using System.Runtime.Serialization;

namespace JNCC.PublicWebsite.Core.BackofficeAuthenticator
{
    [DataContract]
    public class TwoFactorAuthInfo
    {
        [DataMember(Name = "qrCodeSetupImageUrl")]
        public string QrCodeSetupImageUrl { get; set; }

        [DataMember(Name = "secret")]
        public string Secret { get; set; }
    }
}
