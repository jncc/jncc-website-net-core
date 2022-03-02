using Aws4RequestSigner;
using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class DataHubRawQueryService : IDataHubRawQueryService
    {
        private readonly IConfiguration _configuration;

        public DataHubRawQueryService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(DataHubRawQueryService));
        }

        public SearchModel GetByRawQuery(string rawQuery, int numberOfItems)
        {
            var completeRawQuery = string.Format(@"
            {{
                ""from"": {0},
                ""size"": {1},
                ""query"": {2}
            }}", 0, numberOfItems, rawQuery);

            return EsGetByRawQuery(completeRawQuery);
        }

        public SearchModel EsGetByRawQuery(string query)
        {
            return Task.Run(() => EsGetByRawQueryAsync(query)).Result;
        }

        public async Task<SearchModel> EsGetByRawQueryAsync(string rawQuery)
        {
            return await PerformSearchAsync(rawQuery);
        }

        private async Task<SearchModel> PerformSearchAsync(string query)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_configuration.GetValue<string>(AppSettings.AWSESEndpoint) + _configuration.GetValue<string>(AppSettings.AWSESIndex) + "/_search/"),
                Content = new StringContent(query, Encoding.UTF8, "application/json")
            };

            var signedRequest = await GetSignedRequest(request);
            var response = await new HttpClient().SendAsync(signedRequest);
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SearchModel>(responseString);
        }

        private async Task<HttpRequestMessage> GetSignedRequest(HttpRequestMessage request)
        {
            var signer = new AWS4RequestSigner(_configuration.GetValue<string>(AppSettings.AWSESAccessKey), _configuration.GetValue<string>(AppSettings.AWSESSecretKey));
            return await signer.Sign(request, "es", _configuration.GetValue<string>(AppSettings.AWSESRegion));
        }
    }
}
