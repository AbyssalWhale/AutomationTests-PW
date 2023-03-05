namespace Core.Models.ZephyrScale.Folders
{
    public class FoldersResponse
    {
        public object next { get; set; }
        public int startAt { get; set; }
        public int maxResults { get; set; }
        public int total { get; set; }
        public bool isLast { get; set; }
        public List<FolderInfo> values { get; set; }
    }
}
