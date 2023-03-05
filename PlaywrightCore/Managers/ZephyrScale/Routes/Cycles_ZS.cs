using AutomationCore.Managers;
using Core.Models.ZephyrScale.Cycles;
using Core.Models.ZephyrScale.Folders;
using Core.Models.ZephyrScale.TestExecutions.Statuses;
using RestSharp;

namespace Core.Managers.ZephyrScale.Routes
{
    public class Cycles_ZS : Routes
    {
        public Cycles_ZS(RunSettings RunSettings) : base(RunSettings)
        {
        }

        protected override string routeURL => "testcycles";

        //public CyclePostResponse CreateTestCycle(FolderInfo folder, StatusInfo status, string description = null)
        //{
        //    var request = playwright.APIRequest.NewContextAsync();
        //    APIRequestContextOptions options = new APIRequestContextOptions();
        //    options.Headers = ZephyrHeaders;
        //    options.Params = ZephyrParams;

        //    var configToWrite = new
        //    {
        //        projectKey = runSettings.ZephyrProjectKey,
        //        name = $"{DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")} Build ID: {runSettings.BuildId}",
        //        description = "Default Description",
        //        jiraProjectVersion = 0,
        //        statusName = status.name,
        //        folderId = folder.id
        //    };

        //    options.DataObject = configToWrite;

        //    var response = request.Result.PostAsync($"{runSettings.ZephyrUrl}{routeURL}", options);

        //    try
        //    {
        //        return response.Result.JsonAsync().Result.Value.Deserialize<CyclePostResponse>();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error during parsing zephyr scale api response", ex);
        //    }
        //}

        public CyclePostResponse CreateTestCycle_NotAsync(FolderInfo folder, StatusInfo status, string description = null)
        {
            var request = new RestRequest(routeURL, Method.Post);
            Parallel.ForEach(ZephyrHeaders, parameter => { request.AddHeader(parameter.Key, parameter.Value.ToString()); });
            Parallel.ForEach(ZephyrParams, parameter => { request.AddHeader(parameter.Key, parameter.Value.ToString()); });

            request.AddJsonBody(new
            {
                projectKey = runSettings.ZephyrProjectKey,
                name = $"{DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")} Build ID: {runSettings.BuildId}",
                description = "Default Description",
                jiraProjectVersion = 0,
                statusName = status.name,
                folderId = folder.id
            });
            request.AddHeader("content-type", "application/json");

            var result = restClient.Execute<CyclePostResponse>(request);
            return result.Data;
        }
    }
}
