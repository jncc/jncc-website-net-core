using Aws4RequestSigner;
using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Interfaces.Services;
using JNCC.PublicWebsite.Core.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace JNCC.PublicWebsite.Core.Services
{
    internal sealed class SearchQueryService : ISearchQueryService
    {
        private readonly IConfiguration _configuration;

        public SearchQueryService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(SearchQueryService));
        }

        public SearchModel Query(string query, int size, int start, string siteFilter = "")
        {
            return Task.Run(() => QueryAsync(query, size, start)).Result;
        }

        public async Task<SearchModel> QueryAsync(string query, int size, int start, string siteFilter = "")
        {
            var q = new
            {
                from = start,
                size = size,
                highlight = new
                {
                    pre_tags = new[] { "<strong>" },
                    post_tags = new[] { "</strong>" },
                    fields = new
                    {
                        content = new
                        {
                            number_of_fragments = 1,
                            order = "score",
                            type = "fvh"
                        },
                        title = new
                        {

                        }
                    }
                },
                _source = new
                {
                    includes = new[] { "*" },
                    excludes = new[] { "content" }
                },
                query = new
                {
                    @bool = new
                    {
                        should = new object[] {
                            new {
                                common = new {
                                    content = new {
                                        query = query,
                                        cutoff_frequency = 0.001,
                                        low_freq_operator = "or"
                                    }
                                }
                            },
                            new {
                                common = new {
                                    title = new {
                                        query = query,
                                        cutoff_frequency = 0.001,
                                        low_freq_operator = "or"
                                    }
                                }
                            }
                        },
                        filter = GetQuerySiteFilter(siteFilter),
                        minimum_should_match = 1
                    }
                }
            };

            var serializedQuery = JsonConvert.SerializeObject(q, Formatting.None);

            return await PerformSearchAsync(serializedQuery);
        }

        private object GetQuerySiteFilter(string siteFilter)
        {
            if (string.IsNullOrWhiteSpace(siteFilter))
            {
                return null;
            }

            return new[] {
                new {
                    match = new {
                        site = new {
                            query = siteFilter
                        }
                    }
                }
            };
        }

        private async Task<SearchModel> PerformSearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(_configuration.GetValue<string>(AppSettings.AWSESEndpoint))) { return null; }
            
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
