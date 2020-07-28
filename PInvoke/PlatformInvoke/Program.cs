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

            var callback = new StatusCallback();
            callback.online_callback = (v, v1) => Console.WriteLine(v.ToString());
            SetCallback(callback);
        }

        [DllImport("demo0.so")]
        public extern static void SetCallback(StatusCallback callback);

        public struct StatusCallback
        {
            public delegate void online_callback_del(uint value, int value1);
            public online_callback_del online_callback;
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
