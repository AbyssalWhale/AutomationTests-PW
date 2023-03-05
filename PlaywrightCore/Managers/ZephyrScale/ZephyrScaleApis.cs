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
    }
}
