using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xunit;
using static Algorithms.Typical.Sort;

namespace Algorithms.Test.Typical
{
    public class SortTest
    {
        [Theory(DisplayName = "插入排序")]
        [InlineData("8,7,6,5,4,3,2,1","1,2,3,4,5,6,7,8")]
        [InlineData("2,7,1,3,4,5,8,6","1,2,3,4,5,6,7,8")]
        [InlineData("2,7,1,3,2,5,8,6", "1,2,2,3,5,6,7,8")]
        [InlineData("2,5,1,3,2,5,8,6", "1,2,2,3,5,5,6,8")]
        public void InsertSortTest(string a, string excepted)
        {
            var values1 = StringHelper.GetValue<int>(a, ',', s => int.Parse(s)).ToArray();
            var values2 = StringHelper.GetValue<int>(excepted, ',', s => int.Parse(s)).ToArray();

            InsertSort(values1);
            for (int i = 0; i < values1.Length; i++)
            {
                Assert.Equal(values2[i], values1[i]);
            }

        }
    }
}
