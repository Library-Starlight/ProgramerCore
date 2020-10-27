using System;
using System.Collections.Generic;
using System.Text;
using static Algorithms.Mathmatic;

namespace Algorithms.Typical
{
    public class Sort
    {
        public static void SelectSort(IComparable[] a)
        {

        }

        /// <summary>
        /// 插入排序
        /// </summary>
        /// <param name="a"></param>
        public static void InsertSort(IComparable[] a)
        {
            for (int i = 1; i < a.Length; i++)
                for (int j = i; j > 0 && Less(a[j], a[j - 1]); j--)
                    Exch(a, j, j - 1);
        }
    }
}
