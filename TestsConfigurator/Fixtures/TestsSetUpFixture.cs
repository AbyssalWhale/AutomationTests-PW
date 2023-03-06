using Core.Managers;
using Core.Managers.ZephyrScale;
using NUnit.Framework;


namespace TestsConfigurator.Fixtures
{
    [SetUpFixture]
    public class TestsSetUpFixture
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

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {

        }
    }
}
