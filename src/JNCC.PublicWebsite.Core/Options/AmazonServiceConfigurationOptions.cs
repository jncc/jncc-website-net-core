using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Options
{
    public class AmazonServiceConfigurationOptions
    {
        public const string AmazonServiceConfiguration = "JNCC_AWS";

        public string AWSESAccessKey { get; set; } = string.Empty;
        public string AWSESSecretKey { get; set; } = string.Empty;
        public string AWSSQSAccessKey { get; set; } = string.Empty;
        public string AWSSQSSecretKey { get; set; } = string.Empty;
        public string AWSESRegion { get; set; } = string.Empty;
        public string AWSService { get; set; } = string.Empty;
        public string AWSESEndpoint { get; set; } = string.Empty;
        public string AWSESIndex { get; set; } = string.Empty;
        public string AWSSQSEndpoint { get; set; } = string.Empty;
        public string AWSSQSPayloadBucket { get; set; } = string.Empty;
        public bool EnableIndexing { get; set; } = false;
        public List<string> IndexFields { get; set; }
    }
}
