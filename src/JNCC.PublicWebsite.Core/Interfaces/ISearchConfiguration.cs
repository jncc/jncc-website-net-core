using System.Collections.Generic;

namespace JNCC.PublicWebsite.Core.Configuration
{
    internal interface ISearchConfiguration
    {
        string AWSESAccessKey { get; set; }
        string AWSESSecretKey { get; set; }
        string AWSESRegion { get; set; }
        string AWSService { get; set; }
        string AWSESEndpoint { get; set; }
        string AWSESIndex { get; set; }
        string AWSSQSEndpoint { get; set; }
        string AWSSQSPayloadBucket { get; set; }
        string AWSSQSAccessKey { get; set; }
        string AWSSQSSecretKey { get; set; }
        bool EnableIndexing { get; set; }
        bool EnableEditPageBar { get; set; }

    }
}
