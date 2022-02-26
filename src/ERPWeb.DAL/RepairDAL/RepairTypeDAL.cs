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
    public class RepairTypeDAL
    {
        /// <summary>
        /// 获取所有修理品类别
        /// </summary>
        /// <returns></returns>
        public static List<RepairType> getAllRepairType()
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@model", 497);
            map.Add("@minCode", "00");
            map.Add("@maxCode", "99");
            DataTable table = new DBHelper().GetDataTable("zp_rpr_getRepairType",map);
            List<RepairType> list = DataHelper.ToDataList<RepairType>(table);
            return list;
        }

        /// <summary>
        /// 新增/修改  修理品构成类型
        /// </summary>
        /// <param name="record">RepairType</param>
        /// <param name="type">【INSERT】【UPDATE】</param>
        public static void putRepairType(RepairType record, string type)
        {
            try
            {
                Dictionary<string, object> map = new Dictionary<string, object>();
                map.Add("@Id", record.Id);
                map.Add("@Code", record.code);
                map.Add("@Name", record.name);
                map.Add("@MaxNum", record.maxnum);
                map.Add("@InputType", (record.inputtype==null?"":record.inputtype));
                map.Add("@PortList", (record.portlist == null ? "" : record.portlist));
                map.Add("@Type", type);
                DataTable table = new DBHelper().GetDataTable("zp_rpr_putRepairType",map);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string delRepairType(int id)
        {
            try
            {
                Dictionary<string, object> map = new Dictionary<string, object>();
                map.Add("@Id", id);
                DataTable table = new DBHelper().GetDataTable("zp_rpr_delRepairType", map);
                return table.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                return "NO:" + ex.Message;
            }
        }



    }
}
