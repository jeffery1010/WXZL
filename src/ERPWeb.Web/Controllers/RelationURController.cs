using ERPWeb.Entity.Power;
using ERPWeb.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ERPWeb.Web.Model.EasyUIModel;

namespace ERPWeb.Web.Controllers
{
    public class RelationURController : Controller
    {
        // GET: UserInRole
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetRoleListStrByUserId(int UserNo) {
            RBAC record = new RBAC();
            try
            {
                List<RoleInfo> list = new RoleController().GetRoleByUserId(UserNo);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < list.Count; i++)
                {
                    sb.Append("," + list[i].Id);
                }
                record.Code = 200;
                record.Message = "Success";
                record.Data = sb.ToString();
            }
            catch (Exception ex)
            {
                record.Code = 500;
                record.Message = ex.Message;
                record.Data = "Null";
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(record));
        }

        [HttpPost]
        public ActionResult SetRelationUR(int UserNo,string RoleListStr)
        {
            RBAC record = new RBAC();
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    System.Data.SqlClient.SqlParameter[] paramList = new System.Data.SqlClient.SqlParameter[2];
                    paramList[0] = new System.Data.SqlClient.SqlParameter("@UserNo", UserNo);
                    paramList[1] = new System.Data.SqlClient.SqlParameter("@RoleListStr", RoleListStr);
                    record=db.Database.SqlQuery<RBAC>("EXEC p_SetRelationUR @UserNo,@RoleListStr ", paramList).ToList().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                record.Code = 500;
                record.Message = ex.Message;
                record.Data = "Null";
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(record));
        }
    }
}