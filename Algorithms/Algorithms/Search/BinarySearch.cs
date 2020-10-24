using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Search
{
    /// <summary>
    /// 二分查找法
    /// </summary>
    public class BinarySearch
    {
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="key">查找的数字</param>
        /// <param name="a">有序数组</param>
        /// <returns></returns>
        public static int Rank(int key, int[] a)
        {
            int lo = 0;
            int hi = a.Length - 1;
            while (lo <= hi)
            {
                int mid = lo + (hi - lo) / 2;
                if (key < a[mid])      hi = mid - 1;
                else if (key > a[mid]) lo = mid + 1;
                else                   return mid;
            }

            return -1;
        }

        public static void Main(string[] args)
        {
            //Console.WriteLine(Math.Abs(-2147483648));

            //int i;
            //Console.WriteLine(i);

            //string s1 = "1";
            //string s2 = "2";
            //Console.WriteLine(s1 > s2);

            int[] values = args
                .Take(3)
                .Select(s => int.Parse(s))
                .ToArray();

            if (values[0] == values[1] && values[1] == values[2])
                Console.WriteLine("equal");
            else
                Console.WriteLine("not equal");
        }
    }
}
