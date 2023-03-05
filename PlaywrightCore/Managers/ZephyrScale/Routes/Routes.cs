using AutomationCore.Managers;
using Microsoft.Playwright;
using RestSharp;
using System.Net;
using System.Text.Json;

namespace Core.Managers.ZephyrScale.Routes
{
    public abstract class Routes
    {
        protected RestClient restClient;
        protected IPlaywright playwright;
        protected RunSettings runSettings;
        protected Dictionary<string, string> ZephyrHeaders;
        protected Dictionary<string, object> ZephyrParams;
        protected abstract string routeURL { get; }

        public Routes(RunSettings RunSettings) 
        { 
            runSettings = RunSettings;
            restClient = new RestClient(runSettings.ZephyrUrl);
            ZephyrHeaders = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {runSettings.ZephyrToken}" }
            };

            ZephyrParams = new Dictionary<string, object> 
            {
                { "projectKey" , runSettings.ZephyrProjectKey }
            };
        }

        public RestResponse<T> GetZephyr<T>(Dictionary<string, string> additionalParams = null)
        {
            var request = new RestRequest(routeURL, Method.Get);
            request.AddHeaders(ZephyrHeaders);
            Parallel.ForEach(ZephyrParams, parameter => { request.AddParameter(parameter.Key, parameter.Value.ToString()); });
            if (additionalParams is not null)
            {
                Parallel.ForEach(additionalParams, parameter => { request.AddParameter(parameter.Key, parameter.Value); });
            }
            var result = restClient.Execute<T>(request);

            return result;
        }

        public async Task<T> GetZephyrAsync<T>(Dictionary<string, object> additionalParams = null)
        {
            var request = await playwright.APIRequest.NewContextAsync();
            APIRequestContextOptions options = new APIRequestContextOptions();
            options.Headers = ZephyrHeaders;

            if (additionalParams is null)
            {
                //options.Params = ZephyrParams;
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

            var response = await request.GetAsync($"{runSettings.ZephyrUrl}{routeURL}", options);

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
