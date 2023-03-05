using AutomationCore.Managers;
using Core.Models.ZephyrScale.TestExecutions.Statuses;
using Microsoft.Playwright;
using System.Text.Json;

namespace Core.Managers.ZephyrScale.Routes.TestExecutions
{
    public class Statuses_ZS : Routes
    {
        public Statuses_ZS(RunSettings RunSettings) : base(RunSettings)
        {
        }

        protected override string routeURL => "statuses";


        public StatusesResponse GetStatuses_NotAsync()
        {
            var result = GetZephyr<StatusesResponse>();
            return result.Data;
        }

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

        public StatusInfo GetStatus_NotAsync(string name = "In Progress")
        {
            var allStatuses = GetStatuses_NotAsync();
            var result = allStatuses.values.FirstOrDefault(s => s.name.ToLower().Equals(name.ToLower()));
            if (result is null)
            {
                throw new Exception($"Test execution status '{name}' is not found amoung available statuses.");
            }

            return result;
        }
    }
}
