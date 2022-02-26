using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ERPWeb.DataRepository;
using ERPWeb.Entity.SPC;
using ERPWeb.Util;

namespace ERPWeb.Business.Lot
{
    public class RepairBiz : BaseBusiness<Repair>
    { 
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<RepairModel> GetDataList(string fieldname, string keyword, Pagination pagination)
        {
            var where = LinqHelper.True<RepairModel>();
            List<System.Data.Common.DbParameter> ps = new List<System.Data.Common.DbParameter>();
            ps.Add(new SqlParameter("@name", keyword));
            ps.Add(new SqlParameter("@pagesize", pagination.PageRows));
            ps.Add(new SqlParameter("@pageindex", pagination.PageIndex));
            ps.Add(new SqlParameter("@sortfields", pagination.SortField+" "+ pagination.SortType)); 
            List<RepairModel> rx = GetListByProc<RepairModel>("wp_meta_getrepairdata", ps);
            pagination.rows = rx.Count;
            pagination.records = rx[0].total;
            return rx;

        }
        public static List<string> GetMacTypeNames(int id = 0)
        {
            RepairMacTypeBiz biz = new RepairMacTypeBiz();


            var q = from a in biz.GetDataList().AsQueryable<RepairMacTypeModel>()
                    where a.id == id
                    select a.MacTypeName;

             
            var list = q.ToList();

            return list;
        }
        public List<MacType> GetMacTypes(int id = 0)
        {
            MacTypeBiz biz = new MacTypeBiz();


            var q = from a in biz.GetDataList(id).AsQueryable<MacType>()                   
                    select a;


            var list = q.ToList();

            return list;
        }
        public List<MUnitModel> GetUnitList(string fieldname,string keywords,Pagination pp)
        {
            MUnitBiz biz = new MUnitBiz();


            var q = from a in biz.GetDataList(fieldname,keywords,pp).AsQueryable<MUnitModel>()
                    select a;


            var list = q.ToList();

            return list;
        }
        public List<RepairType> GetRepairTypeList(int id = 0)
        { 
            var q = from a in Service.GetIQueryable<RepairType>().AsQueryable<RepairType>()
                    where a.id == id || id ==0
                    select a;
             
            var list = q.ToList();

            return list;
        }
        /// <summary>
        /// 获取指定的单条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public RepairModel GetTheData(string id)
        {
            List<System.Data.Common.DbParameter> ps = new List<System.Data.Common.DbParameter>();
            ps.Add(new System.Data.SqlClient.SqlParameter("@id", id));
            List<RepairModel> rx= GetListByProc<RepairModel>("wp_meta_getrepairdata", ps);

            var rlst = from item in rx
                       where item.id == Convert.ToInt32(id)
                       select item;
            if (rlst.Count<RepairModel>() > 0)
                return rlst.ToList<RepairModel>()[0];
            else
                return new RepairModel();
           
        }
        public AjaxResult DeleteData(string ids) {
            List<System.Data.Common.DbParameter> ps = new List<System.Data.Common.DbParameter>(); 
            ps.Add(new System.Data.SqlClient.SqlParameter("@ids", ids)); 
            ps.Add(new System.Data.SqlClient.SqlParameter("@UserNo", GlobalM.UserBusiness.GetCurrentUser().UserNo));
            AjaxResult rx = SubmitDataByProc<RepairModel>("wp_rpr_repairsetting_remove", ps);
            return rx;
        }
        public AjaxResult SaveData(RepairModel model) {
            List<System.Data.Common.DbParameter> ps = new List<System.Data.Common.DbParameter>(); 
            ps.Add(new System.Data.SqlClient.SqlParameter("@desr", model.desr));
            ps.Add(new System.Data.SqlClient.SqlParameter("@mtids", model.mtids));
            ps.Add(new System.Data.SqlClient.SqlParameter("@unitids", model.unitids==null?"": model.unitids));
            ps.Add(new System.Data.SqlClient.SqlParameter("@model", model.model));
            ps.Add(new System.Data.SqlClient.SqlParameter("@id", model.id));
            ps.Add(new System.Data.SqlClient.SqlParameter("@effecttime", model.effecttime));
           
            ps.Add(new System.Data.SqlClient.SqlParameter("@inputtypeid", model.inputtypeid)); 
            ps.Add(new System.Data.SqlClient.SqlParameter("@repairtypecode", model.repairtypeCode));
            ps.Add(new System.Data.SqlClient.SqlParameter("@UserNo", GlobalM.UserBusiness.GetCurrentUser().UserNo));
            AjaxResult rx = SubmitDataByProc<RepairModel>("wp_rpr_repairsetting_change", ps);
            return rx;
        }
    }
    public class RepairModel : Repair, IModelDecorator<RepairModel>
    {
        public string repairtypeCode { get; set; } = "";
        public string repairtypeName { get; set; } = "";
        public string inputtypeName { get; set; } = "";
        public int modelid { get; set; } = 0;
        public int model { get; set; } = 0;
        
        public string mtids { get; set; } = "";
        public string unitids { get; set; } = "";
        public string mtnames { get; set; } = "";
        public string unitnamelist { get; set; } = "";
        /// <summary>
        /// 符合条件的总记录数量
        /// </summary>
        public int total { get; set; } = 0;
        public AjaxResult ParseSubmitResult(DataTable result)
        { 
          
           return new AjaxResult(result); 
        }

        public List<RepairModel> PopModelList(DataTable ds)
        {
            List<RepairModel> rlist = new List<RepairModel>();
            if (ds != null && ds.Rows.Count > 0) {
                foreach (DataRow dr in ds.Rows)
                {
                    RepairModel model = new RepairModel();
                    if (!Convert.IsDBNull(dr["chtime"])) model.chtime = Convert.ToDateTime(dr["chtime"]); else model.chtime = DateTime.MinValue;
                    model.desr = dr["desr"].ToString();
                    model.id = Convert.ToInt32(dr["id"]);
                    model.model = Convert.ToInt32(dr["model"]);
                    model.modelid = Convert.ToInt32(dr["modelid"]);
                    model.inputtypeid = Convert.ToInt32(dr["inputtypeid"]);
                    model.inputtypeName = dr["inputtypeName"].ToString();
                    model.mtids = dr["mtids"].ToString();
                    model.total = Convert.ToInt32(dr["total"]);
                    model.mtnames = dr["mtnames"].ToString();
                    model.unitnamelist = dr["unitnames"].ToString();
                    model.repairtypeCode = dr["repairtypeCode"].ToString();
                    model.repairtypeid = Convert.ToInt32(dr["repairtypeid"]);
                    model.repairtypeName = dr["repairtypeName"].ToString();
                    model.UserNo = dr["UserNo"].ToString();
                    model.effecttime = Convert.ToDateTime(dr["effecttime"]);
                    model.chtime = Convert.ToDateTime(dr["chtime"]);
                    rlist.Add(model);
                }
            }
            return rlist;
               
        }
    }
}


