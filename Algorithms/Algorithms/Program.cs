using Algorithms.DataStruct.Bag;
using Algorithms.DataStruct.Chain;
using Algorithms.Practice;
using Algorithms.Typical;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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
            //var bag = new ChainBag<int>
            //{
            //    1, 2, 3, 4, 5
            //};

            //for (int i = 0; i < 10; i++)
            //{
            //    foreach (var item in bag as IEnumerable)
            //    {
            //        Console.WriteLine(item);
            //    }
            //    foreach (var item in bag as IEnumerable<int>)
            //    {
            //        Console.WriteLine(item);
            //    }
            //}

            //Chapter2.P1_3_10();

            // ?. 操作符
            //string v = null;
            //Console.WriteLine(v?.ToString()?.ToLower() == string.Empty);

            // 链表
            //var node = new Node<int>();
            //node.Value = 1;
            //node.Next = new Node<int>
            //{
            //    Value = 5,
            //    Next = new Node<int>
            //    {
            //        Value = 10,
            //    },
            //};

            //Node<int>.DeleteTail(node);

            var obj = new Test { Value = 5 };
            obj.Action();
            Console.WriteLine(obj.Value);
        }
    }

    public class Test
    {
        public int Value { get; set; }
    }

    public static class TestExtension
    {
        public static void Action(this Test obj)
        {
            obj = new Test { Value = 1 };
        }
    }
}