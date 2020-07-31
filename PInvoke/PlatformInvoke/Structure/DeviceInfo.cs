using System.Runtime.InteropServices;

namespace HeyThings.SDK.Platform.Structs
{
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

        /// <summary>
        /// 转换为平台设备信息类型
        /// </summary>
        /// <returns>设备信息</returns>
        public devinfo_t ToPlatformStruct()
        {
            // 获取设备类型元数据
            var type = typeof(DeviceInfo);
            var props = type.GetProperties();

            // 设备结构体
            var devInfo = new devinfo_t
            {
                filed = new string_t[devinfo_t.Size],
            };

            // 设置devinfo_t结构
            //for (int i = 0; i < props.Length; i++)
            //{
            //    var prop = props[i];
            //    var valueObj = prop.GetValue(this);
            //    var value = (string)valueObj ?? string.Empty;
            //    DeviceInfoFunction.string_assign(out var st, value, value.Length);

            //    devInfo.filed[i] = st;
            //}

            return devInfo;
        }
    }
}
