using DotNetty.Codecs.Base64;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Util.Helper
{
    public class DataTypeConvert
    {
        public byte[] Base64Str2Bytes(string base64str) {
            byte[] b =Convert.FromBase64String(base64str);
            // Encoding.Default.GetBytes(base64str);
            return b;
        }
        public string Bytes2Base64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes); 
        }
    }
}
