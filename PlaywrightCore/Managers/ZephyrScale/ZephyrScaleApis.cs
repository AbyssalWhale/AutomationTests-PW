using AutomationCore.Managers;
using Core.Managers.ZephyrScale.Routes;
using Core.Managers.ZephyrScale.Routes.TestExecutions;
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

        public async Task CreateTestCycle()
        {
            if (runSettings.PublishToZephyr && runSettings.ZephyrCycleID.Equals("0"))
            {
                var status = TestExecutionsStatuses.GetStatus().Result;
                var branchFolder = Folders.GetBranchFolder().Result;
                var testRunCycle = await Cycles.CreateTestCycle(branchFolder, status);
                runSettings.ZephyrCycleID = testRunCycle.id.ToString();
                runSettings.UpdatePropertyValueInConfigFile(updateProperty: "ZephyrCycleID", testRunCycle.id.ToString());
            }
        }
    }
}
