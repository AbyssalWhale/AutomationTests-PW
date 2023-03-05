using Core.Models.ZephyrScale.Cycles;
using NUnit.Framework;
using System.Xml;
using TestsConfigurator.Fixtures;

namespace RegressionTests
{
    [TestFixture]
    public class CreateTestCycle : ZephyrFixtures
    {
        [Test]
        public  void Create_TestCycle_DoNotRunLocally()
        {
            if (runSettings.PublishToZephyr && runSettings.ZephyrCycleID.Equals("0"))
            {
                var status = zephyrScaleApis.TestExecutionsStatuses.GetStatus_NotAsync();
                var branchFolder = zephyrScaleApis.Folders.GetBranchFolder_NotAsync();
                var testRunCycle = zephyrScaleApis.Cycles.CreateTestCycle_NotAsync(branchFolder, status);
                runSettings.ZephyrCycleID = testRunCycle.id.ToString();
                Console.WriteLine(runSettings.ZephyrCycleID);
                UpdateRunSettings(testRunCycle);
            }
        }

        [Test]
        public void Complete_TestCycle_DoNotRunLocally()
        {
            Console.WriteLine(runSettings.ZephyrCycleID);
        }

        private void UpdateRunSettings(CyclePostResponse cycle)
        {
            var targetNodeName = "ZephyrCycleID";
            var configDir = Path.GetFullPath(@"..\..\..\..\") + "TestsConfigurator_PW\\RunSettings\\InstanceSettings.runsettings";
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

                    if (currentValue.Equals("0"))
                    {
                        nodeValueAttr.Value = cycle.id.ToString();
                    }
                }
            }

            doc.Save(configDir);
        }
    }
}
