using Algorithms.Typical;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Practice
{
    public class Chapter2
    {
        /// <summary>
        /// 中序表达式转换为后序表达式求值
        /// </summary>
        public static void P1_3_10()
        {
            var infix = "(3*(1-5))";
            EvaluatePostfix(infix);
            infix = "((5 + ((60 * ( 3 - 1 )) / ( 6 - 4 ))) + 3)";
            EvaluatePostfix(infix);
        }

        /// <summary>
        /// 后序表达式求值
        /// </summary>
        /// <param name="infix"></param>
        private static void EvaluatePostfix(string infix)
        {
            var postfix = InfixToPostfix.Convert(infix);
            Console.WriteLine($"  Infix: {infix}");
            Console.WriteLine($"Postfix: {postfix}");
            var postfixQueue = InfixToPostfix.ConvertToQueue(infix);
            var result = InfixToPostfix.Evaluate(postfixQueue);
            Console.WriteLine($" Result: {result.ToString()}");
        }
    }
}
