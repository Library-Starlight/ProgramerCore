using System;
using Xunit;

namespace Utility.TST
{
    public class Timestamp
    {
        private const long LongTimestamp = 1507808040455;
        private static DateTime Time = new DateTime(2017, 10, 12, 11, 34, 00, 455);

        [Fact]
        public void GetTime()
        {
            var ts = DateTimeExtensions.UtcMillisecondsToTime(LongTimestamp);
            Assert.Equal(Time, ts);
        }
        
        [Fact]
        public void GetBeijingTime()
        {
            var ts = DateTimeExtensions.UtcMillisecondsToBeiJingTime(LongTimestamp);
            Assert.Equal(Time.AddHours(8D), ts);
        }

        [Fact]
        public void GetInteger()
        {
            var timestamp = DateTimeExtensions.ToInt64(Time);
            Assert.Equal(LongTimestamp, timestamp);
        }

    }
}
