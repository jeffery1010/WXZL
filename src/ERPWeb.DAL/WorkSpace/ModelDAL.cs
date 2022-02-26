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
    public class ModelDAL
    {
        private ModelDAL() { }

        public static List<Model> getModel(Model record)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@id", record.id);
            map.Add("@code", record.code);
            map.Add("@name", record.name);
            map.Add("@isvalid", record.isvalid);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_getmodel", map);
            List<Model> list = DataHelper.ToDataList<Model>(table);
            return list;
        }

        public static RBAC putModel(Model record, string type)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@id", record.id);
            map.Add("@code", record.code);
            map.Add("@name", record.name);
            map.Add("@isvalid", record.isvalid);
            map.Add("@type", type);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_putmodel", map);
            RBAC result = DataHelper.ToDataList<RBAC>(table).FirstOrDefault<RBAC>();
            return result;
        }
    }
}
