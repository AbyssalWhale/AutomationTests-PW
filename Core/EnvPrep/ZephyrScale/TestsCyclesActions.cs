using Core.Managers;
using Core.Managers.ZephyrScale;
using NUnit.Framework;

namespace Core.EnvPrep.ZephyrScale
{
    [TestFixture]
    public class TestsCyclesActions
    {
        protected RunSettings? runSettings;
        protected PlaywrightManager? pwManager;
        protected ZephyrScaleApis? zephyrScaleApis;

        [OneTimeSetUp]
        public async Task RunBeforeAnyTests()
        {
            runSettings = RunSettings.GetRunSettings;
            pwManager = new PlaywrightManager(runSettings);
            zephyrScaleApis = new ZephyrScaleApis(pwManager.GetPlaywright().Result, runSettings);
        }

        [Test]
        public async Task Create_TestCycle()
        {
            var cycle = await zephyrScaleApis.CreateNewTestCycle();
            runSettings.ZephyrCycleID = cycle.id.ToString();
            runSettings.UpdatePropertyValueInConfigFile(updateProperty: nameof(runSettings.ZephyrCycleID), cycle.id.ToString());
        }

        [Test]
        public async Task Complete_TestCycle()
        {
            await zephyrScaleApis.CompleteCurrentTestCycle();
        }
    }
}
