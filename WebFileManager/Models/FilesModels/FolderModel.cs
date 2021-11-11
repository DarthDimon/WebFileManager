using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace WebFileManager.Models.FilesModels
{
    public class FolderModel
    {
        public DirectoryInfo ThisDirectoryInfo { get; set; }
        public FileInfo[] Files { get;  }
        public List<FolderModel> Folders { get; }
        public decimal Size { get; } = 0;



        public FolderModel() { }
        public FolderModel(string path)
        {
            ThisDirectoryInfo = new DirectoryInfo(path);
            Files = ThisDirectoryInfo.GetFiles();
            Folders = ThisDirectoryInfo.GetDirectories().GetFolderModels();
            Size += Files.Sum(r => r.Length) + Folders.Sum(r => r.Size);

        }
        public FolderModel(DirectoryInfo directoryInfo)
        {
            ThisDirectoryInfo = directoryInfo;
            Files = directoryInfo.GetFiles();
            Folders = directoryInfo.GetDirectories().GetFolderModels();
            Size += Files.Sum(r => r.Length);

        }
    }
    public static class FolderModelMetods
    {
        public static List<FolderModel> GetFolderModels(this DirectoryInfo[] directoryInfos)
        {
            List<FolderModel> folderModels = new List<FolderModel>();
            foreach(DirectoryInfo directoryInfo in directoryInfos)
            {
                folderModels.Add(new FolderModel(directoryInfo));
            }
            return folderModels;
        }
    }
}
