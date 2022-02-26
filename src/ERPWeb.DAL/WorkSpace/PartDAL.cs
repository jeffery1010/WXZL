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
    public class PartDAL
    {
        private PartDAL() { }

        public static List<Part> getPartByWS(Part record)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@code", record.code);
            map.Add("@wsidList", record.wsidList);
            map.Add("@isvalid", record.isvalid);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_getpart", map);
            List<Part> list = DataHelper.ToDataList<Part>(table);
            return list;
        }

        public static List<Part> getPart(Part record)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@modelid", record.modelid);
            map.Add("@wsid", record.wsid);
            map.Add("@code", record.code);
            map.Add("@isvalid", record.isvalid);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_getpartindex", map);
            List<Part> list = DataHelper.ToDataList<Part>(table);
            return list;
        }

        public static RBAC putPart(Part record, string type)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@id", record.id);
            map.Add("@code", record.code);
            map.Add("@detail", record.detail);
            map.Add("@wsid", record.wsid);
            map.Add("@isvalid", record.isvalid);
            map.Add("@type", type);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_putpart", map);
            RBAC result = DataHelper.ToDataList<RBAC>(table).FirstOrDefault<RBAC>();
            return result;
        }
    }
}
