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
                name = $"🕵️‍♀️{DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")} 🏗️ {runSettings.ZephyrCycleName}",
                description = $"Playwright tests execution. {runSettings.ZephyrCycleComment}",
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

        public async Task<CycleGetResponse> GetCycle(int id)
        {
            return  await GetZephyrAsync<CycleGetResponse>(newRouteURL: $"{routeURL}/{id}");
        }

        public async Task<IAPIResponse> UpdateCycle(CycleGetResponse cycle, StatusInfo status)
        {
            var request = playwright.APIRequest.NewContextAsync();
            APIRequestContextOptions options = new APIRequestContextOptions();
            options.Headers = ZephyrHeaders;
            options.Params = ZephyrParams;

            var requestBody = new
            {
                id = cycle.id,
                key = cycle.key,
                name = cycle.name,
                project = new
                {
                    id = cycle.project.id,
                    self = cycle.project.self,
                },
                status = new
                {
                    id = status.id,
                },
                folder = new
                {
                    id = cycle.folder.id,
                }
            };
            options.DataObject = requestBody;

            return await request.Result.PutAsync($"{runSettings.ZephyrUrl}{routeURL}/{cycle.id}", options);
        }
    }
}
