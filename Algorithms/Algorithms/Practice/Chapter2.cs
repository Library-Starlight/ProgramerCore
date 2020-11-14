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
        public static void P1_3_10()
        {
            var infix = "(3*(1-5))";
            EvaluatePostfix(infix);
            infix = "((5 + ((60 * ( 3 - 1 )) / ( 6 - 4 ))) + 3)";
            EvaluatePostfix(infix);
        }

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
