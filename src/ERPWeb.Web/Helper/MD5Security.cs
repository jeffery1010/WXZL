using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Web.Helper
{
    public class MD5Security
    { 
        public static string MD5Encrypt16(string str)
        {
            //计算输入数据的哈希值
            var md5 = new MD5CryptoServiceProvider();
            //value:字节数组。 startIndex:value 内的起始位置。length:要转换的 value 中的数组元素数。
            string securityStr = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(str)), 4, 8);
            securityStr = securityStr.Replace("-", "");
            return securityStr;
        }

        public static string MD5Encrypt32(string str)
        {
            //string securityStr = string.Empty;
            ////实例化一个md5对像
            //MD5 md5 = MD5.Create();
            //// 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            //byte[] byteList = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            //// 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            //for (int i = 0; i < byteList.Length; i++)
            //{
            //    // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
            //    securityStr += byteList[i].ToString("X");
            //}
            //return securityStr;

            MD5 md5 = new MD5CryptoServiceProvider();
            var t = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder(32);
            for (var i = 0; i < t.Length; i++)
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            md5.Dispose();
            return sb.ToString();
        }

        public static string MD5Encrypt64(string str)
        {
            MD5 md5 = MD5.Create(); //实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　                
            byte[] byteList = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            //ToBase64String 将 8 位无符号整数的数组转换为其用 Base64 数字编码的等效字符串表示形式。
            string securityStr = Convert.ToBase64String(byteList);
            return securityStr;
        } 
    }
}
