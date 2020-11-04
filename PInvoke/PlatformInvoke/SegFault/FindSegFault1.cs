using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
    using PlatformInvoke;

    public static class UniqueStringExtensions
    {

        //public static IntPtr ToStructurePointer(this unique_string unique)
        //{
        //    var elementSize = Marshal.SizeOf<child_string>();
        //    var size = elementSize * unique.array.Length;
        //    var pointer = Marshal.AllocHGlobal(size);
        //    Marshal.StructureToPtr(unique, pointer, true);
        //    return pointer;
        //}

        public static IntPtr ToStructurePointer(this unique_string value)
        {
            var size = Marshal.SizeOf(value);
            var pointer = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(value, pointer, true);
            return pointer;
        }
    }
}

namespace PlatformInvoke
{
    public struct unique_string
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public child_string[] array;
    }

    public struct child_string
    {
        public int len;
        public IntPtr ptr;
    }

    class FindSegFault1
    {
        public void Run()
        {
            // 子类型
            var word = "Hello World!";
            var data = Encoding.ASCII.GetBytes(word);
            var pointer = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, pointer, data.Length);
            var child_string1 = new child_string
            {
                len = data.Length,
                ptr = pointer,
            };

            word = "Good Bye!";
            data = Encoding.ASCII.GetBytes(word);
            pointer = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, pointer, data.Length);
            var child_string2 = new child_string
            {
                len = data.Length,
                ptr = pointer,
            };

            // 父类型
            var unique_string = new unique_string
            {
                array = new child_string[2],
            };
            unique_string.array[0] = child_string1;
            unique_string.array[1] = child_string2;

            //var uniquePtr = unique_string.ToStructurePointer();
            //var @struct = uniquePtr.ToStructure<unique_string>();
            //Console.WriteLine(@struct.array.Length);

            //var p1 = @struct.array[0].ptr;
            //var len1 = @struct.array[0].len;
            //var s1 = Marshal.PtrToStringUTF8(p1, len1);
            //Console.WriteLine(s1);

            //var p2 = @struct.array[1].ptr;
            //var len2 = @struct.array[1].len;
            //var s2 = Marshal.PtrToStringUTF8(p2, len2);
            //Console.WriteLine(s2);

            // 发送
            Console.WriteLine($"SendString");
            SendString(unique_string, 1);

            Console.WriteLine();
            Console.WriteLine();

            // 发送指针
            Console.WriteLine($"SendStringPointer");
            var ptr = unique_string.ToStructurePointer();
            SendStringPointer(ptr, 2);
        }

        #region SegFault

        [DllImport("demo0.so")]
        public extern static void SendString(unique_string str, int len);

        [DllImport("demo0.so")]
        public extern static void SendStringPointer(IntPtr str, int len);

        #endregion
    }
}
