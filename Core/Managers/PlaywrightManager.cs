using Core.Enums;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Collections.Concurrent;

namespace Core.Managers
{
    public class PlaywrightManager
    {
        private readonly Lazy<Task<IPlaywright>> playwrightTask;
        private RunSettings runSettings;
        private ConcurrentDictionary<string, IBrowser>? testsBrowsers;
        private ConcurrentDictionary<string, IBrowserContext>? testsContexts;

        public PlaywrightManager(RunSettings RunSettings)
        {
            runSettings = RunSettings;
            playwrightTask = new Lazy<Task<IPlaywright>>(() => Playwright.CreateAsync());
            testsBrowsers ??= new ConcurrentDictionary<string, IBrowser>();
            testsContexts ??= new ConcurrentDictionary<string, IBrowserContext>();
        }

        public async Task<IPlaywright> GetPlaywright()
        {
            return await playwrightTask.Value.ConfigureAwait(false);
        }

        public async Task<IBrowserContext> GetTestContext()
        {   
            if (testsBrowsers.ContainsKey(TestContext.CurrentContext.Test.Name))
            {
                return testsContexts[TestContext.CurrentContext.Test.Name];
            }

            var browser = await InitTestBrowser().ConfigureAwait(false);
            testsBrowsers.TryAdd(TestContext.CurrentContext.Test.Name, browser);
            var context = await InitTestContext(browser).ConfigureAwait(false);
            testsContexts.TryAdd(TestContext.CurrentContext.Test.Name, context);
            return context;
        }

        public async Task ReleaseTestExecution()
        {
            if (Directory.Exists(runSettings.TestReportDirectory))
            {
                var allFiles = Directory.GetFiles(runSettings.TestReportDirectory);
                await Task.Run(() => Parallel.ForEach(allFiles, file => {
                    TestContext.AddTestAttachment(file);
                })).ConfigureAwait(false);
            }
            
            await testsContexts[TestContext.CurrentContext.Test.Name].CloseAsync().ConfigureAwait(false);
            await testsBrowsers[TestContext.CurrentContext.Test.Name].CloseAsync().ConfigureAwait(false);
        }

        private async Task<IBrowser> InitTestBrowser()
        {
            var browser = runSettings.Browser.ToLower();

            if (browser.Equals(Browsers.chrome.ToString()))
            {
                var chrome = await GetPlaywright().Result.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
                {
                    Args = new[] { "--start-maximized" },
                    Headless = runSettings.Headless,
                }); ;

                return chrome;
            }
            else if (browser.Equals(Browsers.firefox.ToString()))
            {
                var fireFox = await GetPlaywright().Result.Firefox.LaunchAsync(new BrowserTypeLaunchOptions()
                {
                    Headless = runSettings.Headless,
                });

                return fireFox;
            }
            else
            {
                var msg = $"Unknown browser is tried to be initialized: {browser}";
                throw Core.AssertAndErrorMsgs.AEMessagesBase.GetException(msg);
            }
        }

        private async Task<IBrowserContext> InitTestContext(IBrowser browser)
        {
            var context = await browser.NewContextAsync(new()
            {
                RecordVideoDir = runSettings.TestReportDirectory,
                RecordVideoSize = new RecordVideoSize() { Width = 1200, Height = 700 },
                IsMobile = false,
            });

            return context;
        }
    }
}
