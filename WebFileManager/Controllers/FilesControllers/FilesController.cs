using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebFileManager.Models;
using WebFileManager.Models.FilesModels;
using System.IO;


namespace WebFileManager.Controllers
{
    public class FilesController : Controller
    {
        public static DisksModel disk = new DisksModel();
        public static List<FolderModel> folderModels = FolderModel.GetFolderModels();
        [HttpGet]
        public IActionResult Files()
        {
            return View("~/Views/FilesViews/Files.cshtml");
        }
        [HttpPost]
        public ContentResult NewFolder(string folderName, string pathNow)
        {
            return Content(folderModels.GetFolderViewModel(pathNow, folderName).ToJson(), "application/json");
        }
        [HttpPost]
        public ContentResult NewFolderBack(string pathNow)
        {
            return Content(folderModels.GetFolderViewModel(pathNow.Substring(0, pathNow.LastIndexOf("\\"))).ToJson(), "application/json");
        }

    }
}
