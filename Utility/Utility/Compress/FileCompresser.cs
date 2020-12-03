using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Utility.Compress
{
    public class FileCompresser
    {
        /// <summary>
        /// 压缩指定的文件夹，但不包含文件夹
        /// </summary>
        /// <param name="folder">文件夹名称</param>
        /// <param name="targetFileName">目标文件名，后缀为.zip</param>
        public static void CompressFolder(string folder, string targetFileName)
            => ZipFile.CreateFromDirectory(folder, targetFileName);

        /// <summary>
        /// 解压指定的zip文件
        /// </summary>
        /// <param name="fileName">待解压的文件</param>
        /// <param name="targetFolder">目标文件夹</param>
        public static void ExtractToFolder(string fileName, string targetFolder)
            => ZipFile.ExtractToDirectory(fileName, targetFolder);
    }
}
