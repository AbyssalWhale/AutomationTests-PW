using AutomationCore.Managers;
using Core.Managers.ZephyrScale.Routes;
using Core.Managers.ZephyrScale.Routes.TestExecutions;
using Core.Models.ZephyrScale.Cycles;
using Microsoft.Playwright;

namespace Core.Managers.ZephyrScale
{
    public class ZephyrScaleApis
    {
        IPlaywright playwright;

        private RunSettings runSettings;
        public Statuses_ZS TestExecutionsStatuses => new Statuses_ZS(playwright, runSettings); 
        public Folders_ZS Folders => new Folders_ZS(playwright, runSettings);
        public Cycles_ZS Cycles => new Cycles_ZS(playwright, runSettings);

        public ZephyrScaleApis(IPlaywright Playwright, RunSettings RunSettings)
        {
            playwright = Playwright;
            runSettings = RunSettings;
        }

        public async Task<CyclePostResponse> CreateNewTestCycle()
        {
            if (runSettings.PublishToZephyr && runSettings.ZephyrCycleID.Equals("0"))
            {
                var status = TestExecutionsStatuses.GetStatus().Result;
                var branchFolder = Folders.GetBranchFolder().Result;
                return await Cycles.CreateTestCycle(branchFolder, status);
            }


            throw new Exception($"Unable create test cycle for test execution. {nameof(runSettings.PublishToZephyr)}:{runSettings.PublishToZephyr}, {nameof(runSettings.ZephyrCycleID)}:{runSettings.ZephyrCycleID}");
        }

        public async Task CompleteCurrentTestCycle()
        {
            if (!int.TryParse(runSettings.ZephyrCycleID, out int cycleId))
            {
                throw new Exception($"Unable to parse test cycle id from .runsettings. Current value: {runSettings.ZephyrCycleID}");
            }

            var cycle = await Cycles.GetCycle(cycleId);
            var status = await TestExecutionsStatuses.GetStatus("Done");
            var cycleUpdateResponse = await Cycles.UpdateCycle(cycle, status);
        }
    }
}
