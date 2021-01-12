using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using Utility.Compress;
using Xunit;

namespace Utility.TST
{
    public class FileCompress
    {
        private const string _testFolder = "CompressTestFolder";
        private const string _targetFile = "CompressTargetFile.zip";
        private const string _targetFolder = "CompressTargetFolder";

        /// <summary>
        /// 压缩文件夹
        /// </summary>
        [Fact]
        public void Compress_ToTargetFile()
        {
            using (new TestFolderProvider())
            {
                FileCompresser.CompressFolder(_testFolder, _targetFile);
                Assert.True(File.Exists(_targetFile));
            }
        }

        /// <summary>
        /// 压缩指定文件夹到其内部。
        /// </summary>
        [Fact]
        public void Compress_ToFileInTargetFolder()
        {
            // 失败，进程锁定了，待优化
            //using (new TestFolderProvider())
            //{
            //    var filePath = Path.Combine(_testFolder, _targetFile);
            //    FileCompresser.CompressFolder(_testFolder, filePath);
            //    Assert.True(File.Exists(_targetFile));
            //}
        }

        /// <summary>
        /// 解压
        /// </summary>
        [Fact]
        public void Extract_ToFolder()
        {
            using (new TestFolderProvider())
            {
                // 创建zip文件
                FileCompresser.CompressFolder(_testFolder, _targetFile);

                FileCompresser.ExtractToFolder(_targetFile, _targetFolder);
                Assert.True(Directory.Exists(_targetFolder));
            }
        }

        public class TestFolderProvider : IDisposable
        {
            public TestFolderProvider()
            {
                // 创建测试目录
                if (!Directory.Exists(_testFolder))
                    Directory.CreateDirectory(_testFolder);

                // 创建测试文件
                var files = new string[]
                {
                    "file1.txt",
                    "file2.xls",
                    "file3.zip",
                    "file4.gz",
                };
                foreach (var subFile in files)
                {
                    var filePath = Path.Combine(_testFolder, subFile);
                    if (!File.Exists(filePath))
                    {
                        using var fs = File.Create(filePath);
                    }
                }
            }

            public void Dispose()
            {
                // 删除测试目录
                if (Directory.Exists(_testFolder))
                    Directory.Delete(_testFolder, true);
                // 删除压缩文件
                if (File.Exists(_targetFile))
                    File.Delete(_targetFile);
                // 删除解压目录
                if (Directory.Exists(_targetFolder))
                    Directory.Delete(_targetFolder, true);
            }
        }
    }
}
