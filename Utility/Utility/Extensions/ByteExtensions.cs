namespace System
{
    /// <summary>
    /// 字节类型扩展方法
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// 获取字节数组的字符串表示
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetString(this byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", "");
        }
    }
}
