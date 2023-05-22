using Core.Managers;
using Core.Managers.ZephyrScale;
using NUnit.Framework;
using TestsConfigurator.Models.API;

namespace TestsConfigurator.Fixtures
{
    [SetUpFixture]
    public class TestsSetUpFixture
    {
        protected RunSettings? runSettings;
        protected PlaywrightManager? pwManager;
        protected ZephyrScaleApis? zephyrScaleApis;
        protected ApiManager apiManager;
        protected BackendApis backendApis;

        [OneTimeSetUp]
        public async Task GlobalOneTimeSetUpAsync()
        {
            runSettings = RunSettings.GetRunSettings;
            pwManager = new PlaywrightManager(runSettings);
            zephyrScaleApis = new ZephyrScaleApis(await pwManager.GetPlaywright().ConfigureAwait(false), runSettings);
            apiManager = new ApiManager(await pwManager.GetPlaywright().ConfigureAwait(false), runSettings);
            backendApis = new BackendApis(apiManager);
        }

        [OneTimeTearDown]
        public async Task GlobalOneTimETearDownAsync()
        {
            await apiManager.DisposeClient();
        }
    }
}