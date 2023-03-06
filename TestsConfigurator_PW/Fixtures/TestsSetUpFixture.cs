using AutomationCore.Managers;
using AutomationCore_PW.Managers;
using Core.Managers.ZephyrScale;
using NUnit.Framework;

namespace TestsConfigurator.Fixtures
{
    public class TestsSetUpFixture
    {
        protected RunSettings? runSettings;
        protected PlaywrightManager? pwManager;
        protected ZephyrScaleApis? zephyrScaleApis;


        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            runSettings = RunSettings.GetRunSettings;
            pwManager = new PlaywrightManager(runSettings);
            zephyrScaleApis = new ZephyrScaleApis(runSettings);
            zephyrScaleApis.CreateTestCycle();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {

        }
    }
}
