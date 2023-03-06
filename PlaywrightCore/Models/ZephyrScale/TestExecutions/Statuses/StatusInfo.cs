using Newtonsoft.Json;

namespace Core.Models.ZephyrScale.TestExecutions.Statuses
{
    public class StatusInfo : StatusBase
    {
        public Project project { get; set; }
        public string name { get; set; }
        public object description { get; set; }
        public int index { get; set; }
        public string color { get; set; }
        public bool archived { get; set; }
        public bool @default { get; set; }
    }
}
