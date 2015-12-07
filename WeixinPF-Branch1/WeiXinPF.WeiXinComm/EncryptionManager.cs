using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WeiXinPF.WeiXinComm
{
    public class EncryptionManager
    {
        /// <summary>
        /// 有密码的AES加密 
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <param name="password">加密的密码</param>
        /// <param name="iv">密钥</param>
        /// <returns></returns>
        public static string AESEncrypt(string text, string password, string iv)
        {
            var rijndaelCipher = new RijndaelManaged
                {
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7,
                    KeySize = 128,
                    BlockSize = 128
                };

            var pwdBytes = System.Text.Encoding.UTF8.GetBytes(password);

            var keyBytes = new byte[24];

            var len = pwdBytes.Length;

            if (len > keyBytes.Length) len = keyBytes.Length;

            System.Array.Copy(pwdBytes, keyBytes, len);

            rijndaelCipher.Key = keyBytes;


            var ivBytes = System.Text.Encoding.UTF8.GetBytes(iv);
            rijndaelCipher.IV = ivBytes;

            var transform = rijndaelCipher.CreateEncryptor();

            var plainText = Encoding.UTF8.GetBytes(text);

            var cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);

            return Convert.ToBase64String(cipherBytes);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="password"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string AESDecrypt(string text, string password, string iv)
        {
            var rijndaelCipher = new RijndaelManaged
                {
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7,
                    KeySize = 128,
                    BlockSize = 128
                };

            var encryptedData = Convert.FromBase64String(text);

            var pwdBytes = System.Text.Encoding.UTF8.GetBytes(password);

            var keyBytes = new byte[24];

            var len = pwdBytes.Length;

            if (len > keyBytes.Length) len = keyBytes.Length;

            System.Array.Copy(pwdBytes, keyBytes, len);

            rijndaelCipher.Key = keyBytes;

            var ivBytes = System.Text.Encoding.UTF8.GetBytes(iv);
            rijndaelCipher.IV = ivBytes;

            var transform = rijndaelCipher.CreateDecryptor();

            var plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);

            return Encoding.UTF8.GetString(plainText);
        }

        /// <summary>
        /// 生成IV
        /// </summary>
        /// <returns></returns>
        public static string CreateIV()
        {
            var rtnIVBuilder = new StringBuilder();
            var rep = 0;
            var num2 = DateTime.Now.Ticks + rep;
            rep++;
            var random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (var i = 0; i < 16; i++)
            {
                char ch;
                var num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                rtnIVBuilder.Append(ch);
            }
            return rtnIVBuilder.ToString();
        }

        /// <summary>
        /// 有密码的AES加密，密码取自AppSettings["EncryptPassword"]
        /// </summary>
        /// <param name="text">明文</param>
        /// <param name="iv">密钥</param>
        /// <returns></returns>
        public static string AESEncrypt(string text, string iv)
        {
            var password = System.Configuration.ConfigurationManager.AppSettings["EncryptPassword"];

            return AESEncrypt(text, password, iv);
        }

        /// <summary>
        /// 有密码的AES解密，密码取自AppSettings["EncryptPassword"]
        /// </summary>
        /// <param name="text"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string AESDecrypt(string text, string iv)
        {
            var password = System.Configuration.ConfigurationManager.AppSettings["EncryptPassword"];

            return AESDecrypt(text, password, iv);
        }
    }
}
