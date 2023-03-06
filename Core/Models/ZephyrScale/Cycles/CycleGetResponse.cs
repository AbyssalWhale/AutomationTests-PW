using Core.Models.ZephyrScale.Folders;
using Core.Models.ZephyrScale.TestExecutions.Statuses;

namespace Core.Models.ZephyrScale.Cycles
{
    public class CycleGetResponse
    {
        public int id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public Project project { get; set; }
        public JiraProjectVersion jiraProjectVersion { get; set; }
        public StatusBase status { get; set; }
        public FolderBase folder { get; set; }
        public string description { get; set; }
        public object plannedStartDate { get; set; }
        public object plannedEndDate { get; set; }
        public object owner { get; set; }
        public CustomFields customFields { get; set; }
        public Links links { get; set; }
    }
}
