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
    public class MacDAL
    {
        private MacDAL() { }

        public static List<Mac> getMacList(Mac record)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@id", record.id);
            map.Add("@ip4", record.ip4);
            map.Add("@name", record.name);
            map.Add("@wsid", record.wsid);
            map.Add("@isvalid", record.isvalid);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_getmac", map);
            List <Mac> list = DataHelper.ToDataList<Mac>(table);
            return list;
        }

        public static List<object> getMacList(string model,string wsIdList,string lineIdList)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@Model", model);
            map.Add("@WSIdList", wsIdList);
            map.Add("@LineList", lineIdList);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_getmacForTime", map);
            var data = from x in table.AsEnumerable()
                       select new
                       {
                           id = x.Field<int>("id"),
                           ip4 = x.Field<string>("ip4"),
                           name = x.Field<string>("name"),
                           wsid = x.Field<int>("wsid"),
                           wscode = x.Field<string>("wscode"),
                           wsname = x.Field<string>("wsname"),
                           modelname = x.Field<string>("modelname"),
                           chtime = x.Field<DateTime>("chtime"),
                           isvalid = x.Field<int>("isvalid"),
                       };
            List<object> list = data.ToList<object>();
            return list;
        }

        public static List<object> getMacObj(Mac record)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@id", record.id);
            map.Add("@ip4", record.ip4);
            map.Add("@name", record.name);
            map.Add("@wsid", record.wsid);
            map.Add("@isvalid", record.isvalid);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_getmac", map);
            var data = from x in table.AsEnumerable()
                       select new
                       {
                           id = x.Field<int>("id"),
                           ip4 = x.Field<string>("ip4"),
                           name = x.Field<string>("name"),
                           wsid = x.Field<int>("wsid"),
                           wscode = x.Field<string>("wscode"),
                           wsname = x.Field<string>("wsname"),
                           modelname = x.Field<string>("modelname"),
                           chtime = x.Field<DateTime>("chtime"),
                           isvalid = x.Field<int>("isvalid"),
                       };
            List<object> list = data.ToList<object>();
            return list;
        }

        public static RBAC putMac(Mac record, string type)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@id", record.id);
            map.Add("@ip4", record.ip4);
            map.Add("@name", record.name);
            map.Add("@wsid", record.wsid);
            map.Add("@isvalid", record.isvalid);
            map.Add("@type", type);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_putmac", map);
            RBAC result = DataHelper.ToDataList<RBAC>(table).FirstOrDefault<RBAC>();
            return result;
        }
    }
}
