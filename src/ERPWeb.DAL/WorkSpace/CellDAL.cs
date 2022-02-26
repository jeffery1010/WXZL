using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.DAL.IMGX
{
    using Entity.IMGX;
    using ERPWeb.Util.Helper;
    using System.Data;

    public class CellDAL
    {
        private CellDAL() { }

        public static List<Cell> getCell(Cell record)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@id", record.id);
            map.Add("@code", record.code);
            map.Add("@name", record.name);
            map.Add("@isvalid", record.isvalid);
            map.Add("@modelid", record.modelid);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_getcell", map);
            List<Cell> list = DataHelper.ToDataList<Cell>(table);
            return list;
        }

        public static RBAC putCell(Cell record, string type)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@id", record.id);
            map.Add("@code", record.code);
            map.Add("@name", record.name);
            map.Add("@isvalid", record.isvalid);
            map.Add("@type", type);
            map.Add("@modelid", record.modelid);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_putcell", map);
            RBAC result = DataHelper.ToDataList<RBAC>(table).FirstOrDefault<RBAC>();
            return result;
        }
    }
}
