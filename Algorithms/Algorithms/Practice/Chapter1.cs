using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Practice
{
    public class Chapter1
    {
        public void P1_1_5(string[] args)
        {
            int[] values = args
                .Take(3)
                .Select(s => int.Parse(s))
                .ToArray();

            if (values[0] == values[1] && values[1] == values[2])
                Console.WriteLine("equal");
            else
                Console.WriteLine("not equal");
        }

        public void P1_1_6()
        {
            int f = 0;
            int g = 1;
            for (int i = 0; i <= 15; i++)
            {
                Console.WriteLine(f);
                f = f + g;
                g = f - g;
            }
        }

        public void P1_1_7()
        {
            // a
            //double t = 9.0;
            //while (Math.Abs(t - 9.0 / t) > 0.001)
            //{
            //    t = (t + 9.0 / t) / 2.0;
            //    //Console.WriteLine(t);
            //}
            //Console.WriteLine(t.ToString("0.00000"));

            // b
            //int sum = 0;
            //for (int i = 1; i < 1000; i++)
            //    for (int j = 0; j < i; j++)
            //        sum++;
            //Console.WriteLine(sum);

            // c
            int sum = 0;
            for (int i = 1; i < 1000; i *= 2)
                for (int j = 0; j < i; j++)
                    sum++;
            Console.WriteLine(sum);
        }

        public void P1_1_8()
        {
            Console.WriteLine('b');

            Console.WriteLine('b' + 'c');

            Console.WriteLine((char)('a' + 4));
        }
    }
}
