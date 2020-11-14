using Algorithms.Typical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Algorithms.Test.Typical
{
    /// <summary>
    /// 算术表达式求值
    /// </summary>
    public class MathmaticExpressionTest
    {
        [Theory]
        [InlineData("1+3*5", 16.0D)]
        [InlineData("1+3*5+20/4", 21.0D)]
        [InlineData("5*(4+1)/5+3*2", 11.0D)]
        [InlineData("5", 5.0D)]
        [InlineData("0", 0.0D)]
        //[InlineData("-10/2", -5.0D)]
        //[InlineData("-10*2", -20.0D)]
        public void Evaluate(string expression, double exceptd)
        {
            var result = MathmaticExpression.EvaluateExpression(expression);
            Assert.Equal(exceptd, result);
        }

        [Fact]
        public void Evaluate1()
        {

        }

        [Theory]
        [InlineData("1", 0)]
        [InlineData("1+2+3", 1)]
        [InlineData("5*4+2*3", 2)]
        [InlineData("5*(3+4)/5+6*7", 3)]
        [InlineData("5.0*6.7", 4)]
        public void GetPostfix(string expression, int index)
        {
            var excepted = ExceptedPostfixElementsList[index].ToList();
            var elements = MathmaticExpression.GetPostfix(expression).ToList();
            for (int i = 0; i < excepted.Count; i++)
                Assert.Equal(excepted[i], elements[i]);
        }

        private static IList<IEnumerable<ValueType>> ExceptedPostfixElementsList = new List<IEnumerable<ValueType>>
        {
            new List<ValueType> { 1.0D },
            new List<ValueType> { 1.0D, 2.0D, '+', 3.0D, '+' },
            new List<ValueType> { 5.0D, 4.0D, '*', 2.0D, 3.0D, '*', '+' },
            new List<ValueType> { 5.0D, 3.0D, 4.0D, '+', '*', 5.0D, '/', 6.0D, 7.0D, '*', '+' },
            new List<ValueType> { 5.0D, 6.7D, '*' },
        };

        [Theory]
        [InlineData("1", 0)]
        [InlineData("1+2+3", 1)]
        [InlineData("5*4+2*3", 2)]
        [InlineData("5*(3+4)/5+6*7", 3)]
        [InlineData("5.0*6.7", 4)]
        public void GetElementEnumerable(string expression, int index)
        {
            var excepted = ExceptedEnumElementsList[index].ToList();
            var elements = MathmaticExpression.GetElementEnumerable(expression).ToList();
            for (int i = 0; i < excepted.Count; i++)
                Assert.Equal(excepted[i], elements[i]);
        }


        private static IList<IEnumerable<ValueType>> ExceptedEnumElementsList = new List<IEnumerable<ValueType>>
        {
            new List<ValueType> { 1.0D },
            new List<ValueType> { 1.0D, '+', 2.0D, '+', 3.0D },
            new List<ValueType> { 5.0D, '*', 4.0D, '+', 2.0D, '*', 3.0D },
            new List<ValueType> { 5.0D, '*', '(', 3.0D, '+', 4.0D, ')', '/', 5.0D, '+', 6.0D, '*', 7.0D },
            new List<ValueType> { 5.0D, '*', 6.7D },
        };

    }
}
