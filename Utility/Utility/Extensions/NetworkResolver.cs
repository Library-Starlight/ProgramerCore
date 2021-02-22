using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Net
{
    public class NetworkResolver
    {
        /// <summary>
        /// 解析服务器网络接口
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> ResolveNetworkInterfaces()
        {
            foreach (NetworkInterface netInt in NetworkInterface.GetAllNetworkInterfaces())
            {
                IPInterfaceProperties property = netInt.GetIPProperties();
                foreach (UnicastIPAddressInformation ip in property.UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        // Windows不包含
                        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                            && !ip.IsDnsEligible)
                        {
                            continue;
                        }

                        var ipaddr = ip.Address.ToString();
                        yield return ipaddr;
                    }
                }
            }
        }

        /// <summary>
        /// 获取服务器网络接口
        /// </summary>
        /// <returns></returns>
        public static List<IPAddress> GetHostNetworkInterface()
        {
            return System.Net.NetworkResolver
                .ResolveNetworkInterfaces()
                .Select(s => IPAddress.Parse(s))
                .ToList();
        }
    }
}
