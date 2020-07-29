using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static PlatformInvoke.Structure.StructureToPointer;

namespace PlatformInvoke.Structure
{
    public static class UniqueStringExtensions
    {
        public static IntPtr ToStructurePointer(this devinfo_t value)
        {
            var size = Marshal.SizeOf(value);
            var pointer = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(value, pointer, true);
            return pointer;
        }
    }

    public class StructureToPointer
    {
        public void Run()
        {
            var data = Pack();

            return;
            Unpack(data.ptr, data.len);
        }

        static void Assign()
        {
            var ts = "Hello World!";
            string_assign(out var st, ts, ts.Length);
            Console.WriteLine(st.len);
            var s = Marshal.PtrToStringAnsi(st.s);
            Console.WriteLine(s);
        }

        /// <summary>
        /// 打包
        /// </summary>
        static (IntPtr ptr, int len) Pack()
        {
            var device = new DeviceInfo()
            {
                DEVINFO_TYPE_DID = "005",
                DEVINFO_TYPE_DEV_PRI_KEY = "dsajfdscSjdfaDDSKCNkdajcnAS11AS_",
            };

            // 获取设备类型元数据
            var type = typeof(DeviceInfo);
            var props = type.GetProperties();


            // 设备结构体
            var devInfo = new devinfo_t
            {
                filed = new string_t[props.Length],
            };

            // 设置devinfo_t结构
            for (int i = 0; i < props.Length; i++)
            {
                var s = (string)props[i].GetValue(device) ?? string.Empty;
                string_assign(out var st, s, s.Length);

                devInfo.filed[i] = st;
            }

            for (int i = 0; i < devInfo.filed.Length; i++)
            {
                var field = devInfo.filed[i];
                var s = Marshal.PtrToStringUTF8(field.s, field.len);
            }

            // 打包为二进制数据
            var ptr = devInfo.ToStructurePointer();
            var pointer = HeyThings_devinfo_pack(ptr, 0, out var len);
            Console.WriteLine($"len: {len.ToString()}, ptr: {pointer.ToString()}");

            var data = new byte[len];
            Marshal.Copy(pointer, data, 0, len);
            Console.WriteLine($"Data: {BitConverter.ToString(data)}");

            return (pointer, len);
        }

        /// <summary>
        /// 解包
        /// </summary>
        static void Unpack(IntPtr data, int len)
        {
            Console.WriteLine($"Pointer: {data.ToString()}, len: {len.ToString()}");
            var unpackResult = HeyThings_devinfo_unpack(out var ptr, data, (ushort)len);

            Console.WriteLine($"Unpack result: {unpackResult}");
        }

        [DllImport("libheythings.so")]
        public static extern int HeyThings_devinfo_unpack(out IntPtr info, IntPtr data, ushort len);

        [DllImport("libheythings.so")]
        public static extern IntPtr HeyThings_devinfo_pack(IntPtr info, uint msgid, out ushort len);

        [DllImport("libheythings.so")]
        public static extern int string_assign(out string_t s, string ts, int len);

        /**
         * devino_t - The device information
         */
        public struct devinfo_t
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 43)]
            public string_t[] filed;
        }

        public struct string_t
        {
            public ushort len;
            public IntPtr s;
            //[MarshalAs(UnmanagedType.SafeArray)]
            //public byte[] s;
        }

        public class DeviceInfo
        {
            public string DEVINFO_TYPE_DID { get; set; }
            public string DEVINFO_TYPE_PID { get; set; }
            public string DEVINFO_TYPE_CID { get; set; }
            public string DEVINFO_TYPE_VID { get; set; }
            public string DEVINFO_TYPE_MAC { get; set; }
            public string DEVINFO_TYPE_MODEL { get; set; }
            public string DEVINFO_TYPE_MANUFACTURER { get; set; }
            public string DEVINFO_TYPE_BRAND { get; set; }
            public string DEVINFO_TYPE_SN { get; set; }
            public string DEVINFO_TYPE_ROOT_CERT { get; set; }
            public string DEVINFO_TYPE_DEV_CERT { get; set; }
            public string DEVINFO_TYPE_DEV_PRI_KEY { get; set; }
            public string DEVINFO_TYPE_PRODUCT_PRI_KEY { get; set; }
            public string DEVINFO_TYPE_ECDH_PRI_KEY { get; set; }
            public string DEVINFO_TYPE_PRODUCT_CERT { get; set; }
            public string DEVINFO_TYPE_PIN { get; set; }
            public string DEVINFO_TYPE_DEV_NAME { get; set; }
            public string DEVINFO_TYPE_BIND_STATUS { get; set; }
            public string DEVINFO_TYPE_CONNECT_TYPE { get; set; }
            public string DEVINFO_TYPE_CONFIG_TYPE { get; set; }
            public string DEVINFO_TYPE_SOFT_VER { get; set; }
            public string DEVINFO_TYPE_HARD_VER { get; set; }
            public string DEVINFO_TYPE_PARENT_DID { get; set; }
            public string DEVINFO_TYPE_SSID { get; set; }
            public string DEVINFO_TYPE_PSK { get; set; }
            public string DEVINFO_TYPE_BSSID { get; set; }
            public string DEVINFO_TYPE_RSSI { get; set; }
            public string DEVINFO_TYPE_IP { get; set; }
            public string DEVINFO_TYPE_NETMASK { get; set; }
            public string DEVINFO_TYPE_GATEWAY { get; set; }
            public string DEVINFO_TYPE_DEV_TIME { get; set; }
            public string DEVINFO_TYPE_TIMEZONE { get; set; }
            public string DEVINFO_TYPE_LOGITUDE { get; set; }
            public string DEVINFO_TYPE_LATITUDE { get; set; }
            public string DEVINFO_TYPE_CLOUD_URL { get; set; }
            public string DEVINFO_TYPE_ACCESS_URL { get; set; }
            public string DEVINFO_TYPE_HOMEID { get; set; }
            public string DEVINFO_TYPE_HOMEADDRESS { get; set; }
            public string DEVINFO_TYPE_HOMESIGN { get; set; }
            public string DEVINFO_TYPE_HOMEPUBKEY { get; set; }
            public string DEVINFO_TYPE_MASTER_KEY { get; set; }
            public string DEVINFO_TYPE_EVENT_UUID_SEQ { get; set; }
            public string DEVINFO_TYPE_MAX { get; set; }
        }
    }
}
