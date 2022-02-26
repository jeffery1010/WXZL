using ERPWeb.Business.GlobalM;
using ERPWeb.Entity.Power;
using ERPWeb.Entity.GlobalM;
using ERPWeb.Util;
using ERPWeb.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPWeb.Web.Controllers
{
    public class RoleController : BaseMvcController
    {
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult GetRole(int page, int rows, RoleInfo record)
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    //得到结果集
                    var data = db.RoleInfo.Where(x=>true);
                    //筛选
                    if (record.RoleCode != null) data = db.RoleInfo.Where(x => x.RoleCode.Contains(record.RoleCode));
                    if (record.RoleName != null) data = db.RoleInfo.Where(x => x.RoleName.Contains(record.RoleName));
                    if (record.IsVisible != -1) data = data.Where(x => x.IsVisible == record.IsVisible);
                    if (record.IsEnable != -1) data = data.Where(x => x.IsEnable == record.IsEnable);
                    //总数
                    int total = data.Count();
                    //截取
                    data = data.OrderByDescending(x=>x.CreateTime).Skip((rows * page - rows)).Take(rows);
                    //最终返回(左连接)
                    var result = from obj in data
                                 join u in db.UserInfo on obj.CreateUserId equals u.UserId into addUsered from addUser in addUsered.DefaultIfEmpty()
                                 select new
                                 {
                                     obj.Id,
                                     obj.RoleCode,
                                     obj.RoleName,
                                     obj.RoleRemark,
                                     obj.CreateTime,
                                     obj.CreateUserId,
                                     UserName = addUser != null ? addUser.UserName : "",
                                     obj.IsVisible,
                                     obj.IsEnable
                                 };
                    ActionResult R= Json(new { total = total, rows = Json(result.ToList()).Data });
                    return R;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPost]
        public ActionResult GetDropDownListRole()
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    List<RoleInfo> list = new List<RoleInfo>();
                    list.Add(new RoleInfo() { Id = -1, RoleName = "--选择角色--" });
                    list.AddRange(db.RoleInfo);
                    var data = from x in list
                               select new
                               {
                                   Id = x.Id,
                                   Text = x.RoleName
                               };
                    return Json(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<RoleInfo> GetRoleByUserId(int UserNo) {
            List<RoleInfo> resultList = null;
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    System.Data.SqlClient.SqlParameter[] paramList = new System.Data.SqlClient.SqlParameter[1];
                    paramList[0] = new System.Data.SqlClient.SqlParameter("@UserNo", UserNo);
                    resultList = db.Database.SqlQuery<RoleInfo>("EXEC p_GetRoleByUserId @UserNo ", paramList).ToList();
                }
            }
            catch(Exception)
            {
                resultList = null;
            }
            return resultList;
        }

        public ActionResult Insert(RoleInfo record)
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    record.CreateTime = DateTime.Now;
                    record.CreateUserId = 0;
                    db.RoleInfo.Add(record);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Content("YES");
        }
        public ActionResult Delete(int Id)
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    RoleInfo record = db.RoleInfo.Find(Id);
                    db.RoleInfo.Remove(record);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return Content("主外键存在约束关系");
                throw new Exception(e.Message);
            }
            return Content("YES");
        }
        public ActionResult Update(RoleInfo record)
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    RoleInfo tempRecord = db.RoleInfo.Find(record.Id);
                    tempRecord.RoleCode = record.RoleCode;
                    tempRecord.RoleName = record.RoleName;
                    tempRecord.RoleRemark = record.RoleRemark;
                    tempRecord.IsVisible = record.IsVisible;
                    tempRecord.IsEnable = record.IsEnable;
                    tempRecord.OrganizationId = record.OrganizationId;
                    //创建时间和创建人不更改
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Content("YES");
        }
        public ActionResult Select(int Id)
        {
            RoleInfo record = null;
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    record = db.RoleInfo.Find(Id);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Json(record);
        }

        
    }
}