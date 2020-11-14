using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Algorithms.Typical
{
    public class MathmaticExpression
    {
        /// <summary>
        /// 双栈算数表达式求值算法
        ///     数学的算术表达式如何在计算机中进行运算是计算机科学早期讨论的一个课题。
        ///     双栈算数表达是早期提出的一种简单的算法。
        ///     
        ///     后序表达式变体：Practice.InfixToPostfix
        /// </summary>
        /// <remarks>
        /// - by E.W.Dijkstra 
        /// </remarks>
        public static void Evaluate()
        {
            Console.WriteLine($"请输入算数表达式：");
            var expression = Console.ReadLine();
            Evaluate(expression);
        }

        public static void Evaluate(string expression)
        {
            // Test unit: (10 + ((1 + 2) * (3 * 4)))

            var operations = new Stack<char>();
            var variables = new Stack<double>();
            var sbValue = new StringBuilder();

            for (int i = 0; i < expression.Length; i++)
            {
                switch (expression[i])
                {
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                        operations.Push(expression[i]);
                        break;
                    case ')':
                        var operation = operations.Pop();
                        var right = variables.Pop();
                        switch (operation)
                        {
                            case '+': right = variables.Pop() + right; break;
                            case '-': right = variables.Pop() - right; break;
                            case '*': right = variables.Pop() * right; break;
                            case '/': right = variables.Pop() / right; break;
                            default:
                                break;
                        }
                        variables.Push(right);
                        break;
                    case >= '0' and <= '9':
                        do
                        {
                            sbValue.Append(expression[i]);
                        } while (++i < expression.Length && expression[i] is >= '0' and <= '9');
                        --i;
                        var value = double.Parse(sbValue.ToString());
                        sbValue.Clear();
                        variables.Push(value);
                        break;
                    default:
                        break;
                }
            }

            var result = variables.Pop();
            Console.WriteLine($"计算结果：{result.ToString()}");
        }
    }
}
