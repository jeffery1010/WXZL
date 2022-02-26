using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPWeb.Entity.RepairEntity;
using ERPWeb.Util.Helper;

namespace ERPWeb.DAL.RepairDAL
{
    public class RepairPortDAL
    {
        public static List<RepairPort> getAllepairPort()
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            DataTable table = new DBHelper().GetDataTable("zp_rpr_getRepairPort", map);
            List<RepairPort> list = DataHelper.ToDataList<RepairPort>(table);
            return list;
        }

        /// <summary>
        /// 新增/修改  修理品构成类型部品
        /// </summary>
        /// <param name="record">RepairPort</param>
        /// <param name="type">【INSERT】【UPDATE】</param>
        public static void putRepairPort(RepairPort record, string type)
        {
            try
            {
                Dictionary<string, object> map = new Dictionary<string, object>();
                map.Add("@Name", record.name);
                map.Add("@UnitNo", record.unitno);
                map.Add("@MachineCodes", record.machinecodes);
                map.Add("@Type", type);
                DataTable table = new DBHelper().GetDataTable("zp_rpr_putRepairPort", map);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
