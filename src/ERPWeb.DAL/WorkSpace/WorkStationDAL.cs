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
    public class WorkStationDAL
    {
        private WorkStationDAL() { }

        public static List<WorkStation> getWorkStation(WorkStation record)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@id", record.id);
            map.Add("@code", record.code);
            map.Add("@name", record.name);
            map.Add("@modelid", record.modelid);
            map.Add("@fsequence", record.fsequence);
            map.Add("@status", record.status);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_getworkstation", map);
            List<WorkStation> list = DataHelper.ToDataList<WorkStation>(table);
            return list;
        }
        

        public static RBAC putWorkStation(WorkStation record, string type)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@id", record.id);
            map.Add("@code", record.code);
            map.Add("@name", record.name);
            map.Add("@modelid", record.modelid);
            map.Add("@fsequence", record.fsequence);
            map.Add("@status", record.status);
            map.Add("@type", type);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_putworkstation", map);
            RBAC result = DataHelper.ToDataList<RBAC>(table).FirstOrDefault<RBAC>();
            return result;
        }
    }
}
