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
    public class SearchDAL
    {
        public static List<Search> getRecord(
            string Model,string Line,
            string Mt,string Mac,
            string Location,string Judge,
            DateTime timeBegin,DateTime timeEne, 
            string type,string Cameras)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@Model", Model);
            map.Add("@Line", Line);
            map.Add("@Mt", Mt);
            map.Add("@Mac", Mac);
            map.Add("@Location", Location);
            map.Add("@Judge", Judge);
            map.Add("@timeBegin", timeBegin);
            map.Add("@timeEnd", timeEne);
            map.Add("@Type", type);
            map.Add("@Cameras", Cameras);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_searchfortime", map);
            List<Search> list = DataHelper.ToDataList<Search>(table);
            return list;
        }


        public static List<Search> getRecord(string Model,string Mt, string opid,string type)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@Model", Model);
            map.Add("@Mt", Mt);
            map.Add("@OPID", opid);
            map.Add("@Type", type);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_searchforop", map);
            List<Search> list = DataHelper.ToDataList<Search>(table);
            return list;
        }

        public static List<ExcelEntity> getExcel(string Model, DataTable tableIn) {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("@Model", Model);
            map.Add("@IdStrList", tableIn);
            DataTable table = new DBHelper("IMGX").GetDataTable("zp_imgx_SearchInfoByid", map);
            List<ExcelEntity> list = DataHelper.ToDataList<ExcelEntity>(table);
            return list;
        }
    }
}
