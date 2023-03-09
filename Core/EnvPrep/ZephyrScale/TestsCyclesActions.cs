using Core.Managers;
using Core.Managers.ZephyrScale;
using NUnit.Framework;
using System.Collections.Concurrent;

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
            //runSettings.UpdatePropertyValueInConfigFile(updateProperty: nameof(runSettings.ZephyrCycleID), cycle.id.ToString());
            var configPropertiesToUpdate = new ConcurrentDictionary<string, string>();
            configPropertiesToUpdate.TryAdd(nameof(runSettings.ZephyrCycleID), cycle.id.ToString());
            configPropertiesToUpdate.TryAdd(nameof(runSettings.ZephyrCycleKey), cycle.key.ToString());
            runSettings.UpdatePropertiesValueInConfigFile(configPropertiesToUpdate);
        }

        [Test]
        public async Task Complete_TestCycle()
        {
            await zephyrScaleApis.CompleteCurrentTestCycle();
        }
    }
}
