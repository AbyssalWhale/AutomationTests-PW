using AutomationCore.Managers;
using Core.Models.ZephyrScale.Folders;
using Microsoft.Playwright;
using System.Text.Json;
using System.Xml.Linq;

namespace Core.Managers.ZephyrScale.Routes
{
    public class Folders_ZS : Routes
    {
        public Folders_ZS(RunSettings RunSettings) : base(RunSettings)
        {
        }

        protected override string routeURL => "folders";


        public async Task<FoldersResponse> GetFolders(string folderType = "TEST_CYCLE")
        {
            var myParams = new Dictionary<string, object>()
            {
                { "folderType", folderType }
            };
            var result = await GetZephyrAsync<FoldersResponse>(myParams);
            return result;
        }

        public FoldersResponse GetFolders_NotAsync(string folderType = "TEST_CYCLE")
        {
            var myParams = new Dictionary<string, string>()
            {
                { "folderType", folderType }
            };
            var result = GetZephyr<FoldersResponse>(myParams);
            return result.Data;
        }

        public async Task<FolderInfo> GetBranchFolder(string folderType = "TEST_CYCLE")
        {
            var folders = await GetFolders(folderType);
            var result = folders.values.FirstOrDefault(f => f.name.ToLower().Equals(runSettings.Branch));
            if (result is null)
            {
                throw new Exception($"Folder for branch '{runSettings.Branch}' is not found amoung folder types {folderType}.");
            }

            return result;
        }

        public FolderInfo GetBranchFolder_NotAsync(string folderType = "TEST_CYCLE")
        {
            var folders = GetFolders_NotAsync(folderType);
            var result = folders.values.FirstOrDefault(f => f.name.ToLower().Equals(runSettings.Branch));
            if (result is null)
            {
                throw new Exception($"Folder for branch '{runSettings.Branch}' is not found amoung folder types {folderType}.");
            }

            return result;
        }
    }
}
