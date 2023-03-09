using Core.AssertAndErrorMsgs.UI;
using NUnit.Framework;
using System.Collections.Concurrent;
using System.Configuration;
using System.Xml.Linq;

namespace Core.Managers
{
    public class RunSettings
    {
        private static RunSettings? instance = null;

        public bool PublishToZephyr { get; set; }
        public string ZephyrProjectKey { get; set; }
        public string ZephyrUrl { get; set; }
        public string ZephyrCycleName { get; set; }
        public string ZephyrCycleComment { get; set; }
        public string ZephyrCycleKey { get; set; }
        public string ZephyrCycleID { get; set; }
        public string ZephyrToken { get; set; }
        public string AgentTestsResultsFolder { get; set; }
        public string Branch { get; set; }
        public static string? InstanceUrl => TryToParseTestContext(nameof(InstanceUrl));
        public string ApiInstanceUrl { get; set; }
        public string Browser { get; set; }
        public bool Headless { get; set; }
        public int ImplicitWait { get; set; }
        public string RunId { get; set; }
        public string TestReportDirectory => TestsReportDirectory + TestContext.CurrentContext.Test.Name;
        public string TestsReportDirectory { get; set; }
        public string ApiKey { get; set; }
        public string ApiToken { get; set; }
        public string DBServer { get; set; }
        public string DBName { get; set; }
        public string DBUserId { get; set; }
        public string DBUserPass { get; set; }

        public static RunSettings GetRunSettings
        {
            get
            {
                if (instance == null)
                {
                    instance = new RunSettings();
                }
                return instance;
            }
        }

        private RunSettings()
        {
            bool.TryParse(TryToParseTestContext(nameof(PublishToZephyr)), out bool publishToZephyr);
            ZephyrProjectKey = TryToParseTestContext(nameof(ZephyrProjectKey));
            ZephyrUrl = TryToParseTestContext(nameof(ZephyrUrl));
            PublishToZephyr = publishToZephyr;
            ZephyrCycleName = TryToParseTestContext(nameof(ZephyrCycleName));
            ZephyrCycleComment = TryToParseTestContext(nameof(ZephyrCycleComment));
            ZephyrCycleKey = TryToParseTestContext(nameof(ZephyrCycleKey));
            ZephyrCycleID = TryToParseTestContext(nameof(ZephyrCycleID));
            ZephyrToken = TryToParseTestContext(nameof(ZephyrToken));
            AgentTestsResultsFolder = TryToParseTestContext(nameof(AgentTestsResultsFolder));
            Branch = TryToParseTestContext(nameof(Branch));
            ApiInstanceUrl = TryToParseTestContext(nameof(ApiInstanceUrl));
            Browser = TryToParseTestContext(nameof(Browser));
            bool.TryParse(TryToParseTestContext(nameof(Headless)), out bool headless);
            Headless = headless;
            int.TryParse(TryToParseTestContext(nameof(ImplicitWait)), out int implicitWait);
            ImplicitWait = implicitWait;
            RunId = DateTime.UtcNow.ToString("MM-dd-yyyy, hh-mm-ss").Replace("-", "_").Replace(",", "").Replace(" ", "_");
            TestsReportDirectory = $"../../../TestsResults/{RunId}/";
            ApiKey = TryToParseTestContext(nameof(ApiKey));
            ApiToken = TryToParseTestContext(nameof(ApiToken));
            DBServer = TryToParseTestContext(nameof(DBServer));
            DBName = TryToParseTestContext(nameof(DBName));
            DBUserId = TryToParseTestContext(nameof(DBUserId));
            DBUserPass = TryToParseTestContext(nameof(DBUserPass));
        }

        private static string TryToParseTestContext(string settingName)
        {
            var value = TestContext.Parameters[settingName];

            if (value is null) value = ConfigurationManager.AppSettings[settingName];
            if (value is null) throw UIAMessages.GetException($"'{settingName}' setting is not found"); 

            return value;
        }

        public void UpdatePropertiesValueInConfigFile(ConcurrentDictionary<string, string> properties, string configName = "InstanceSettings.runsettings")
        {
            var configPath = Path.Combine(Path.GetFullPath(@"..\..\..\..\"), "TestsConfigurator", "RunSettings", configName);

            if (!File.Exists(configPath))
            {
                throw new Exception($"Unable to locate .runsettings file in path: {configPath}");
            }

            var doc = XDocument.Load(configPath);
            foreach (var property in properties)
            {
                var paramName = property.Key;
                var paramValue = property.Value;

                var node = doc.Descendants("Parameter").SingleOrDefault(n => (string)n.Attribute("name") == paramName);
                if (node != null)
                {
                    node.SetAttributeValue("value", paramValue);
                }
            }
            doc.Save(configPath);
        }
    }
}
