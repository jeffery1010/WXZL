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
    public class WorkStationCellDAL
    {
        public static List<WorkStationCell> getWorkStationCell(WorkStationCell record)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@id", record.id);
            map.Add("@wsid", record.wsid);
            map.Add("@cellid", record.cellid);
            map.Add("@isvalid", record.isvalid);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_getworkstationcell", map);
            List<WorkStationCell> list = DataHelper.ToDataList<WorkStationCell>(table);
            return list;
        }


        public static RBAC putWorkStationCell(WorkStationCell record, string type)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@id", record.id);
            map.Add("@wsid", record.wsid);
            map.Add("@cellid", record.cellid);
            map.Add("@isvalid", record.isvalid);
            map.Add("@type", type);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_putworkstationcell", map);
            RBAC result = DataHelper.ToDataList<RBAC>(table).FirstOrDefault<RBAC>();
            return result;
        }
    }
}
