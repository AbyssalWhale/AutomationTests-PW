using AutomationCore.AssertAndErrorMsgs.UI;
using NUnit.Framework;
using System.Collections.Concurrent;
using TestsConfigurator.Fixtures;
using TestsConfigurator_PW.Models.POM;

namespace TestsConfigurator_PW.Fixtures
{
    [Parallelizable(ParallelScope.Children)]
    [TestFixture]
    public class UITestsSuitFixture : TestsSetUpFixture
    {
        private ConcurrentDictionary<string, HomePage>? homePages;
        protected HomePage? HomePage => homePages[TestContext.CurrentContext.Test.Name];
        

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            homePages = new ConcurrentDictionary<string, HomePage>();
        }

        [SetUp]
        public async Task Setup()
        {
            Directory.CreateDirectory(runSettings.TestsReportDirectory);

            if (pwManager is null)
            {
                throw UIAMessages.GetExceptionForNullObject(nameof(pwManager), nameof(Setup));
            }
            var newHomePage = new HomePage(await pwManager.GetTest_PWContext().Result.NewPageAsync());

            lock (this)
            {
                if (homePages is null)
                {
                    throw UIAMessages.GetExceptionForNullObject(nameof(homePages), nameof(Setup));
                }

                homePages.TryAdd(TestContext.CurrentContext.Test.Name, newHomePage);
            }

            if (HomePage is null)
            {
                throw UIAMessages.GetExceptionForNullObject(nameof(HomePage), nameof(Setup));
            }

            await HomePage.Navigate();
        }

        [TearDown] 
        public async Task TearDown()
        {
            if (pwManager is null)
            {
                throw UIAMessages.GetExceptionForNullObject(nameof(pwManager), nameof(Setup));
            }
            await pwManager.ReleaseTestExecution();
        }
    }
}