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
    public class XPathDAL
    {
        public static List<XPath> getXPath(XPath record)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@id", record.id);
            map.Add("@macid", record.macid);
            map.Add("@xpath", record.xpath);
            map.Add("@topath", record.topath);
            map.Add("@extend", record.extend);
            map.Add("@ratemin", record.ratemin);
            map.Add("@ratemax", record.ratemax);
            map.Add("@userno", record.userno);
            map.Add("@isvalid", record.isvalid);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_getxpath", map);
            List<XPath> list = DataHelper.ToDataList<XPath>(table);
            return list;
        }
        

        public static RBAC putXPath(XPath record, string type)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@id", record.id);
            map.Add("@macid", record.macid);
            map.Add("@xpath", record.xpath);
            map.Add("@topath", record.topath);
            map.Add("@extend", record.extend);
            map.Add("@rate", record.rate);
            map.Add("@userno", record.userno);
            map.Add("@isvalid", record.isvalid);
            map.Add("@type", type);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_putxpath", map);
            RBAC result = DataHelper.ToDataList<RBAC>(table).FirstOrDefault<RBAC>();
            return result;
        }
    }
}
