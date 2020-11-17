using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Utility.Extensions
{
    /* ********************************************************************************
     * 类名称: DatatableAndJson
     * 类描述:
     * 1.ToJson() 获取的DataTable 对象 转换为Json 字符串
     * 2.ToDataTable() Json 字符串 转换为 DataTable数据集合
     *
     * 创建人:
     * 创建时间:2016/3/30 13:50:02
     * 备注：
     * Version: 1.0
     *
     * ********************************************************************************
     */

    /// <summary>
    /// Datatable转Json
    /// </summary>
    public static class DatatableAndJson
    {

        /// <summary>
        /// DataTable 对象 转换为Json 字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToJson(this DataTable dt)
        {
            ArrayList arrayList = new ArrayList();
            foreach (DataRow dataRow in dt.Rows)
            {
                // 实例化一个参数集合
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName].ToString());
                }

                // ArrayList集合中添加键值
                arrayList.Add(dictionary);
            }

            // 返回一个json字符串
            return JsonConvert.SerializeObject(arrayList);
        }

        /// <summary>
        /// 提供简单的对象Json字符串反序列化对象的方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T FromJson<T>(this string obj) where T : class
        {
            if (obj != null)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(obj);

            return null;
        }
    }
}