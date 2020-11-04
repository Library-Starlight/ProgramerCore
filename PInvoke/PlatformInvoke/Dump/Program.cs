using System;
using System.Runtime.InteropServices;

namespace PlatformInvoke
{
    class Program
    {
        static void Main(string[] args)
        {
            //var callback = new StatusCallback();
            //callback.online_callback = () => Console.WriteLine("Callback");

            //callback.ext_callback1 = (v, v1, v2, v3, v4) => Console.WriteLine($"ext1 called: {v.ToString()}, v1: {v1.ToString()}, v2: {v2.ToString()}, v3: {v3.ToString()}");
            //callback.ext_callback2 = v => Console.WriteLine($"ext2 called: {v.ToString()}");
            //callback.ext_callback3 = v => Console.WriteLine($"ext3 called: {v.ToString()}");

            //SetCallback(callback);

            // 查找回调函数数量
            var callback = new Callbacker();
            callback.c1 = () => Console.WriteLine($"c1");
            callback.c2 = () => Console.WriteLine($"c2");
            callback.c3 = () => Console.WriteLine($"c3");
            callback.c4 = () => Console.WriteLine($"c4");
            callback.c5 = () => Console.WriteLine($"c5");
            callback.c6 = () => Console.WriteLine($"c6");
            callback.c7 = () => Console.WriteLine($"c7");
            callback.c8 = () => Console.WriteLine($"c8");
            callback.c9 = () => Console.WriteLine($"c9");
            callback.c10 = () => Console.WriteLine($"c10");
            //callback.c11 = () => Console.WriteLine($"c11");
            //callback.c12 = () => Console.WriteLine($"c12");
            //callback.c13 = () => Console.WriteLine($"c13");
            //callback.c14 = () => Console.WriteLine($"c14");
            //callback.c15 = () => Console.WriteLine($"c15");
            //callback.c16 = () => Console.WriteLine($"c16");
            FindCallback(callback);

            Console.WriteLine($"End");
            Console.ReadLine();
        }

        public struct Callbacker
        {
            public delegate void callback();
            public callback c1;
            public callback c2;
            public callback c3;
            public callback c4;
            public callback c5;
            public callback c6;
            public callback c7;
            public callback c8;
            public callback c9;
            public callback c10;
            //public callback c11;
            //public callback c12;
            //public callback c13;
            //public callback c14;
            //public callback c15;
            //public callback c16;
        }

        [DllImport("demo0.so", EntryPoint = "SetCallback")]
        public extern static void FindCallback(Callbacker callback);

        [DllImport("demo0.so")]
        public extern static void SetCallback(StatusCallback callback);

        public struct StatusCallback
        {
            public delegate void online_callback_del();
            public online_callback_del online_callback;

            //public delegate void ext_callback_del1(uint value, int v1, int v2, int v3, int v4);
            //public ext_callback_del1 ext_callback1;

            //public delegate void ext_callback_del2(uint value);
            //public ext_callback_del2 ext_callback2;

            //public delegate void ext_callback_del3(uint value);
            //public ext_callback_del3 ext_callback3;

            //public delegate void extra_callback_del1(int v, int y);
            //public extra_callback_del1 extra_callback1;

            //public delegate void extra_callback_del3();
            //public extra_callback_del3 extra_callback3;

            //public delegate void extra_callback_del4();
            //public extra_callback_del4 extra_callback4;

            //public delegate void extra_callback_del5();
            //public extra_callback_del5 extra_callback5;

            //public delegate void extra_callback_del6();
            //public extra_callback_del6 extra_callback6;

            //public delegate void extra_callback_del7();
            //public extra_callback_del7 extra_callback7;

            //public delegate void extra_callback_del8();
            //public extra_callback_del8 extra_callback8;

            //public delegate void extra_callback_del9();
            //public extra_callback_del9 extra_callback9;
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
