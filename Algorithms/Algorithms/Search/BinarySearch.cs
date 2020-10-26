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
                if (key < a[mid])       hi = mid - 1;
                else if (key > a[mid])  lo = mid + 1;
                else                    return mid;
            }

            return -1;
        }

        public static int RecursionRank(int key, int[] a)
        {
            return RecursionRank(key, a, 0, a.Length - 1, 0);
        }

        public static int RecursionRank(int key, int[] a, int lo, int hi, int depth)
        {
            System.Console.WriteLine($"{string.Empty.PadLeft(depth, ' ')}lo: {lo.ToString()}, hi: {hi.ToString()}");
            if (lo > hi) return -1;
            int mid = lo + (hi - lo) / 2;
            if (key < a[mid])      return RecursionRank(key, a, lo, mid - 1, ++depth);
            else if (key > a[mid]) return RecursionRank(key, a, mid + 1, hi, ++depth);
            else                   return mid;
        }
    }
}
