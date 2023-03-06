namespace Core.Models.ZephyrScale.Folders
{
    public class FolderInfo : FolderBase
    {
        public int? parentId { get; set; }
        public int index { get; set; }
        public string folderType { get; set; }
        public Project project { get; set; }
    }
}
