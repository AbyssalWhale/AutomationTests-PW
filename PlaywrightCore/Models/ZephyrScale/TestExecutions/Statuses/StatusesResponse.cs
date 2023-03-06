using Newtonsoft.Json;

namespace Core.Models.ZephyrScale.TestExecutions.Statuses
{
    public class StatusesResponse
    {
        public string next { get; set; }
        public int startAt { get; set; }
        public int maxResults { get; set; }
        public int total { get; set; }
        public bool isLast { get; set; }
        public List<StatusInfo> values { get; set; }
    }
}
