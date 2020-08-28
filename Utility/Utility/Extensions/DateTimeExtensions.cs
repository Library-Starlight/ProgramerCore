﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
    /// <summary>
    /// <see cref="DateTime"/>的扩展方法
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 零时刻
        /// </summary>
        private readonly static DateTime _zero = new DateTime(1970, 1, 1);

        /// <summary>
        /// 获取长整型时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ToInt64(this DateTime time)
        {
            return (long)(time - _zero).TotalMilliseconds;
        }

        public static ulong ToUint64(this DateTime time)
        {
            return (ulong)(time - _zero).TotalMilliseconds;
        }

        /// <summary>
        /// 将Utc整数值转换为时间
        /// </summary>
        /// <param name="value">需要转换为时间的Utc整数值</param>
        /// <returns></returns>
        public static DateTime UtcMillisecondsToTime(this long value)
        {
            return _zero.AddMilliseconds(value);
        }
    }
}
