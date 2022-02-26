using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Business.Common
{
    public class DBTypeConvert
    {

        public static string GetString(object obj)
        {
            if (obj == DBNull.Value) return "";
            else return obj.ToString();
        }
        public static DateTime GetDateTime(object obj)
        {
            if (obj == DBNull.Value) return DateTime.MinValue;
            else return Convert.ToDateTime(obj);
        }
        public static int GetInt(object obj)
        {
            if (obj == DBNull.Value) return 0;
            else return Convert.ToInt32(obj);
        }
    }
}
