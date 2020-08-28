using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Utility.Secret
{
    public static class HmacSha256
    {
        /// <summary>
        /// HmacSha256加密
        /// </summary>
        /// <param name="message">明文</param>
        /// <param name="secret">密钥</param>
        /// <returns>密文</returns>
        private static byte[] Encrypt(string message, string appSecret)
        {
            var encoding = new UTF8Encoding();
            var keyByte = encoding.GetBytes(appSecret);
            var messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                var hashmessage = hmacsha256.ComputeHash(messageBytes);
                return hashmessage;
            }
        }
    }
}
