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
    public class MachineSubmodelDAL
    {
        public static List<MachineSubmodel> getAllMachineSubmodel()
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            DataTable table = new DBHelper().GetDataTable("zp_mac_getMachineSubmodel", map);
            List<MachineSubmodel> list = DataHelper.ToDataList<MachineSubmodel>(table);
            return list;
        }

        public static List<LineSubmodel> getAllLineSubmodel()
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            DataTable table = new DBHelper().GetDataTable("zp_mac_getLineSubmodel", map);
            List<LineSubmodel> list = DataHelper.ToDataList<LineSubmodel>(table);
            return list;
        }
        public static List<GrtWastePercent> getAllGrtWaste(string line,string shift,string date)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@ModelLine", line);
            map.Add("@Shift", shift);
            map.Add("@Date", date);
            DataTable table = new DBHelper().GetDataTable2("wp_grt_tray_wastepercent", map);
            List<GrtWastePercent> list = DataHelper.ToDataList<GrtWastePercent>(table);
            return list;
        }
        /// <summary>
        /// 【0】code   【1】msg
        /// </summary>
        /// <param name="pcid"></param>
        /// <param name="newsubids"></param>
        /// <returns></returns>
        public static string[] PutMacSubRelation(int pcid,string newsubids)
        {
            try
            {
                Dictionary<string, object> map = new Dictionary<string, object>();
                map.Add("@PCId", pcid);
                map.Add("@NewSubIds", newsubids);
                DataTable table = new DBHelper().GetDataTable("zp_mac_putMacSubRelation", map);
                DataRow row = table.Rows[0];
                return new string[] { row[0].ToString(), row[1].ToString() };
            }
            catch (Exception ex)
            {
                return new string[] { "0", ex.Message };
            }
            
        }

        public static string[] PutMacLineSub(int submodel, string tbname)
        {
            try
            {
                string[] tbNameArr = tbname.Split(',');
                Dictionary<string, object> map = new Dictionary<string, object>();
                map.Add("@SubModel", submodel);
                map.Add("@tbNameNew", tbNameArr[0]);
                map.Add("@tbNameOld", tbNameArr[1]);
                DataTable table = new DBHelper().GetDataTable("zp_mac_putMacLineSub", map);
                DataRow row = table.Rows[0];
                return new string[] { row[0].ToString(), row[1].ToString() };
            }
            catch (Exception ex)
            {
                return new string[] { "0", ex.Message };
            }

        }

    }
}
