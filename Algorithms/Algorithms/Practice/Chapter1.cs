using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Practice
{
    public class Chapter1
    {
        public static void P1_1_5(string[] args)
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

        public static void P1_1_6()
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

        public static void P1_1_7()
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

        public static void P1_1_8()
        {
            Console.WriteLine('b');

            Console.WriteLine('b' + 'c');

            Console.WriteLine((char)('a' + 4));
        }

        public static void P1_1_20()
        {
            var value = LgFactorial(10);
            Console.WriteLine(value);
        }

        private static double LgFactorial(int n)
        {
            var v = Factorial(n);
            return Math.Log(v);
        }

        private static int Factorial(int n)
        {
            if (n == 1) return 1;
            return n * Factorial(n - 1);
        }

        public static void P1_1_21(string[] args)
        {
            // args: "jack 60 100" "mike 50 100" "jane 77 100"
            foreach (var arg in args)
            {
                var parameters = arg.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var name = parameters[0];
                var v1 = int.Parse(parameters[1]);
                var v2 = int.Parse(parameters[2]);
                var percent = ((double)v1 / (double)v2).ToString("0.000");
                Console.WriteLine($"{name} {v1.ToString()} {v2.ToString()} {percent}");
            }
        }
    }
}
