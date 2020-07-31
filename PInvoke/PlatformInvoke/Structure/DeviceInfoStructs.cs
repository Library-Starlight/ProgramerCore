using System;
using System.Runtime.InteropServices;

namespace HeyThings.SDK.Platform.Structs
{
	/**
	 * string_t - The device information store struct
	 */
	public struct string_t
	{
		public ushort len;
		public IntPtr s;
	}

	/**
	 * devino_t - The device information
	 */
	public struct devinfo_t
	{
		public const int Size = 43;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = Size)]
		public string_t[] filed;

		public IntPtr ToPointer()
        {
			var size = Marshal.SizeOf(this);
			var pointer = Marshal.AllocHGlobal(size);
			Marshal.StructureToPtr(this, pointer, true);
			return pointer;
		}

		/// <summary>
		/// 转化为CLR类型
		/// </summary>
		/// <returns></returns>
		public DeviceInfo ToCLRClass()
        {
			var deviceInfo = new DeviceInfo();

			// 获取设备类型元数据
			var type = typeof(DeviceInfo);
			var props = type.GetProperties();

			// 设置DeviceInfo
			for (int i = 0; i < props.Length; i++)
			{
				var prop = props[i];
				var st = filed[i];

				// 空指针不解析
				if (st.s == IntPtr.Zero)
				{
					prop.SetValue(deviceInfo, string.Empty);
					continue;
				}

				// 从指针中获取字符串
				var value = Marshal.PtrToStringAnsi(st.s, st.len);

				// 设置属性值
				prop.SetValue(deviceInfo, value);
			}

			return deviceInfo;
		}
	}

}
