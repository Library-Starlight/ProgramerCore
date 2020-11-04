namespace System
{
    /// <summary>
    /// <see cref="Object"/>类型通用的方法，通常都使用了反射
    /// </summary>
    public static class ObjectHelper
    {
        /// <summary>
        /// 在控制台打印给定对象的元数据描述。
        /// 该方法用于调试和开发期间，对对象的运行时状态进行观察。
        /// </summary>
        /// <param name="obj">对象实例</param>
        public static void GetPropertyInfo(object obj)
        {
            var type = obj.GetType();

            var props = type.GetProperties();
            foreach (var prop in props)
                Console.WriteLine($"{prop.Name}: {prop.GetValue(obj)}");
        }

        /// <summary>
        /// 通过反射获取对象指定的属性值
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryGetPropertyValue(object obj, string propertyName, out object value)
        {
            var property = obj.GetType().GetProperty(propertyName);
            if (property == null)
            {
                value = default;
                return false;
            }

            value = property.GetValue(obj);

            // 除bool，string，数字类型外的其他类型需转换为字符串
            if (value == null)
            {
                value = string.Empty;
            }
            else if (value is DateTime time)
            {
                value = time.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (value is Enum)
            {
                value = value.ToString();
            }

            return true;
        }
    }
}
