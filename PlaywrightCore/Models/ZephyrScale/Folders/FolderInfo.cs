namespace Core.Models.ZephyrScale.Folders
{
    public class FolderInfo
    {
        public int id { get; set; }
        public int? parentId { get; set; }
        public string name { get; set; }
        public int index { get; set; }
        public string folderType { get; set; }
        public Project project { get; set; }
    }
}
