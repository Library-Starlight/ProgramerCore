using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Algorithms.Helper
{
    public class RunningTimeWatcher : IDisposable
    {
        private readonly Stopwatch _sw;

        public RunningTimeWatcher()
        {
            _sw = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            _sw.Stop();
            Console.WriteLine($"Elapsed time: {_sw.Elapsed}");
        }
    }
}
