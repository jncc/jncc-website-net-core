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
        private string getSitemapUrl = "/sitemap/";

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

            //TODO: REMOVE TEMP ITEMS AND USE REAL CODE ONCE API ENDPOINT IS CORRECTLY SET UP
            //var model = await _runtimeCache.GetCacheItem(cacheKey, async () =>
            //{
            //    try
            //    {
            //        var response = await _httpClient.GetAsync($"{_configuration.GetValue<string>(AppSettings.ResourceApiBaseUrl)}{getSitemapUrl}");

            //        if (response.IsSuccessStatusCode)
            //        {
            //            var model = JsonConvert.DeserializeObject<List<ResourceSitemap>>(await response.Content.ReadAsStringAsync());

            //            return model;
            //        }
            //        else
            //        {
            //            _logger.LogWarning($"Resource API could not find the sitemap items");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        _logger.LogError(ex, "Error occured accessing the Resource API");
            //    }

            //    return null;
            //}, TimeSpan.FromHours(_configuration.GetValue<int>(AppSettings.ResourceApiCacheInHours)));

            var model = new List<ResourceSitemap>();

            model.Add(new ResourceSitemap() { Id = "fc25e10e-857b-4c08-a591-30fcd65d96dc", LastUpdated = DateTime.Now.AddDays(-1) });
            model.Add(new ResourceSitemap() { Id = "daa8e792-a36e-436b-98d7-e2f38e860650", LastUpdated = DateTime.Now.AddDays(-2) });
            model.Add(new ResourceSitemap() { Id = "048f7e78-a2c6-4982-91c3-e496f063bf2b", LastUpdated = DateTime.Now.AddDays(-3) });

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
