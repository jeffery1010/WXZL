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
    public class ReadSettingDAL
    {
        public static List<ReadSetting> getReadSetting(ReadSetting record)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@id", record.id);
            map.Add("@consoleid", record.consoleid);
            map.Add("@xpathid", record.xpathid);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_getreadsetting", map);
            List<ReadSetting> list = DataHelper.ToDataList<ReadSetting>(table);
            return list;
        }


        public static RBAC putReadSetting(ReadSetting record, string type)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@id", record.id);
            map.Add("@consoleid", record.consoleid);
            map.Add("@xpathid", record.xpathid);
            map.Add("@type", type);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_putreadsetting", map);
            RBAC result = DataHelper.ToDataList<RBAC>(table).FirstOrDefault<RBAC>();
            return result;
        }
    }
}
