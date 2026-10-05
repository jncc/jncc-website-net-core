namespace JNCC.PublicWebsite.Core.Options
{
    public class AmazonServiceConfigurationOptions
    {
        public const string AmazonServiceConfiguration = "JNCC_AWS";

        public string AWSSQSAccessKey { get; set; } = string.Empty;
        public string AWSSQSSecretKey { get; set; } = string.Empty;
        public string AWSESRegion { get; set; } = string.Empty;
        public string AWSESIndex { get; set; } = string.Empty;
        public string AWSSQSEndpoint { get; set; } = string.Empty;
        public string AWSSQSPayloadBucket { get; set; } = string.Empty;
        public bool EnableIndexing { get; set; } = false;
        public string IndexFields { get; set; } = string.Empty;
    }
}
