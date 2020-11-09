using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.DataStruct.Bag
{
    public class ChainBag<T> : IEnumerable<T>
    {
        private Node _first;
        private int _count;

        public void Add(T value)
        {
            var oldNode = _first;
            _first = new Node
            {
                _value = value,
                _next = oldNode,
            };
            _count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ChainBagEnum(_first);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class ChainBagEnum : IEnumerator<T>
        {
            private Node _first;
            private Node _current;

            public ChainBagEnum(Node first)
            {
                _first = first;
                _current = new Node
                {
                    _next = _first,
                };
            }

            public T Current
            {
                get
                {
                    return _current._value;
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                _current = _current._next;
                return _current != null;
            }

            public void Reset()
            {
                _current = _first;
            }
        }

        private class Node
        {
            public T _value;
            public Node _next;
        }
    }
}
