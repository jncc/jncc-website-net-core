using JNCC.PublicWebsite.Core.Interfaces.Api;
using JNCC.PublicWebsite.Core.Models.Custom;
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

        private string getAssetUrl = "https://api.hub.beta.jncc.io/asset/";
        private int timeoutInHours = 3;

        private static HttpClient? _httpClient;

        public ResourceApi(ILogger<ResourceApi> logger, AppCaches caches)
        {
            _logger = logger;
            _runtimeCache = caches.RuntimeCache;

            _httpClient ??= new HttpClient();
        }

        public async Task<List<ResourceModel>> GetAllResources()
        {
            //TODO: IMPLEMENT THIS ONCE WE HAVE API ENDPOINT
            return null;
        }

        public async Task<ResourceModel> GetResource(string id)
        {
            var cacheKey = $"resource_api_{id}";

            var model = await _runtimeCache.GetCacheItem(cacheKey, async () =>
            {
                try
                {
					var response = await _httpClient.GetAsync($"{getAssetUrl}{id}");

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
            }, TimeSpan.FromHours(timeoutInHours));

            return model;
        }
    }
}
