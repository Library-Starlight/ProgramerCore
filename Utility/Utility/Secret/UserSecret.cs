using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Secret
{
    public class UserSecret
    {
        /// <summary>
        /// 机密存储：一般用于开发时存储和访问机密信息
        /// 机密文件路径：%APPDATA%\Roaming\Microsoft\UserSecrets\12b8d648-056b-4819-99d6-5ee3923d66f3
        ///  cmd: dotnet user-secrets set key 777
        ///  ref: https://www.cnblogs.com/nianming/p/7068253.html
        /// </summary>
        /// <returns></returns>
        public static string Get()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();

            builder.AddUserSecrets("12b8d648-056b-4819-99d6-5ee3923d66f3");
            var configuration = builder.Build();
            return configuration["key"].ToString();
        }
    }
}
