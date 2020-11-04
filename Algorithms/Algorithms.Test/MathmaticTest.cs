using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static Algorithms.Mathmatic;

namespace Algorithms.Test
{
    public class MathmaticTest
    {
        [Theory]
        [InlineData(1, 2, true)]
        [InlineData(2, 2, false)]
        [InlineData(3, 2, false)]
        public void LessTest(int i, int j, bool except)
        {
            var result = Less(i, j);
            Assert.Equal(except, result);
        }

        [Theory]
        [InlineData(49, 7)]
        [InlineData(36, 6)]
        [InlineData(25, 5)]
        [InlineData(16, 4)]
        [InlineData(9, 3)]
        public void SqrtTest(double value, double except)
        {
            const double err = 1e-15;
            var result = Sqrt(value);
            Assert.True(result - value / result  <= err * result);
        }
    }
}
