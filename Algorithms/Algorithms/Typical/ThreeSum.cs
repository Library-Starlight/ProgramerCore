using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Typical
{
    /// <summary>
    /// 给定一组不重复数字，计算其中3个数字相加为零的组合数
    /// </summary>
    public class ThreeSum
    {
        /// <summary>
        /// 暴力算法
        /// </summary>
        /// <remarks>
        /// Order-of-Growth: ½ N³
        /// </remarks>
        /// <param name="v"></param>
        /// <returns></returns>
        public static int Count(int[] v)
        {
            int count = 0;
            for (int i = 0; i < v.Length; i++)
                for (int j = i + 1; j < v.Length; j++)
                    for (int k = j + 1; k < v.Length; k++)
                        if (v[i] + v[j] + v[k] == 0)
                            count++;
            return count;
        }

        /// <summary>
        /// 优化算法
        /// </summary>
        /// <remarks>
        /// Order-of-Growth: N²log N
        /// </remarks>
        /// <param name="v"></param>
        /// <returns></returns>
        public static int Count1(int[] v)
        {
            return default;
        }
    }
}
