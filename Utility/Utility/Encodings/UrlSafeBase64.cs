using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Encodings
{
    public static class UrlSafeBase64
    {
        /// <summary>
        /// 获取Url安全Base64字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetString(byte[] data) 
            => Convert.ToBase64String(data).TrimEnd('=').Replace('+', '-').Replace('/', '_');
    }
}
