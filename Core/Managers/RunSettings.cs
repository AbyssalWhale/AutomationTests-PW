using Core.AssertAndErrorMsgs.UI;
using NUnit.Framework;
using System.Configuration;
using System.Xml;

namespace Core.Managers
{
    public class RunSettings
    {
        private static RunSettings? instance = null;

        public bool PublishToZephyr { get; set; }
        public string ZephyrProjectKey { get; set; }
        public string ZephyrUrl { get; set; }
        public string ZephyrCycleName { get; set; }
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

        public void UpdatePropertyValueInConfigFile(string updateProperty, string valueToSet)
        {
            var targetNodeName = updateProperty;
            var configDir = Path.GetFullPath(@"..\..\..\..\") + "TestsConfigurator\\RunSettings\\InstanceSettings.runsettings";
            if (!File.Exists(configDir))
            {
                throw new Exception($"Unable to locate .runsettings file in path: {configDir}");
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(configDir);

            XmlNodeList allParams = doc.SelectNodes("/RunSettings/TestRunParameters/Parameter");
            foreach (XmlNode aNode in allParams)
            {
                XmlAttribute nodeNameAttr = aNode.Attributes["name"];
                XmlAttribute nodeValueAttr = aNode.Attributes["value"];

                if (nodeNameAttr != null && nodeNameAttr.Value.Equals(targetNodeName))
                {
                    string currentValue = nodeValueAttr.Value;

                    if (!currentValue.Equals(valueToSet))
                    {
                        nodeValueAttr.Value = valueToSet;
                    }
                }
            }

            doc.Save(configDir);
        }
    }
}
