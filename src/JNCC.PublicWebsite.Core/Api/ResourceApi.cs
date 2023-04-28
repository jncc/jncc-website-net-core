using JNCC.PublicWebsite.Core.Constants;
using JNCC.PublicWebsite.Core.Interfaces.Api;
using JNCC.PublicWebsite.Core.Models.Custom;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Cache;
using Umbraco.Extensions;

namespace JNCC.PublicWebsite.Core.Api
{
    public class ResourceApi : IResourceApi
    {
        private readonly ILogger<ResourceApi> _logger;
        private readonly IAppPolicyCache _runtimeCache;
        private readonly IConfiguration _configuration;

        private string getAssetUrl = "/asset/";
        private string getSitemapUrl = "/get-all";

        private static HttpClient? _httpClient;

        public ResourceApi(ILogger<ResourceApi> logger, AppCaches caches, IConfiguration configuration)
        {
            _logger = logger;
            _runtimeCache = caches.RuntimeCache;
            _configuration = configuration;

            _httpClient ??= new HttpClient();
        }

        public async Task<List<ResourceSitemap>> GetSitemapResources()
        {
            var cacheKey = $"resource_api_sitemap";

            var model = await _runtimeCache.GetCacheItem(cacheKey, async () =>
            {
                try
                {
                    var response = await _httpClient.GetAsync($"{_configuration.GetValue<string>(AppSettings.ResourceApiBaseUrl)}{getSitemapUrl}");

                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = JsonConvert.DeserializeObject<ResourceSitemapResonse>(await response.Content.ReadAsStringAsync());

                        var model = apiResponse?.Items;

                        return model;
                    }
                    else
                    {
                        _logger.LogWarning($"Resource API could not find the sitemap items, response code was: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occured accessing the Resource API");
                }

                return null;
            }, TimeSpan.FromHours(_configuration.GetValue<int>(AppSettings.ResourceApiCacheInHours)));

            return model;
        }

        public async Task<ResourceModel> GetResource(string id)
        {
            var cacheKey = $"resource_api_{id}";

            var model = await _runtimeCache.GetCacheItem(cacheKey, async () =>
            {
                try
                {
					var response = await _httpClient.GetAsync($"{_configuration.GetValue<string>(AppSettings.ResourceApiBaseUrl)}{getAssetUrl}{id}");

					if (response.IsSuccessStatusCode)
					{
						var model = JsonConvert.DeserializeObject<ResourceModel>(await response.Content.ReadAsStringAsync());

						return model;
					}
					else
					{
						_logger.LogWarning($"Resource API could not find resource with ID: {id}");
					}
				}
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Error occured accessing the Resource API");
                }

                return null;
            }, TimeSpan.FromHours(_configuration.GetValue<int>(AppSettings.ResourceApiCacheInHours)));

            return model;
        }
    }
}
