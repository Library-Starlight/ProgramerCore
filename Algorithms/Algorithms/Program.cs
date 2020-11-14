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

            //var obj = new Test { Value = 5 };
            //obj.Action();
            //Console.WriteLine(obj.Value);

            // case新语法
            //for (int i = 0; i < 100; i++)
            //{
            //    switch (i)
            //    {
            //        case >= 7 and <= 10 or 4 or >= 50 and <= 52:
            //            Console.WriteLine(i);
            //            break;
            //        default:
            //            break;
            //    }
            //}

            // 打印ASCII码表格
            //for (int i = byte.MinValue; i <= byte.MaxValue; i++)
            //{
            //    Console.Write($"\t{i}: {((char)i).ToString()}");
            //    if (i % 5 == 0)
            //        Console.WriteLine();
            //}

            // double类型字符串解析
            var s = "5.543";
            Console.WriteLine(double.Parse(s));
            Console.WriteLine(double.Parse(".034"));
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