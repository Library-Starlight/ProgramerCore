using Algorithms.DataStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Algorithms.Test.DataStruct
{
    public class StackTest
    {
        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 })]
        [InlineData(new int[] { 7, 8, 3, 3, 0, 4, 5, 3, 2341, 4324, 11034 })]
        public void FixedCapacityStackTest(int[] values)
        {
            var stack = new FixedCapacityStack<int>(values.Length);
            Assert.True(stack.IsEmpty());

            for (int i = 0; i < values.Length; i++)
                stack.Push(values[i]);

            for (int i = 0; i < values.Length; i++)
            {
                var value = stack.Pop();

                var index = values.Length - 1 - i;
                Assert.Equal(values[index], value);
                Assert.Equal(index, stack.Count());
            }

            Assert.True(stack.IsEmpty());
        }
    }
}
