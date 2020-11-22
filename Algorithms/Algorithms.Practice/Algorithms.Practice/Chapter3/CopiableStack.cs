using Algorithms.DataStruct.Chain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Practice.Chapter3
{
    /// <summary>
    /// 1.3.12
    /// </summary>
    public class CopiableStack : IEnumerable<char>
    {
        private Node<char> _first;

        private int _count;

        public CopiableStack()
        { }

        public int Count()
            => _count;

        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="value"></param>
        public void Push(char value)
        {
            var top = new Node<char> { Value = value };

            if (_first != null)
                top.Next = _first;

            _first = top;
            _count++;
        }

        /// <summary>
        /// 出栈
        /// </summary>
        /// <returns></returns>
        public char Pop()
        {
            _count--;

            var top = _first;
            _first = _first.Next;
            return top.Value;
        }

        public static CopiableStack Copy(CopiableStack original)
        {
            var stack = new CopiableStack();

            foreach (var item in original.Reverse())
                stack.Push(item);

            return stack;
        }

        public IEnumerator<char> GetEnumerator()
            => new CopiableStackEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public class CopiableStackEnumerator : IEnumerator<char>
        {
            private Node<char> current;

            public CopiableStackEnumerator(CopiableStack stack)
            {
                current = new Node<char>
                {
                    Next = stack._first,
                };
            }

            public char Current => current.Value;

            object IEnumerator.Current => Current;

            public void Dispose()
            { }

            public bool MoveNext()
            {
                current = current.Next;
                return current != null;
            }

            public void Reset()
            {

            }
        }

        #region 定制可复制堆栈

        public static void Start()
        {
            // 定义并初始化堆栈
            var stack = new CopiableStack();
            stack.Push('1');
            stack.Push('2');
            stack.Push('3');

            // 可用性测试

            //var count = stack.Count();
            //Console.WriteLine(count);

            //Console.WriteLine(stack.Pop());
            //Console.WriteLine(stack.Pop());
            //Console.WriteLine(stack.Pop());
            //Console.WriteLine(stack.Pop());

            // 习题
            var copy = CopiableStack.Copy(stack);

            foreach (var item in copy)
            {
                Console.WriteLine(item);
            }

            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(copy.Pop());
            Console.WriteLine(copy.Pop());
            Console.WriteLine(copy.Pop());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
        }

        #endregion
    }
}
