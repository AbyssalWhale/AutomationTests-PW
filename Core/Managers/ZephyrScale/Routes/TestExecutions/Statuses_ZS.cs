using Core.Managers;
using Core.Models.ZephyrScale.TestExecutions.Statuses;
using Microsoft.Playwright;
using System.Text.Json;

namespace Core.Managers.ZephyrScale.Routes.TestExecutions
{
    public class Statuses_ZS : Routes
    {
        public Statuses_ZS(IPlaywright Playwright, RunSettings RunSettings) : base(Playwright, RunSettings)
        {
        }

        protected override string routeURL => "statuses";

        public async Task<StatusesResponse> GetStatuses()
        {
            return await GetZephyrAsync<StatusesResponse>();
        }

        public async Task<StatusInfo> GetStatus(string name = "In Progress")
        {
            var allStatuses = await GetStatuses();
            var result = allStatuses.values.FirstOrDefault(s => s.name.ToLower().Equals(name.ToLower()));
            if (result is null)
            {
                throw new Exception($"Test execution status '{name}' is not found amoung available statuses.");
            }

            return result;
        }
    }
}
