using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPWeb.Web.Controllers
{
    using ERPWeb.Entity.Power;
    using ERPWeb.Web.Model;

    public class PowerController : Controller
    {
        // GET: Power
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search() {
            return PartialView();
        }

        [HttpPost]
        public ActionResult GetPowerNoSearch(int rows, int page)
        {
            try
            {
                using (UserManageContext db=new UserManageContext())
                {
                    var data = db.PowerInfo.ToList();
                    int total = data.Count();
                    data= data.OrderBy(x => x.CreateTime).Skip((rows * page - rows)).Take(rows).ToList();
                    return Json(new { total = total, rows = Json(data).Data });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPost]
        public ActionResult GetDropDownListPower()
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    List<PowerInfo> list = new List<PowerInfo>();
                    list.Add(new PowerInfo() { Id = -1, PowerName = "--选择权限--" });
                    list.AddRange(db.PowerInfo);
                    var data = from x in list
                               select new
                               {
                                   Id = x.Id,
                                   Text = x.PowerName
                               };
                    return Json(Json(data.ToList()).Data);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public ActionResult GetPower(int page, int rows, PowerInfo record)
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    //得到结果集
                    var data = db.PowerInfo.Where(x=>true);
                    //筛选
                    if (record.PowerCode != null) data = db.PowerInfo.Where(x => x.PowerCode.Contains(record.PowerCode));
                    if (record.PowerName != null) data = db.PowerInfo.Where(x => x.PowerName.Contains(record.PowerName));
                    if (record.IsVisible != -1) data = data.Where(x => x.IsVisible == record.IsVisible);
                    if (record.IsEnable != -1) data = data.Where(x => x.IsEnable == record.IsEnable);
                    //总数
                    int total = data.Count();
                    //截取
                    data = data.OrderByDescending(x => x.CreateTime).Skip((rows * page - rows)).Take(rows);
                    //最终返回(左连接)
                    var result = from obj in data
                                 join u in db.UserInfo on obj.CreateUserId equals u.UserId into addUsered
                                 from addUser in addUsered.DefaultIfEmpty()
                                 select new
                                 {
                                     obj.Id,
                                     obj.PowerCode,
                                     obj.PowerName,
                                     obj.PowerRemark,
                                     obj.CreateTime,
                                     obj.CreateUserId,
                                     UserName = addUser != null ? addUser.UserName : "",
                                     obj.IsVisible,
                                     obj.IsEnable
                                 };
                    return Json(new { total = total, rows = Json(result.ToList()).Data });
                    //return Json("{\"total\":\"" + total + "\",\"rows\":" + Newtonsoft.Json.JsonConvert.SerializeObject(result) + "}");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PowerInfo> GetPowerByRoleId(int RoleId)
        {
            List<PowerInfo> resultList = null;
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    System.Data.SqlClient.SqlParameter[] paramList = new System.Data.SqlClient.SqlParameter[1];
                    paramList[0] = new System.Data.SqlClient.SqlParameter("@RoleId", RoleId);
                    resultList = db.Database.SqlQuery<PowerInfo>("EXEC p_GetPowerByRoleId @RoleId ", paramList).ToList();
                }
            }
            catch (Exception)
            {
                resultList = null;
            }
            return resultList;
        }

        #region 基本增删改查
        public ActionResult Insert(PowerInfo record)
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    record.CreateTime = DateTime.Now;
                    record.CreateUserId = 0;
                    db.PowerInfo.Add(record);
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
                    PowerInfo record = db.PowerInfo.Find(Id);
                    db.PowerInfo.Remove(record);
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
        public ActionResult Update(PowerInfo record)
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    PowerInfo tempRecord = db.PowerInfo.Find(record.Id);
                    tempRecord.PowerCode = record.PowerCode;
                    tempRecord.PowerName = record.PowerName;
                    tempRecord.PowerRemark = record.PowerRemark;
                    tempRecord.IsVisible = record.IsVisible;
                    tempRecord.IsEnable = record.IsEnable;
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
            PowerInfo record = null;
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    record = db.PowerInfo.Find(Id);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Json(record);
        }
        #endregion
    }
}