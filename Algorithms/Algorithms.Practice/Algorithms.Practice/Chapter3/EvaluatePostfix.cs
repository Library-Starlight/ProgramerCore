using Algorithms.Typical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Practice.Chapter3
{
    /// <summary>
    /// P1.3.11
    /// </summary>
    public class EvaluatePostfix
    {
        public static void Start()
        {
            var value = MathmaticExpression.EvaluateExpression("5+3*4+5/2");
            Console.WriteLine(value);
        }
    }
}
