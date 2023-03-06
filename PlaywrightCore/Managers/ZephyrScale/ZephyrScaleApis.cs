using AutomationCore.Managers;
using Core.Managers.ZephyrScale.Routes;
using Core.Managers.ZephyrScale.Routes.TestExecutions;
using Microsoft.Playwright;

namespace Core.Managers.ZephyrScale
{
    public class ZephyrScaleApis
    {
        private RunSettings runSettings;
        public Statuses_ZS TestExecutionsStatuses => new Statuses_ZS(runSettings); 
        public Folders_ZS Folders => new Folders_ZS(runSettings);
        public Cycles_ZS Cycles => new Cycles_ZS(runSettings);

        public ZephyrScaleApis(RunSettings runSettings)
        {
            this.runSettings = runSettings;
        }

        public void CreateTestCycle()
        {
            if (runSettings.PublishToZephyr && runSettings.ZephyrCycleID.Equals("0"))
            {
                var status = TestExecutionsStatuses.GetStatus_NotAsync();
                var branchFolder = Folders.GetBranchFolder_NotAsync();
                var testRunCycle = Cycles.CreateTestCycle_NotAsync(branchFolder, status);
                runSettings.ZephyrCycleID = testRunCycle.id.ToString();
                runSettings.UpdatePropertyValueInConfigFile(updateProperty: "ZephyrCycleID", testRunCycle.id.ToString());
            }
        }
    }
}
