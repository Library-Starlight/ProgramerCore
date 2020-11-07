using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.DataStruct
{
    /// <summary>
    /// 定容栈
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FixedCapacityStack<T> 
    {
        /// <summary>
        /// 数据缓存
        /// </summary>
        private readonly T[] _array;

        /// <summary>
        /// 当前元素个数
        /// </summary>
        private int _count;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="capacity">容量</param>
        public FixedCapacityStack(int capacity)
        {
            _array = new T[capacity];
        }

        /// <summary>
        /// 栈是否为空
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
            => _count == 0;

        /// <summary>
        /// 元素个数
        /// </summary>
        /// <returns></returns>
        public int Count()
            => _count;

        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="value">元素项</param>
        public void Push(T value)
            => _array[_count++] = value;

        /// <summary>
        /// 出栈
        /// </summary>
        /// <returns>元素项</returns>
        public T Pop()
            => _array[--_count];
    }
}
