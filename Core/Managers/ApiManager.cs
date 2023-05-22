using Microsoft.Playwright;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Text.Json;

namespace Core.Managers
{
    public class ApiManager
    {
        private IPlaywright playwright;
        private RunSettings runSettings;
        private IAPIRequestContext Request;

        public ApiManager(IPlaywright Playwright, RunSettings RunSettings)
        {
            playwright = Playwright;
            runSettings = RunSettings;
        }

        public async Task<IAPIResponse>ExecuteRequest(string url, Method method, bool checkResponseCode = false, HttpStatusCode expectedCode = HttpStatusCode.OK, IEnumerable<KeyValuePair<string, object>>? parameters = null, IEnumerable<KeyValuePair<string, string>>? headers = null)
        {
            if (Request is null) await Create_APIRequest_Context();
            url = url + $"?key={runSettings.ApiKey}";
            var response = await Execute(url, method, parameters, headers);
            if (checkResponseCode) CheckResponseCode(response, expectedCode);

            return response;
        }

        public async Task DisposeClient()
        {
            await Request.DisposeAsync();
        }

        public T Deserialize_ResponseData<T>(IAPIResponse response)
        {
            var responseData = response.JsonAsync().Result;
            if (responseData is null)
            {
                throw new Exception("Can not deserialize response data with null value");
            }

            return responseData.Value.Deserialize<T>();
        }

        private async Task Create_APIRequest_Context()
        {
            Request = await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions()
            {
                BaseURL = runSettings.ApiInstanceUrl
            });
        }

        private async Task<IAPIResponse> Execute(string url, Method method, IEnumerable<KeyValuePair<string, object>>? parameters = null, IEnumerable<KeyValuePair<string, string>>? headers = null)
        {
            var response = method switch
            {
                Method.Get => await Request.GetAsync(url, new APIRequestContextOptions() { Params = parameters, Headers = headers }),
                Method.Post => await Request.PostAsync(url, new APIRequestContextOptions() { Params = parameters, Headers = headers }),
                Method.Put => await Request.PutAsync(url, new APIRequestContextOptions() { Params = parameters, Headers = headers }),
                Method.Delete => await Request.DeleteAsync(url, new APIRequestContextOptions() { Params = parameters, Headers = headers }),
                Method.Head => await Request.HeadAsync(url, new APIRequestContextOptions() { Params = parameters, Headers = headers }),
                Method.Patch => await Request.PatchAsync(url, new APIRequestContextOptions() { Params = parameters, Headers = headers }),
                _ => throw new NotImplementedException(),
            };

            return response;
        }
    
        private void CheckResponseCode(IAPIResponse response, HttpStatusCode expectedCode)
        {
            if (!response.StatusText.Equals(expectedCode.ToString()))
            {
                throw new Exception($"Response code '{response.Status}' is not matched with expected {expectedCode}.");
            }
        }
    }
}
