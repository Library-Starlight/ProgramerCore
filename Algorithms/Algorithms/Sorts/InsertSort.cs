using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Algorithms.Mathmatic;

namespace Algorithms.Sorts
{
    public class InsertSort
    {
        /// <summary>
        /// 插入排序
        /// </summary>
        /// <remarks>
        /// Order-of-Growth: N²
        /// </remarks>
        /// <param name="a"></param>
        public static void Sort(IComparable[] a)
        {
            for (int i = 1; i < a.Length; i++)
                for (int j = i; j > 0 && Less(a[j], a[j - 1]); j--)
                    Exch(a, j, j - 1);
        }
    }
}
