using HeyThings.SDK.Platform.Structs;
using Newtonsoft.Json;
using PlatformInvoke.Structure;
using System;
using System.IO;
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
                await RunDemoAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        #region 
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
    }
}
