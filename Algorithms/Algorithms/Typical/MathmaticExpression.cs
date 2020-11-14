using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Algorithms.Typical
{
    public class MathmaticExpression
    {
        #region 前期调研

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

        /// <summary>
        /// 计算算数表达式
        /// </summary>
        /// <param name="expression"></param>
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

        #endregion

        #region 公共方法

        /// <summary>
        /// 计算通用算数表达式
        ///     5 * (3 - 6) - (5 + 4) * (6 / 2) => 5 3 6 - * 5 4 + 6 2 / *
        /// </summary>
        /// <remarks>
        /// ref: https://blog.csdn.net/zsuguangh/article/details/6280863
        /// </remarks>
        /// <returns>运算结果</returns>
        public static double EvaluateExpression(string expression)
        {
            // 获取后序表达式
            var postfix = GetPostfix(expression);

            // 计算后序表达式
            return Evaluate(postfix);
        }

        /// <summary>
        /// 计算后序表达式值
        /// </summary>
        /// <param name="postfix">后序表达式</param>
        /// <returns></returns>
        public static double Evaluate(IEnumerable<ValueType> postfix)
        {
            var numbers = new Stack<double>();
            foreach (var current in postfix)
            {
                if (current is double value)
                    numbers.Push(value);
                else if (current is char operation)
                {
                    var v2 = numbers.Pop();
                    var v1 = numbers.Pop();
                    switch (operation)
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

        /// <summary>
        /// 获取后序表达式
        /// </summary>
        /// <remarks>
        /// 算法规则：
        ///     定义一个堆栈存放操作符，遍历算术表达式
        ///     ①当遇到数字的时候直接输出；
        ///     ②当遇到左括号时，放入栈中；
        ///     ③当遇到右括号时，将栈中操作符弹出，直到遇到左括号；
        ///     ④当遇到运算符时，输出栈顶的操作符，直到优先级低于当前操作符。最后将当前操作符入栈；
        ///     ⑤遍历结束后，将堆栈中剩余的操作符依次输出。
        /// </remarks>
        /// <param name="expression">算数表达式</param>
        /// <returns></returns>
        public static IEnumerable<ValueType> GetPostfix(string expression)
        {
            var operations = new Stack<char>();
            foreach (var element in GetElementEnumerable(expression))
            {
                if (element is double)
                    // 返回数字
                    yield return element;

                // 操作符
                else if (element is char operation)
                {
                    switch (operation)
                    {
                        case '(':
                            operations.Push(operation);
                            break;
                        case ')':
                            while (true)
                            {
                                var top = operations.Pop();
                                if (top == '(')
                                    break;
                                yield return top;
                            }
                            break;
                        case '+':
                        case '-':
                            while (operations.Count > 0)
                            {
                                var top = operations.Peek();
                                if (top is '*' or '/' or '+' or '-')
                                    yield return operations.Pop();
                                else
                                    break;
                            }
                            operations.Push(operation);
                            break;
                        case '*':
                        case '/':
                            while (operations.Count > 0)
                            {
                                var top = operations.Peek();
                                if (top is '*' or '/')
                                    yield return operations.Pop();
                                else
                                    break;
                            }
                            operations.Push(operation);
                            break;
                        default:
                            break;
                    }
                }
            }

            while (operations.Count > 0)
                // 返回剩余的操作符
                yield return operations.Pop();
        }

        /// <summary>
        /// 获取算术元素
        /// </summary>
        /// <remarks>
        /// 元素要么是<see cref="double"/>数字，要么是<see cref="char"/>操作符及左、右括号
        /// </remarks>
        /// <param name="expression">算数表达式</param>
        /// <returns>算术元素的枚举</returns>
        public static IEnumerable<ValueType> GetElementEnumerable(string expression)
        {
            var sbNum = new StringBuilder();
            for (int i = 0; i < expression.Length; i++)
            {
                switch (expression[i])
                {
                    case >= '(' and <= '+' or '-' or '/':
                        // 返回操作符或左、右括号
                        yield return expression[i];
                        break;
                    case >= '0' and <= '9' or '.':
                        do
                        {
                            sbNum.Append(expression[i]);
                        } while (++i < expression.Length && expression[i] is >= '0' and <= '9' or '.');
                        i--;
                        // 返回数字
                        yield return double.Parse(sbNum.ToString());
                        sbNum.Clear();
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion
    }
}
