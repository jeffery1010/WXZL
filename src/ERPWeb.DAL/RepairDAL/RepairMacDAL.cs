using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPWeb.Util.Helper;

namespace ERPWeb.DAL.RepairDAL
{
    public class RepairMacDAL
    {
        public static Dictionary<string, string> getAllRepairMac()
        {
            DBHelper db = new DBHelper();
            Dictionary<string, object> map = new Dictionary<string, object>();
            DataTable table = db.GetDataTable("zp_rpr_getRepairMachine",map);
            Dictionary<string, string> resultMap = new Dictionary<string, string>();
            DataRow row = null;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                row = table.Rows[i];
                resultMap.Add(row["code"].ToString(), row["bkc"].ToString());
            }
            return resultMap;
        }
    }
}
