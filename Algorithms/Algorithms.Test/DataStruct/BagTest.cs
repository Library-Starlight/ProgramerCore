using Algorithms.DataStruct.Bag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Algorithms.Test.DataStruct
{
    public class BagTest
    {
        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 })]
        [InlineData(new int[] { 7, 8, 3, 3, 0, 4, 5, 3, 2341, 4324, 11034 })]
        public void IntTest(int[] values) => StartTest(values);

        [Theory]
        [InlineData(new double[] { 50.8D, 8.1D, double.MaxValue, double.MinValue, -1.0D })]
        [InlineData(new double[] { 5000.5D })]
        [InlineData(new double[] { })]
        public void DoubleTest(double[] values) => StartTest(values);

        [Fact]
        public void StringTest()
        {
            var values = new string[]
            {
                "Hello World!",
                ".NET is awesome!",
                "Open source is such a great ideal!",
            };

            StartTest(values);
        }

        private static void StartTest<T>(T[] values)
        {

            // 多次使用测试
            for (int i = 0; i < 100; i++)
            {
                var chainQueue = new ChainBag<T>();
                AssertBag(values, chainQueue);
            }
        }

        private static void AssertBag<T>(T[] values, dynamic bag)
        {
            for (int i = 0; i < values.Length; i++)
                bag.Add(values[i]);

            var enumerable = bag as IEnumerable<T>;
            Assert.Equal(values.Length, enumerable.Count());

            int j = values.Length;
            foreach (T item in bag)
            {
                Assert.Equal(values[--j], item);
            }
        }
    }
}
