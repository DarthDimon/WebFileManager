using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
        /// <summary>
        /// сразу в память чтоб не пересчитывать каждый раз размер
        /// </summary>
        public static DisksModel disk = new DisksModel();
        public static List<FolderModel> folderModels = FolderModel.GetFolderModels();
        [HttpGet]
        public IActionResult Files()
        {
            return View("~/Views/FilesViews/Files.cshtml");
        }
        /// <summary>
        /// отправляет представление папки по указанному пути
        /// </summary>
        /// <param name="folderName">путь к папке(сейчас или полный)</param>
        /// <param name="pathNow">(необязательный)добавляет к folderName следующую папку</param>
        /// <param name="orderBy">сортировка по полю</param>
        /// <param name="back">true-возвращает на папку назад от folderName</param>
        /// <returns></returns>
        [HttpPost]
        public ContentResult NewFolder(string folderName, string pathNow, string orderBy=null, bool back=false)
        {
            if (orderBy != null)
            {
                if (HttpContext.Session.GetString("OrderBy") == orderBy)
                {
                    if (HttpContext.Session.GetString("SortType") == "Asc") { HttpContext.Session.SetString("SortType", "Desc"); }
                    else { HttpContext.Session.SetString("SortType", "Asc"); }
                }
                else
                {
                    HttpContext.Session.SetString("OrderBy", orderBy);
                    HttpContext.Session.SetString("SortType", "Asc");
                }
            }
            return Content(folderModels.GetFolderViewModel(!back ? pathNow: pathNow.Substring(0, pathNow.LastIndexOf("\\")), folderName, HttpContext.Session.GetString("OrderBy"), HttpContext.Session.GetString("SortType")).ToJson(), "application/json");
        }
        [HttpPost]
        public ContentResult NewFolderNext(string pathNow)
        {
            if (HttpContext.Session.GetString("maxPath") != pathNow && pathNow.Contains(HttpContext.Session.GetString("maxPath")))
            {
                if (HttpContext.Session.GetString("SortType") == "Asc") { HttpContext.Session.SetString("SortType", "Desc"); }
                else { HttpContext.Session.SetString("SortType", "Asc"); }
                return Content(folderModels.GetFolderViewModel(pathNow.Substring(0, pathNow.LastIndexOf("\\")), null, HttpContext.Session.GetString("OrderBy"), HttpContext.Session.GetString("SortType")).ToJson(), "application/json");
            }
            return null;
        }
    }
}
