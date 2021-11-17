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
        public FolderModel(string path):this(new DirectoryInfo(path)) { }
        public FolderModel(DirectoryInfo directoryInfo)
        {
            ThisDirectoryInfo = directoryInfo;
            Files = directoryInfo.GetFiles();
            Folders = ThisDirectoryInfo.GetDirectories().Where(r => !r.Attributes.HasFlag(FileAttributes.System) & !r.Attributes.HasFlag(FileAttributes.Hidden)).ToArray().GetFolderModels();
            Size += Files.Sum(r => r.Length) + Folders.Sum(r => r.Size);
        }
        public static List<FolderModel> GetFolderModels()
        {
            List<FolderModel> folderModels = new List<FolderModel>();
            foreach (var el in WebFileManager.Controllers.FilesController.disk.Disks)
            {
                folderModels.Add(new FolderModel(el.Name));
            }
            //folderModels.Add(new FolderModel("C:\\"));
            return folderModels;
        }

    }
    public static class FolderModelMetods
    {
        /// <summary>
        /// возвращает FolderModel по массиву DirectoryInfo
        /// </summary>
        /// <param name="directoryInfos"></param>
        /// <returns></returns>
        public static List<FolderModel> GetFolderModels(this DirectoryInfo[] directoryInfos)
        {
            List<FolderModel> folderModels = new List<FolderModel>();
            foreach(DirectoryInfo directoryInfo in directoryInfos)
            {
                try
                {
                    folderModels.Add(new FolderModel(directoryInfo));
                }
                catch 
                {
                }
            }
            return folderModels;
        }
        /// <summary>
        /// возвращает FolderModel по указанному пути
        /// </summary>
        /// <param name="folderModels"> откуда ищет</param>
        /// <param name="fullPath">путь по которому брать модель</param>
        /// <returns></returns>
        public static FolderModel GetCurentFolderModel(this List<FolderModel> folderModels, string fullPath)
        {
            string[] pathEls = fullPath.Split('\\').Where(r=>r!="").ToArray();
            FolderModel folderModel = folderModels.FirstOrDefault(r => r.ThisDirectoryInfo.Name.Replace("\\", "") == pathEls[0]);
            for (int i = 1; i < pathEls.Length; i++)
            {
                folderModel = folderModel.Folders.FirstOrDefault(r => r.ThisDirectoryInfo.Name.Trim() == pathEls[i].Trim());
            }
            return folderModel;
        }
    }
}
