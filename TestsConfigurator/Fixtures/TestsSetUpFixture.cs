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
        public async Task GlobalOneTimeSetUpAsync()
        {
            runSettings = RunSettings.GetRunSettings;
            pwManager = new PlaywrightManager(runSettings);
            zephyrScaleApis = new ZephyrScaleApis(await pwManager.GetPlaywright().ConfigureAwait(false), runSettings);
        }

        [OneTimeTearDown]
        public void GlobalOneTimETearDownAsync()
        {

        }
    }
}