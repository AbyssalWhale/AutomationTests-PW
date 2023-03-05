using AutomationCore.Managers;
using AutomationCore_PW.Managers;
using NUnit.Framework;

namespace TestsConfigurator.Fixtures
{
    public class TestsSetUpFixture
    {
        protected RunSettings? runSettings;
        protected PlaywrightManager? pwManager;
        

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            runSettings = RunSettings.GetRunSettings;
            pwManager = new PlaywrightManager(runSettings);
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {

        }
    }
}
