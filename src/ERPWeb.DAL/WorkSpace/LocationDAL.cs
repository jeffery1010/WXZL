using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPWeb.Entity.IMGX;
using ERPWeb.Util.Helper;

namespace ERPWeb.DAL.IMGX
{
    public class LocationDAL
    {
        private LocationDAL() { }

        public static List<Location> getRecord(Location record)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@id", record.id);
            map.Add("@name", record.name);
            map.Add("@features", record.features);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_getlocation", map);
            List<Location> list = DataHelper.ToDataList<Location>(table);
            return list;
        }

        public static RBAC putRecord(Location record, string type)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@id", record.id);
            map.Add("@name", record.name);
            map.Add("@features", record.features);
            map.Add("@type", type);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_putlocation", map);
            RBAC result = DataHelper.ToDataList<RBAC>(table).FirstOrDefault<RBAC>();
            return result;
        }
    }
}
