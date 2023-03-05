using AutomationCore.Managers;
using Core.Managers.ZephyrScale;
using Core.Models.ZephyrScale.Cycles;
using NUnit.Framework;
using System.Xml;

namespace TestsConfigurator.Fixtures
{
    [SetUpFixture]
    public class ZephyrFixtures
    {
        protected RunSettings? runSettings;
        protected ZephyrScaleApis? zephyrScaleApis;


        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            runSettings = RunSettings.GetRunSettings;
            zephyrScaleApis = new ZephyrScaleApis(runSettings);
        }
    }
}
