using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Practice
{
    public class Chapter1
    {
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
