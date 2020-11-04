using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Typical
{
    public class Search
    {
        #region 二分查找

        /// <summary>
        /// 二分查找法
        /// </summary>
        /// <remarks>
        /// Order-of-Growth: 1+lg N
        /// </remarks>
        /// <param name="key">查找的数字</param>
        /// <param name="a">有序数组</param>
        /// <returns></returns>
        public static int BinarySearch(int key, int[] a)
        {
            int lo = 0;
            int hi = a.Length - 1;
            while (lo <= hi)
            {
                int mid = lo + (hi - lo) / 2;
                if (key < a[mid]) hi = mid - 1;
                else if (key > a[mid]) lo = mid + 1;
                else return mid;
            }

            return -1;
        }

        /// <summary>
        /// 二分查找法(递归)
        /// </summary>
        /// <param name="key">查找的数字</param>
        /// <param name="a">有序数组</param>
        /// <returns></returns>
        public static int RecursionBinarySearch(int key, int[] a)
            => RecursionBinarySearch(key, a, 0, a.Length - 1);

        /// <summary>
        /// 二分查找法(递归)
        /// </summary>
        /// <param name="key">查找的数字</param>
        /// <param name="a">有序数组</param>
        /// <param name="lo">当前查找左边界</param>
        /// <param name="hi">当前查找右边界</param>
        /// <param name="depth">调用栈深度</param>
        /// <returns></returns>
        public static int RecursionBinarySearch(int key, int[] a, int lo, int hi, int depth = 0)
        {
            System.Console.WriteLine($"{string.Empty.PadLeft(depth, ' ')}lo: {lo.ToString()}, hi: {hi.ToString()}");
            if (lo > hi) return -1;
            int mid = lo + (hi - lo) / 2;
            if (key < a[mid]) return RecursionBinarySearch(key, a, lo, mid - 1, ++depth);
            else if (key > a[mid]) return RecursionBinarySearch(key, a, mid + 1, hi, ++depth);
            else return mid;
        }

        #endregion


    }
}
