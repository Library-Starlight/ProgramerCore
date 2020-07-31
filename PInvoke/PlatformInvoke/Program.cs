using HeyThings.SDK.Platform.Structs;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using PlatformInvoke.Structure;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlatformInvoke
{

    public class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var data = Encoding.UTF8.GetBytes("rtaK");

                // 以小端模式表示
                if (!BitConverter.IsLittleEndian)
                    Array.Reverse(data);

                var value = BitConverter.ToUInt32(data, 0);
                Console.WriteLine(BitConverter.ToString(data));
                Console.WriteLine(value);
                Console.WriteLine(value.ToString("X2"));
                Console.WriteLine(BitConverter.IsLittleEndian);

                var d = BitConverter.GetBytes(value);
                Console.WriteLine(BitConverter.ToString(d));

                return;

                Console.WriteLine(uint.MaxValue);
                var s = "rtaK";

                var ptr = Marshal.AllocHGlobal(s.Length);
                Console.WriteLine(ptr);



                var v = uint.Parse(ptr.ToString());

                Console.WriteLine(v);
                var uptr = new UIntPtr((ulong)v);
                Console.WriteLine(uptr);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        #region ReadJson

        private const string devinfo2 = "data/devinfo2.json";

        static async Task RunDemoAsync()
        {
            using (var fs = new FileStream(devinfo2, FileMode.Open, FileAccess.Read))
            using (var sr = new StreamReader(fs))
            {
                var s = await sr.ReadToEndAsync();
                Console.WriteLine(s);

                var devinfo = JsonConvert.DeserializeObject<devinfo_t>(s);
                var deviceInfo = devinfo.ToCLRClass();
            }
        }

        #endregion
    }
}
