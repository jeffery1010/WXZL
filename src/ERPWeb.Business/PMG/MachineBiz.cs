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

namespace ERPWeb.Business.PMG
{
    public class MachineBiz : BaseBusiness<Machine>
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<MachineModel> GetDataList(string fieldname, string keyword, Pagination pagination)
        {
            var where = LinqHelper.True<Machine>();
            List<System.Data.Common.DbParameter> ps = new List<System.Data.Common.DbParameter>();
            ps.Add(new SqlParameter("@name", keyword));
            ps.Add(new SqlParameter("@pagesize", pagination.PageRows));
            ps.Add(new SqlParameter("@pageindex", pagination.PageIndex));
            ps.Add(new SqlParameter("@sortfields", pagination.SortField + " " + pagination.SortType));
            List<MachineModel> rx = GetListByProc<MachineModel>("wp_pmg_getmachinelist", ps);
            pagination.rows = rx.Count;
            pagination.records = rx[0].total;
            return rx;

        }
         
        public List<RepairType> GetRepairTypeList(int id = 0)
        {
            var q = from a in Service.GetIQueryable<RepairType>().AsQueryable<RepairType>()
                    where a.id == id || id == 0
                    select a;

            var list = q.ToList();

            return list;
        }
        /// <summary>
        /// 获取指定的单条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public MachineModel GetTheData(string id)
        {
            List<System.Data.Common.DbParameter> ps = new List<System.Data.Common.DbParameter>();
            ps.Add(new System.Data.SqlClient.SqlParameter("@id", id));
            List<MachineModel> rx = GetListByProc<MachineModel>("wp_pmg_getmachinelist", ps);

            var rlst = from item in rx
                       where item.id == Convert.ToInt32(id)
                       select item;
            if (rlst.Count<MachineModel>() > 0)
                return rlst.ToList<MachineModel>()[0];
            else
                return new MachineModel();

        }
        public AjaxResult DeleteData(string ids)
        {
            List<System.Data.Common.DbParameter> ps = new List<System.Data.Common.DbParameter>();
            ps.Add(new System.Data.SqlClient.SqlParameter("@ids", ids));
            ps.Add(new System.Data.SqlClient.SqlParameter("@UserNo", GlobalM.UserBusiness.GetCurrentUser().UserNo));
            AjaxResult rx = SubmitDataByProc<MachineModel>("wp_pmg_machine_remove", ps);
            return rx;
        }
        public AjaxResult SaveData(MachineModel model)
        {
            List<System.Data.Common.DbParameter> ps = new List<System.Data.Common.DbParameter>(); 
            ps.Add(new System.Data.SqlClient.SqlParameter("@code", model.code));
            ps.Add(new System.Data.SqlClient.SqlParameter("@flag", model.flag));
            ps.Add(new System.Data.SqlClient.SqlParameter("@flag1", model.flag1));
            ps.Add(new System.Data.SqlClient.SqlParameter("@id", model.id));
            ps.Add(new System.Data.SqlClient.SqlParameter("@modeid", model.modeid));
            ps.Add(new System.Data.SqlClient.SqlParameter("@name", model.name));
            ps.Add(new System.Data.SqlClient.SqlParameter("@model", model.model));
            ps.Add(new System.Data.SqlClient.SqlParameter("@statusid", model.statusid)); 
            ps.Add(new System.Data.SqlClient.SqlParameter("@UserNo", GlobalM.UserBusiness.GetCurrentUser().UserNo));
            AjaxResult rx = SubmitDataByProc<MachineModel>("wp_pmg_machine_change", ps);
            return rx;
        }
    }
    public class MachineModel : Machine, IModelDecorator<MachineModel>
    {
        public string modename { get; set; } = "";
        public string statusname { get; set; } = ""; 

        public string flagalias { get; set; }
        public string flag1alias { get; set; }
        /// <summary>
        /// 符合条件的总记录数量
        /// </summary>
        public int total { get; set; } = 0;
        public AjaxResult ParseSubmitResult(DataTable result)
        { 
            return new AjaxResult(result);
        }

        public List<MachineModel> PopModelList(DataTable ds)
        {
            List<MachineModel> rlist = new List<MachineModel>();
            if (ds != null && ds.Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Rows)
                {
                    MachineModel model = new MachineModel();
                    if (!Convert.IsDBNull(dr["chtime"])) model.chtime = Convert.ToDateTime(dr["chtime"]); else model.chtime = DateTime.MinValue;
                    model.code = dr["code"].ToString();
                    model.flag = Convert.ToInt32(dr["flag"]);
                    model.flag1 = Convert.ToInt32(dr["flag1"]);
                    model.id = Convert.ToInt32(dr["id"]);
                    model.modeid = Convert.ToInt32(dr["modeid"]);
                    model.name = dr["name"].ToString();
                    model.statusid = Convert.ToInt32(dr["statusid"]);
                    model.model = Convert.ToInt32(dr["model"]);
                    model.userno = dr["userno"].ToString();
                    model.modename= dr["modename"].ToString();
                    model.statusname = dr["statusname"].ToString();
                    model.flagalias = dr["flagalias"].ToString();
                    model.flag1alias = dr["flag1alias"].ToString();
                    rlist.Add(model);
                }
            }
            return rlist;

        }
    }
}


