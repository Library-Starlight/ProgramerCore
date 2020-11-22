using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Typical
{
    public static class InfixToPostfix
    {
        /// <summary>
        /// 转换，将中序表达式转化为后序表达式
        ///     如：2 * ( 5 - 1 ) => 2 5 1 - *
        /// </summary>
        /// <remarks>
        /// ref：https://blog.csdn.net/weixin_40558287/article/details/86561057
        /// </remarks>
        /// <param name="expression">算数表达式</param>
        /// <returns></returns>
        public static string Convert(string expression)
        {
            var postfix = ConvertToQueue(expression);
            return postfix.Select(v => v.ToString()).Aggregate((prev, cur) => $"{prev} {cur}");
        }

        /// <summary>
        /// 转换，将中序表达式转化为后序表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>后续表达式队列，其中数字为<see cref="double"/>, 操作符为<see cref="char"/></returns>
        public static Queue<ValueType> ConvertToQueue(string expression)
        {
            var postfix = new Queue<ValueType>();

            var operations = new Stack<char>();
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
                        postfix.Enqueue(operations.Pop());
                        break;
                    case >= '0' and <= '9':
                        do
                        {
                            sbValue.Append(expression[i]);
                        } while (++i < expression.Length && expression[i] is >= '0' and <= '9');
                        --i;
                        var value = double.Parse(sbValue.ToString());
                        sbValue.Clear();
                        postfix.Enqueue(value);
                        break;
                    default:
                        break;
                }
            }

            return postfix;
        }

        /// <summary>
        /// 求值：对后序表达式进行求值
        /// </summary>
        /// <param name="postfix"></param>
        /// <returns></returns>
        public static double Evaluate(Queue<ValueType> postfix)
        {
            var numbers = new Stack<double>();

            while (postfix.Count > 0)
            {
                var current = postfix.Dequeue();

                // 数字
                if (current is double number)
                    numbers.Push(number);
                // 操作符
                else
                {
                    var v2 = numbers.Pop();
                    var v1 = numbers.Pop();
                    switch ((char)current)
                    {
                        case '+': numbers.Push(v1 + v2); break;
                        case '-': numbers.Push(v1 - v2); break;
                        case '*': numbers.Push(v1 * v2); break;
                        case '/': numbers.Push(v1 / v2); break;
                        default:
                            break;
                    }
                }
            }

            return numbers.Pop();
        }
    }
}
