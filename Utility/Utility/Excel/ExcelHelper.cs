using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Utility.Excel
{
    public class ExcelHelper
    {
        /// <summary>
        /// 导出到Excel文件
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="data">数据列表</param>
        /// <param name="fileName">文件名称，后缀名为.xls</param>
        public static void Export<T>(IEnumerable<T> data, string fileName)
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Sheet1");

            // 获取需导出的属性
            var props = typeof(T).GetProperties();

            // 设置表头
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < props.Length; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(props[i].Name);
            }


            // 设置数据体
            var items = data.ToList();
            for (int index = 0; index < items.Count; index++)
            {
                T item = items[index];
                IRow dataRow = sheet.CreateRow(index + 1);
                for (int propIndex = 0; propIndex < props.Length; propIndex++)
                {
                    object value = props[propIndex].GetValue(item);
                    ICell cell = dataRow.CreateCell(propIndex);
                    cell.SetCellValue(value?.ToString());
                }
            }

            // 导出到文件
            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                byte[] buffer = ms.ToArray();

                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(buffer, 0, buffer.Length);
                    fs.Flush();
                }
            }
        }
    }
}
