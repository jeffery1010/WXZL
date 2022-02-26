using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPWeb.Web.Controllers
{
    using ERPWeb.Entity.Power;
    using ERPWeb.Web.Model;
    using System.Text;
    using ERPWeb.Web.Model.EasyUIModel;

    public class RelationPMController : Controller
    {
        // GET: PowerInRes
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetMenuListStrByPowerId(int PowerId)
        {
            RBAC record = new RBAC();
            try
            {
                List<MenuInfo> list = new MenuController().GetMenuByPowerId(PowerId);
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
        public ActionResult SetRelationPM(int PowerId, string MenuListStr)
        {
            RBAC record = new RBAC();
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    System.Data.SqlClient.SqlParameter[] paramList = new System.Data.SqlClient.SqlParameter[2];
                    paramList[0] = new System.Data.SqlClient.SqlParameter("@PowerId", PowerId);
                    paramList[1] = new System.Data.SqlClient.SqlParameter("@MenuListStr", MenuListStr);
                    record = db.Database.SqlQuery<RBAC>("EXEC p_SetRelationPM @PowerId,@MenuListStr ", paramList).ToList().FirstOrDefault();
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