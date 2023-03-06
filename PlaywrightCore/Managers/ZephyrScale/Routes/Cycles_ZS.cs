using AutomationCore.Managers;
using Core.Models.ZephyrScale.Cycles;
using Core.Models.ZephyrScale.Folders;
using Core.Models.ZephyrScale.TestExecutions.Statuses;
using Microsoft.Playwright;
using RestSharp;
using System.Text.Json;

namespace Core.Managers.ZephyrScale.Routes
{
    public class Cycles_ZS : Routes
    {
        public Cycles_ZS(IPlaywright Playwright, RunSettings RunSettings) : base(Playwright, RunSettings)
        {
        }

        protected override string routeURL => "testcycles";

        public async Task<CyclePostResponse> CreateTestCycle(FolderInfo folder, StatusInfo status, string description = null)
        {
            var request = playwright.APIRequest.NewContextAsync();
            APIRequestContextOptions options = new APIRequestContextOptions();
            options.Headers = ZephyrHeaders;
            options.Params = ZephyrParams;

            var configToWrite = new
            {
                projectKey = runSettings.ZephyrProjectKey,
                name = $"{DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")} Build ID: {runSettings.BuildId}",
                description = "Default Description",
                jiraProjectVersion = 0,
                statusName = status.name,
                folderId = folder.id
            };

            options.DataObject = configToWrite;

            var response = await request.Result.PostAsync($"{runSettings.ZephyrUrl}{routeURL}", options);

            try
            {
                return response.JsonAsync().Result.Value.Deserialize<CyclePostResponse>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error during parsing zephyr scale api response", ex);
            }
        }
    }
}
