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
    }
}
