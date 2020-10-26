using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    /// <summary>
    /// 数学
    /// </summary>
    public class Mathmatic
    {
        /// <summary>
        /// 最大公约数
        /// </summary>
        /// <param name="p">非负整数1</param>
        /// <param name="q">非负整数2</param>
        /// <returns></returns>
        public static uint Gcd(uint p, uint q)
        {
            if (q == 0) return p;
            uint r = p % q;
            return Gcd(q, r);
        }

        /// <summary>
        /// 求根(牛顿迭代法)(误差1e-15)
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double Sqrt(double v)
        {
            const double err = 1e-15;

            if (v < 0) return double.NaN;
            var t = v;
            while (Math.Abs(t - v / t) > err * t)
                t = (v / t + t) / 2.0D;
            return t;
        }
    }
}
