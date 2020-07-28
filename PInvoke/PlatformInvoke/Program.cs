using System;
using System.Runtime.InteropServices;

namespace PlatformInvoke
{
    class Program
    {
        static void Main(string[] args)
        {
            Print();

            Invoke(Status_enum.state3);

            // Stack overflow.
            // Aborted(core dumped)
            var callback = new StatusCallback();
            SetCallback(callback);
        }

        [DllImport("demo0.so")]
        public extern static void SetCallback(StatusCallback callback);

        public struct StatusCallback
        {
            void online_callback(uint value)
            {
                Console.WriteLine(value);
            }
        }

        [DllImport("demo0.so")]
        public extern static void Invoke(Status_enum status);

        public enum Status_enum
        {
            state1,
            state2,
            state3,
        }

        [DllImport("demo0.so")]
        public extern static void Print();
    }
}
