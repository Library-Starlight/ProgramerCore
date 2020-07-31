using System;
using System.Collections.Generic;
using System.Text;

namespace HeyThings.SDK.Platform.Structs
{
	/**
	 * local_type_t - Local communication message type
	 */
	internal enum local_type_t
	{
		LOCAL_TYPE_HEYTHINGS_FORWARD, //*< Forward the message, SDK will not process the message
		LOCAL_TYPE_HEYTHINGS_PROCESS, //*< SDK process the message or forward the msg by transaction id
		LOCAL_TYPE_DEV_STATUS, //*< SDK send the program device status
		LOCAL_TYPE_DEV_STATUS_REQ, //*< Program request the device status
		LOCAL_TYPE_DEV_INFO, //*< Program and SDK synchronization device information
		LOCAL_TYPE_DEV_INFO_REQ, //*< Program request the device information
		LOCAL_TYPE_CLIENT_DISCONNECT, //*< APP client disconnect
		LOCAL_TYPE_DEV_TIME, //*< SDK synchronization the cloud time and send to prgram
		LOCAL_TYPE_GET_PROPERTIES_REQ, //*< Program request to get service properties
		LOCAL_TYPE_GET_PROPERTIES_RESP, //*< SDK response the service proerties
		LOCAL_TYPE_SET_PROPERTIES_REQ, //*< Program change and set the service properties
		LOCAL_TYPE_SET_PROPERTIES_RESP, //*< SDK response the set result
		LOCAL_TYPE_OBSERVE_PROPERTIES_REQ, //*< Program observe service properties
		LOCAL_TYPE_OBSERVE_PROPERTIES_RESP, //*< SDK response the observe result
		LOCAL_TYPE_PROPERTIES_CHANGE, //*< When service properties change, SDK notification program
		LOCAL_TYPE_SUBSCIBE_MSG_BY_ADDRESS, //*< Program subscibe the heythings message by dst_addr
		LOCAL_TYPE_SUBSCIBE_MSG_BY_TYPE, //*< Program subscibe the heythings message by message type, except the properties message
		LOCAL_TYPE_SUBSCIBE_MSG_BY_SIID, //*< program subscibe the properties message by serivce id
		LOCAL_TYPE_SUBSCIBE_RESP, //*< subscibe response
		LOCAL_TYPE_UNSUBSCIBE_MSG_BY_ADDRESS, //*< unsubscibe the message by dst_addr
		LOCAL_TYPE_UNSUBSCIBE_MSG_BY_TYPE, //*< unsubscibe the message by type
		LOCAL_TYPE_UNSUBSCIBE_MSG_BY_SIID, //*< unsubscibe the message by service id
		LOCAL_TYPE_UNSUBSCIBE_RESP, //*< unsubscibe response
		LOCAL_TYPE_GENERATE_DEV_KEY_REQ,
		LOCAL_TYPE_GENERATE_DEV_KEY_RESP
	}

	/**
	 * dev_status_t - Device current status
	 */
	public enum dev_status_t
	{
		DEV_STATUS_MIN,

		DEV_STATUS_UNKNOW, //*< The SDK has some error

		DEV_STATUS_BIND_FAILED, //*< The SDK with cloud bind failed

		/// <summary>
		/// 初始化状态，此时HeyThings Linux SDK刚启动，这时开始监听各服务端口，初始化内部数据结构
		/// </summary>
		DEV_STATUS_INIT, //*< The SDK will start now

		/// <summary>
		/// 等待设备信息的状态，此时HeyThings Linux SDK等待其它的进程传输设备的信息，
		/// 如设备证书、设备did等。当传送过来的设备信息中的
		/// bind status 为1且具备最基本的信息时，这时设备开始正常连接云端切换状态到DEV_STATUS_CLOUD_CONNECTING。
		/// bind status 为0时，设备开始进行快连配网操作，切换状态至DEV_STATUS_WAIT_BINDINFO。
		/// </summary>
		DEV_STATUS_WAIT_DEVINFO, //*< The SDK wait other program send the devinfo

		/// <summary>
		/// 等待APP的快连配网信息(setup information)，当SDK广播此状态时，设备的其它本地进程应该根据HeyThings协议
		/// 中的规定操控硬件接收APP的配网信息，如使用soft-AP模式配网，则需要操控设备wifi进入AP模式，并根据HeyThings协议
		/// 规定将热点名称设成“HeyThings.[pid].[唯一标识].版本号”
		/// HeyThings: 固定字符，标识HeyThings生态智能设备
		/// pid：产品ID，由英文字母和数字组成
		/// 唯一标识：通常取mac地址后2个字节(string为后4字节)作为唯一标识，字母用小写
		/// 版本号：数字，从1开始编号版本
		/// 其它配网方式可参考HeyThings协议文档。
		/// </summary>
		DEV_STATUS_WAIT_BINDINFO, //*< The SDK wait APP send the setup information

		/// <summary>
		/// 设备正在bind中，SDK收到配网信息时，即进入此状态，此时设备开始与云端绑定服务器开始连接并发送bind 信息给云。
		/// </summary>
		DEV_STATUS_BINDING, //*< The SDK is binding now

		/// <summary>
		/// 设备正在与云端建立连接，当设备绑定成功后，设备开始连入云端时，会广播此状态。
		/// </summary>
		DEV_STATUS_CLOUD_CONNECTING, //*< The SDK connect the cloud now

		/// <summary>
		/// 设备与云端连接成功后，会广播此状态，表示SDK已经正常工作
		/// </summary>
		DEV_STATUS_NORMAL, //*< The SDK is normal, then can receive the cloud and app message

		/// <summary>
		/// 当SDK收到解绑消息时，会广播此状态，设备中的其它应用程序在收到此状态时，应该重置设备。
		/// </summary>
		DEV_STATUS_RESET, //*< The SDK is reset

		DEV_STATUS_MAX
	}

	/**
	 * HeyThings_handler_t - libheythings library process handler
	 */

	/**
	 * property_t - The service property 
	 */
	public struct property_t
	{
		public uint siid; //*< service id
		public uint iid; //*< property id
		public uint id; //*< The array property id
		public int length; //*< property value length
		public IntPtr data; //*< property value data
	}

	internal static class HeyThings_Info
	{
		public const int HEYTHINGS_VERSION_MAJOR = 1;
		public const int HEYTHINGS_VSERION_MINOR = 1;
		public const int HEYTHINGS_VERSION_PATCH = 0;
	}
}
