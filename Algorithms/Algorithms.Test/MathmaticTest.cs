using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Algorithms.Test
{
    public class MathmaticTest
    {
        [Theory]
        [InlineData(49, 7)]
        [InlineData(36, 6)]
        [InlineData(25, 5)]
        [InlineData(16, 4)]
        [InlineData(9, 3)]
        public void Sqrt(double value, double except)
        {
            const double err = 1e-15;
            var result = Mathmatic.Sqrt(value);
            Assert.True(result - value / result  <= err * result);
        }
    }
}
