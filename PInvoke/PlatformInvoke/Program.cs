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
            callback.online_callback = () => Console.WriteLine("Callback");
            SetCallback(callback);
        }

        [DllImport("demo0.so")]
        public extern static void SetCallback(StatusCallback callback);

        public struct StatusCallback
        {
            public delegate void extra_callback_del1(int v, int y);
            public extra_callback_del1 extra_callback1;

            public delegate void online_callback_del();
            public online_callback_del online_callback;


            //public delegate void extra_callback_del2();
            //public extra_callback_del2 extra_callback2;
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
