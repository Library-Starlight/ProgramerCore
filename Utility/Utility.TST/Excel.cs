using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Utility.Excel;
using Xunit;

namespace Utility.TST
{
    public class Excel
    {
        [Fact]
        public void Excel_ItemsExportAsFile()
        {
            var items = new List<Row>
            {
               new Row{ Integer = 1, String = "H", Float = 7.777F },
               new Row{ Integer = 1, String = "H", Float = 7.777F },
               new Row{ Integer = 1, String = "H", Float = 7.777F },
               new Row{ Integer = 1, String = "H", Float = 7.777F },
               new Row{ Integer = 1, String = "H", Float = 7.777F },
               new Row{ Integer = 1, String = "H", Float = 7.777F },
               new Row{ Integer = 1, String = "H", Float = 7.777F },
            };

            var fileName = "ExcelExportTest.xls";
            ExcelHelper.Export(items, fileName);

            Assert.True(File.Exists(fileName), "Excel导出失败，文件不存在。");
            File.Delete(fileName);
        }

        public class Row
        {
            public int Integer { get; set; }

            public string String { get; set; }
            public float Float { get; set; }
        }
    }
}
