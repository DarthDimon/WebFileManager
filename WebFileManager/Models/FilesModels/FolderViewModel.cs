using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebFileManager.Models.FilesModels
{
    public class FolderViewModel
    {
        public string CurentPath { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Size { get; set; }
        public DateTime ChangeDate { get; set; }
    }
    public static class FolderViewModelM
    {
        /// <summary>
        /// возвращает FolderViewModel указаной папки
        /// </summary>
        /// <param name="folderModels"></param>
        /// <param name="path">текущий путь</param>
        /// <param name="folderName">папка в которую надо зайти</param>
        /// <returns></returns>
        public static List<FolderViewModel> GetFolderViewModel(this List<FolderModel> folderModels, string path, string folderName, string SortName, string SortType)
        {
            List<FolderViewModel> folderViewModels;
            if(folderName==null) { folderViewModels = folderModels.GetFolderViewModel((path).Replace("\\\\", "\\")); }
            else { folderViewModels = folderModels.GetFolderViewModel((path + "\\" + folderName).Replace("\\\\", "\\")); }
            if (SortName != null)
            {
                switch (SortName)
                {
                    case "Name":
                        if (SortType == "Asc") { folderViewModels = folderViewModels.OrderBy(r => r.Name).ToList(); }
                        else { folderViewModels = folderViewModels.OrderByDescending(r => r.Name).ToList(); }
                        break;
                    case "Type":
                        if (SortType == "Asc") { folderViewModels = folderViewModels.OrderBy(r => r.Type).ToList(); }
                        else { folderViewModels = folderViewModels.OrderByDescending(r => r.Type).ToList(); }
                        break;
                    case "Size":
                        if (SortType == "Asc") { folderViewModels = folderViewModels.OrderBy(r => r.Size).ToList(); }
                        else { folderViewModels = folderViewModels.OrderByDescending(r => r.Size).ToList(); }
                        break;
                    case "ChangeDate":
                        if (SortType == "Asc") { folderViewModels = folderViewModels.OrderBy(r => r.ChangeDate).ToList(); }
                        else { folderViewModels = folderViewModels.OrderByDescending(r => r.ChangeDate).ToList(); }
                        break;
                }
            }
            return folderViewModels;
        }
        /// <summary>
        /// возвращает FolderViewModel указаной папки
        /// </summary>
        /// <param name="folderModels"></param>
        /// <param name="fullPath">текущий путь + папка в которую надо зайти</param>
        /// <returns></returns>
        public static List<FolderViewModel> GetFolderViewModel(this List<FolderModel> folderModels, string fullPath)
        {
            FolderModel folderModel = folderModels.GetCurentFolderModel(fullPath);
            return folderModel.ToFolderViewModel();
        }
        /// <summary>
        /// возвращает FolderViewModel преобразованную из FolderModel
        /// </summary>
        /// <param name="folderModel"></param>
        /// <returns></returns>
        public static List<FolderViewModel> ToFolderViewModel(this FolderModel folderModel)
        {
            List<FolderViewModel> folderViewModels = new List<FolderViewModel>();
            foreach (var folder in folderModel.Folders)
            {
                folderViewModels.Add(new FolderViewModel()
                {
                    Name = folder.ThisDirectoryInfo.Name,
                    ChangeDate = folder.ThisDirectoryInfo.LastWriteTime,
                    Type = "Folder",
                    Size = folder.Size.ToMB(),
                    CurentPath = folderModel.ThisDirectoryInfo.FullName
                });
            }
            foreach (var file in folderModel.Files)
            {
                folderViewModels.Add(new FolderViewModel()
                {
                    Name = file.Name,
                    ChangeDate = file.LastWriteTime,
                    Type = "File",
                    Size = file.Length.ToMB(),
                    CurentPath = folderModel.ThisDirectoryInfo.FullName
                });
            }
            return folderViewModels;
        }
        public static string ToJson(this List<FolderViewModel> folderViewModels)
        {
            return JsonSerializer.Serialize(folderViewModels);
        }
    }
}
