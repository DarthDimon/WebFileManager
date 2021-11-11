﻿using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public IActionResult Files()
        {
            FolderModel folderModel = new FolderModel("C:\\");
            DirectoryInfo directoryInfo = new DirectoryInfo("C:\\");
            if (directoryInfo.Exists)
            {
                var a=directoryInfo.GetDirectories();
            }
            return View("~/Views/FilesViews/Files.cshtml");
        }

    }
}
