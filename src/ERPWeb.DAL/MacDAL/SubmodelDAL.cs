using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPWeb.Entity.MacEntity;
using ERPWeb.Util.Helper;

namespace ERPWeb.DAL.MacDAL
{
    public class SubmodelDAL
    {
        public static List<Submodel> getAllSubmodel()
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            DataTable table = new DBHelper().GetDataTable("zp_mac_getMachine", map);
            List<Submodel> list = DataHelper.ToDataList<Submodel>(table);
            return list;
        }
    }
}
