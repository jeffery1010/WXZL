using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.DAL.IMGX
{
    using Entity.IMGX;
    using System.Data;
    using ERPWeb.Util.Helper;

    public class ConsoleDAL
    {
        private ConsoleDAL() { }

        public static List<Consoles> getConsole(Consoles record) {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@id", record.id);
            map.Add("@ip4", record.ip4);
            map.Add("@name", record.name);
            map.Add("@isvalid",record.isvalid);
            map.Add("@modelid",record.modelid);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_getconsole", map);
            List<Consoles> list = DataHelper.ToDataList<Consoles>(table);
            return list;
        }

        public static RBAC putConsole(Consoles record,string type)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@id", record.id);
            map.Add("@ip4", record.ip4);
            map.Add("@name", record.name);
            map.Add("@isvalid", record.isvalid);
            map.Add("@type", type);
            map.Add("@modelid", record.modelid);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_putconsole", map);
            RBAC result = DataHelper.ToDataList<RBAC>(table).FirstOrDefault<RBAC>();
            return result;
        }
    }
}
