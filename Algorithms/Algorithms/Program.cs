using Algorithms.DataStruct.Bag;
using Algorithms.Practice;
using Algorithms.Typical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BinarySearch.RecursionRank(5, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 });
            //BinarySearch.RecursionRank(5, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });

            // 算数表达式
            //while (true)
            //{
            //    MathmaticExpression.Evaluate();
            //}

            // Bag
            var bag = new ChainBag<int>
            {
                1, 2, 3, 4, 5
            };

            foreach (var item in bag)
            {
                Console.WriteLine(item);
            }
        }
    }
}
