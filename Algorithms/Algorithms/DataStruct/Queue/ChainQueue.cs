using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.DataStruct.Queue
{
    public class ChainQueue<T>
    {
        private Node _first;
        private Node _last;
        private int _count;

        public bool IsEmpty()
            => _count == 0;

        public int Count()
            => _count;

        /// <summary>
        /// 入列
        /// </summary>
        /// <param name="value"></param>
        public void Enqueue(T value)
        {
            // 添加到链表尾
            var node = new Node();
            node._value = value;
            if (_first == null)
                _first = _last = node;
            else
            {
                _last._next = node;
                _last = _last._next;
            }
            _count++;
        }

        /// <summary>
        /// 出列
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            // 从链表头取出
            var value = _first._value;
            _first = _first._next;
            if (IsEmpty()) _last = null;
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
