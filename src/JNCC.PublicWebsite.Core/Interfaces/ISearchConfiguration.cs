namespace JNCC.PublicWebsite.Core.Configuration
{
    internal interface ISearchConfiguration
    {
        string AWSESRegion { get; set; }
        string AWSESIndex { get; set; }
        string AWSSQSEndpoint { get; set; }
        string AWSSQSPayloadBucket { get; set; }
        string AWSSQSAccessKey { get; set; }
        string AWSSQSSecretKey { get; set; }
        bool EnableIndexing { get; set; }
        bool EnableEditPageBar { get; set; }

    }
}
