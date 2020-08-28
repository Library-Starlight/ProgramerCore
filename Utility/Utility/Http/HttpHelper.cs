using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Utility.Http
{
    public static class HttpHelper
    {
        /// <summary>
        /// 在Url上添加请求参数
        /// </summary>
        /// <param name="url">基础Url</param>
        /// <param name="param">请求参数字典</param>
        /// <returns></returns>
        public static string AppendHttpGetParam(string url, IDictionary<string, string> param)
        {
            // 若无参数，则返回url
            if (param == null || param.Count <= 0)
                return url;

            var sb = new StringBuilder();
            sb.Append(url);
            var first = true;
            foreach (var kv in param)
            {
                // 对数据进行Url编码
                var encodedValue = WebUtility.UrlEncode(kv.Value);
                if (!first)
                {
                    sb.Append($"&{kv.Key}={encodedValue}");
                }
                else
                {
                    sb.Append($"?{kv.Key}={encodedValue}");
                    first = false;
                }
            }
            return sb.ToString();
        }
    }
}
