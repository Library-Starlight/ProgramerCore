using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Algorithms.DataStruct.Stack
{
    /// <summary>
    /// 链表堆栈
    /// </summary>
    public class ChainStack<T>
    {
        private Node _first;
        private int _count;

        public bool IsEmpty()
            => _first == null;

        public int Count()
            => _count;

        public void Push(T value)
        {
            var oldNode = _first;
            _first = new Node()
            {
                _value = value,
                _next = oldNode,
            };
            _count++;
        }

        public T Pop()
        {
            var value = _first._value;
            _first = _first._next;
            _count--;
            return value;
        }

        private class Node
        {
            public T _value;
            public Node _next;
        }
    }
}
