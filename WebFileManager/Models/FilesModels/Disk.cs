using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace WebFileManager.Models.FilesModels
{
    public class Disk
    {
        public decimal TotalFreeSpace { get; }
        public decimal TotalSize { get; }
        public List<DriveInfo> Disks { get; }


        public Disk()
        {
            Disks = DriveInfo.GetDrives().Where(r => r.IsReady).Where(r => r.DriveType == DriveType.Fixed).ToList();
            TotalSize = Disks.Sum(r => r.TotalSize);
            TotalFreeSpace = Disks.Sum(r => r.TotalFreeSpace);
        }
    }
    public static class DiskMetods
    {
        /// <summary>
        /// возвращает число в килобайтах
        /// </summary>
        /// <param name="size">число для преобразования в килобайты</param>
        /// <param name="decimals">количество знаков после запятой</param>
        /// <returns></returns>
        public static decimal ToKB(this decimal size, int decimals = 2)
        {
            return Math.Round(size / 1024, decimals);
        }
        /// <summary>
        /// возвращает число в мегабайтах
        /// </summary>
        /// <param name="size">число для преобразования в мегабайты</param>
        /// <param name="decimals">количество знаков после запятой</param>
        /// <returns></returns>
        public static decimal ToMB(this decimal size, int decimals = 2)
        {
            return Math.Round(size.ToKB(0) / 1024, decimals);
        }
        /// <summary>
        /// возвращает число в гигобайтах
        /// </summary>
        /// <param name="size">число для преобразования в гигобайты</param>
        /// <param name="decimals">количество знаков после запятой</param>
        /// <returns></returns>
        public static decimal ToGB(this decimal size, int decimals = 2)
        {
            return Math.Round(size.ToMB(0) / 1024, decimals);
        }
        /// <summary>
        /// возвращает число в терабайтах
        /// </summary>
        /// <param name="size">число для преобразования в теробайты</param>
        /// <param name="decimals">количество знаков после запятой</param>
        /// <returns></returns>
        public static decimal ToTB(this decimal size, int decimals = 2)
        {
            return Math.Round(size.ToGB(0) / 1024, decimals);
        }
        /// <summary>
        /// возвращает число в килобайтах
        /// </summary>
        /// <param name="size">число для преобразования в килобайты</param>
        /// <param name="decimals">количество знаков после запятой</param>
        /// <returns></returns>
        public static decimal ToKB(this long size, int decimals = 2)
        {
            return Math.Round((decimal)size / 1024, decimals);
        }
        /// <summary>
        /// возвращает число в мегабайтах
        /// </summary>
        /// <param name="size">число для преобразования в мегабайты</param>
        /// <param name="decimals">количество знаков после запятой</param>
        /// <returns></returns>
        public static decimal ToMB(this long size, int decimals = 2)
        {
            return Math.Round(size.ToKB(0) / 1024, decimals);
        }
        /// <summary>
        /// возвращает число в гигобайтах
        /// </summary>
        /// <param name="size">число для преобразования в гигобайты</param>
        /// <param name="decimals">количество знаков после запятой</param>
        /// <returns></returns>
        public static decimal ToGB(this long size, int decimals = 2)
        {
            return Math.Round(size.ToMB(0) / 1024, decimals);
        }
        /// <summary>
        /// возвращает число в терабайтах
        /// </summary>
        /// <param name="size">число для преобразования в теробайты</param>
        /// <param name="decimals">количество знаков после запятой</param>
        /// <returns></returns>
        public static decimal ToTB(this long size, int decimals = 2)
        {
            return Math.Round(size.ToGB(0) / 1024, decimals);
        }
    }
}
