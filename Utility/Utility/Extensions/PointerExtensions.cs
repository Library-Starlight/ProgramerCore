using System.Runtime.InteropServices;

namespace System
{
    /// <summary>
    /// <see cref="IntPtr"/>扩展方法
    /// </summary>
    public static class PointerExtensions
    {
        #region 扩展方法

        /// <summary>
        /// 将对象实例转化为非托管的指针
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象实例</param>
        /// <returns>指向对象的指针</returns>
        //public static IntPtr ToPointer(this GatewayManager obj)
        //{
        //    var handle = GCHandle.Alloc(obj);
        //    return GCHandle.ToIntPtr(handle);
        //}

        /// <summary>
        /// 将指针转化为对象实例
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="pointer">指向对象的指针</param>
        /// <returns>结构体类型</returns>
        //public static T ToInstance<T>(this IntPtr pointer)
        //    where T: class
        //{
        //    var handle = GCHandle.FromIntPtr(pointer);
        //    return handle.Target as T;
        //}

        /// <summary>
        /// 将指针转化为结构体实例
        /// </summary>
        /// <typeparam name="T">结构体类型</typeparam>
        /// <param name="pointer">指向结构体的指针</param>
        /// <returns>结构体实例</returns>
        //public static T ToStructure<T>(this IntPtr pointer)
        //    where T: struct
        //    => Marshal.PtrToStructure<T>(pointer);

        #endregion
    }
}
