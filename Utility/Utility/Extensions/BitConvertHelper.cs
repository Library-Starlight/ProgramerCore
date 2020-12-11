using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Extensions
{
    public static class BitConvertHelper
    {
        /// <summary>
        /// 将字节数组转换为Uint16
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="index">起始位置</param>
        /// <param name="isLittleEndian">是否为小端，若系统与该值指定的模式一样，则直接转换，否则需要将数组反转后转换</param>
        /// <returns>转换结果</returns>
        public static ushort ToUInt16(byte[] data, int index, bool isLittleEndian)
        {
            if (BitConverter.IsLittleEndian == isLittleEndian)
                return BitConverter.ToUInt16(data, index);
            var buffer = new byte[2];
            Array.Copy(data, index, buffer, 0, 2);
            buffer = buffer.Reverse().ToArray();
            return BitConverter.ToUInt16(buffer, 0);
        }

        /// <summary>
        /// 将字节数组转换为Uint16
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="index">起始位置</param>
        /// <param name="isLittleEndian">是否为小端，若系统与该值指定的模式一样，则直接转换，否则需要将数组反转后转换</param>
        /// <returns>转换结果</returns>
        public static uint ToUInt32(byte[] data, int index, bool isLittleEndian)
        {
            if (BitConverter.IsLittleEndian == isLittleEndian)
                return BitConverter.ToUInt32(data, index);
            var buffer = new byte[4];
            Array.Copy(data, index, buffer, 0, 4);
            buffer = buffer.Reverse().ToArray();
            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <summary>
        /// 获取<see cref="ushort"/>数据编码
        /// </summary>
        /// <param name="value">数据值</param>
        /// <param name="isLittleEndian">是否为小端，若系统与该值指定的模式一样，则直接转换，否则需要将数组反转后转换</param>
        /// <returns>数据数组</returns>
        public static byte[] GetBytes(ushort value, bool isLittleEndian)
        {
            var data = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian != isLittleEndian)
                data = data.Reverse().ToArray();
            return data;
        }

        /// <summary>
        /// 获取<see cref="uint"/>数据编码
        /// </summary>
        /// <param name="value">数据值</param>
        /// <param name="isLittleEndian">是否为小端，若系统与该值指定的模式一样，则直接转换，否则需要将数组反转后转换</param>
        /// <returns>数据数组</returns>
        public static byte[] GetBytes(uint value, bool isLittleEndian)
        {
            var data = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian != isLittleEndian)
                data = data.Reverse().ToArray();
            return data;
        }
    }
}
