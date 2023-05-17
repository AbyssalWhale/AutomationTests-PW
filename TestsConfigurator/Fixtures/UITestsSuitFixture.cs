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
            //var testDir = $"{runSettings.TestsReportDirectory}{TestContext.CurrentContext.Test.Name}";
            //testDir = testDir.Replace(@"""", "_");
            //Directory.CreateDirectory(testDir);

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
            await pwManager.ReleaseTestExecution().ConfigureAwait(false);
        }

        private async Task InitTestHomePage()
        {
            var context = await pwManager.GetTestContext().ConfigureAwait(false);
            var newHomePage = new HomePage(await context.NewPageAsync().ConfigureAwait(false));
            homePages.TryAdd(TestContext.CurrentContext.Test.Name, newHomePage);
        }
    }
}