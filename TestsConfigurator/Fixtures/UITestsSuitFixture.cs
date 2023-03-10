using Core.AssertAndErrorMsgs.UI;
using NUnit.Framework;
using System.Collections.Concurrent;
using TestsConfigurator_PW.Models.POM;

namespace TestsConfigurator.Fixtures
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class UITestsSuitFixture : TestsSetUpFixture
    {
        private ConcurrentDictionary<string, HomePage> homePages;
        protected HomePage? HomePage => homePages[TestContext.CurrentContext.Test.Name];
        

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            homePages ??= new ConcurrentDictionary<string, HomePage>();
        }

        [SetUp]
        public async Task SetupAsync()
        {
            Directory.CreateDirectory(runSettings.TestsReportDirectory);

            await InitTestHomePage();
            await HomePage.Navigate();
        }

        [TearDown] 
        public async Task TearDownAsync()
        {
            if (pwManager is null)
            {
                throw UIAMessages.GetExceptionForNullObject(nameof(pwManager), nameof(SetupAsync));
            }
            await pwManager.ReleaseTestExecution();
        }

        private async Task InitTestHomePage()
        {
            if (pwManager is null)
            {
                throw UIAMessages.GetExceptionForNullObject(nameof(pwManager), nameof(SetupAsync));
            }
            var newHomePage = new HomePage(await Task.FromResult(await pwManager.GetTest_PWContext().Result.NewPageAsync()));


            if (homePages is null)
            {
                throw UIAMessages.GetExceptionForNullObject(nameof(homePages), nameof(SetupAsync));
            }

            homePages.TryAdd(TestContext.CurrentContext.Test.Name, newHomePage);

            if (HomePage is null)
            {
                throw UIAMessages.GetExceptionForNullObject(nameof(HomePage), nameof(SetupAsync));
            }
        }
    }
}