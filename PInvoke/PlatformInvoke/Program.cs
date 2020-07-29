using System;
using System.Runtime.InteropServices;

namespace PlatformInvoke
{
    public class Notifier
    {
        public void C1()
        {
            Console.WriteLine("C1");
        }

        public void C2()
        {
            Console.WriteLine("C2");
        }
    }

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
            callback.c1 = () => Console.WriteLine($"{DateTime.Now}: Call successded");
            callback.c2 = () => Console.WriteLine($"{DateTime.Now}: Call successded");
            callback.c3 = () => Console.WriteLine($"{DateTime.Now}: Call successded");
            callback.c4 = () => Console.WriteLine($"{DateTime.Now}: Call successded");

            var size = Marshal.SizeOf<Callbacker>();
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(callback, ptr, true);

            while (true)
            {
                Console.WriteLine("通过指针or结构体调用？1:指针, 2:结构体");
                var s = Console.ReadLine();
                var i = int.Parse(s);

                if (i == 1)
                {
                    // 传递指针
                    Console.WriteLine("传递指针");
                    FindCallback(ptr);
                }
                else if (i == 2)
                {
                    // 传递结构体
                    Console.WriteLine("传递结构体");
                    FindCallback(callback);
                }
            }

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
        }

        [DllImport("demo0.so", EntryPoint = "SetCallback")]
        public extern static void FindCallback(IntPtr callback);

        [DllImport("demo0.so", EntryPoint = "SetCallback")]
        public extern static void FindCallback(Callbacker callback);



        [DllImport("demo0.so")]
        public extern static void SetCallback([MarshalAs(UnmanagedType.FunctionPtr)]StatusCallback callback);

        public struct StatusCallback
        {
            //public delegate void online_callback_del();
            //public online_callback_del online_callback;

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
