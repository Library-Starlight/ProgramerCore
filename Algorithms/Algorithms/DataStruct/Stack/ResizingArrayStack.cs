using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.DataStruct.Stack
{
    public class ResizingArrayStack<T>
    {
        private T[] _array;

        private int _count;

        public ResizingArrayStack()
        {
            _array = new T[1];
        }

        public bool IsEmpty() => _count == 0;

        public int Count() => _count;

        public void Push(T value)
        {
            _array[_count++] = value;
            if (_count >= _array.Length)
                Resize(2 * _array.Length);
        }

        public T Pop()
        {
            var value = _array[--_count];
            _array[_count] = default;
            if (_count > 0 && _count <= _array.Length / 4)
                Resize(_array.Length / 2);
            return value;
        }

        private void Resize(int length)
        {
            var newArray = new T[length];
            for (int i = 0; i < _count; i++)
                newArray[i] = _array[i];
            _array = newArray;
        }
    }
}
