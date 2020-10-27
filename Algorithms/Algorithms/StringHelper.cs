using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    public class StringHelper
    {
        /// <summary>
        /// 从字符串获取实例
        /// </summary>
        /// <typeparam name="T">预期数据类型</typeparam>
        /// <param name="s">用分隔符分隔的字符串</param>
        /// <param name="spliter">字符串分隔符</param>
        /// <param name="converter">从字符串转换为<typeparamref name="T"/>的类型转换器</param>
        /// <returns></returns>
        public static IEnumerable<T> GetValue<T>(string s, char spliter, Func<string, T> converter)
            => s.Split(spliter).Select(converter);
    }
}
