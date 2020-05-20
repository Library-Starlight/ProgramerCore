using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflections.Metadata
{
    /// <summary>
    /// 在控制台打印给定对象的元数据描述。
    /// 该方法用于调试和开发期间，对对象的运行时状态进行观察。
    /// </summary>
    public class ObjectHelper
    {
        public static void GetPropertyInfo(object obj)
        {
            var type = obj.GetType();

            var props = type.GetProperties();
            foreach (var prop in props)
                Console.WriteLine($"{prop.Name}: {prop.GetValue(obj)}");
        }
    }
}
