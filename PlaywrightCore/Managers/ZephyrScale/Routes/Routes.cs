using AutomationCore.Managers;
using Microsoft.Playwright;
using RestSharp;
using System.Text.Json;

namespace Core.Managers.ZephyrScale.Routes
{
    public abstract class Routes
    {
        protected IPlaywright playwright;
        protected RunSettings runSettings;
        protected Dictionary<string, string> ZephyrHeaders;
        protected Dictionary<string, object> ZephyrParams;
        protected abstract string routeURL { get; }

        public Routes(IPlaywright Playwright, RunSettings RunSettings) 
        { 
            runSettings = RunSettings;
            playwright = Playwright;
            ZephyrHeaders = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {runSettings.ZephyrToken}" }
            };

            ZephyrParams = new Dictionary<string, object> 
            {
                { "projectKey" , runSettings.ZephyrProjectKey }
            };
        }

        public async Task<T> GetZephyrAsync<T>(string newRouteURL = null, Dictionary<string, object> additionalParams = null)
        {
            var request = await playwright.APIRequest.NewContextAsync();
            APIRequestContextOptions options = new APIRequestContextOptions();
            options.Headers = ZephyrHeaders;

            if (additionalParams is null)
            {
                options.Params = ZephyrParams;
            } else
            {
                var combinedParams = new Dictionary<string, object>();
                Parallel.ForEach(additionalParams, param =>
                {
                    combinedParams.TryAdd(param.Key, param.Value);
                });
                Parallel.ForEach(ZephyrParams, param =>
                {
                    combinedParams.TryAdd(param.Key, param.Value);
                });
                options.Params = combinedParams;
            }

            var url_Suffix = newRouteURL ?? routeURL;
            var response = await request.GetAsync($"{runSettings.ZephyrUrl}{url_Suffix}", options);

            try
            {
                return response.JsonAsync().Result.Value.Deserialize<T>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error during parsing zephyr scale api response", ex);
            }
        }
    }
}
