using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Utility.TST
{
    public class UserSecret
    {
        [Fact]
        public void Test()
        {
            var key = Utility.Secret.UserSecret.Get();
            Assert.Equal("777", key);
        }
    }
}
