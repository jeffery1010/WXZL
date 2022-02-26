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
    public class UserController : BaseMvcController
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult GetUser(int page, int rows, UserInfo record)
        {
            List<UserInfo> list = new List<UserInfo>();
            int total = 0;
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    var data = db.UserInfo.Where(x=>true);
                    //人员编号  人员名  
                    if (record.UserNo != null) data = data.Where(x => x.UserNo.Contains(record.UserNo));
                    if (record.UserName != null) data = data.Where(x => x.UserName.Contains(record.UserName));
                    //电话  邮件 地址 身份证  手机
                    if (record.Tel != null) data = data.Where(x => x.Tel.Contains(record.Tel));
                    if (record.Email != null) data = data.Where(x => x.Email.Contains(record.Email));
                    if (record.Address != null) data = data.Where(x => x.Address.Contains(record.Address));
                    if (record.IdCard != null) data = data.Where(x => x.IdCard.Contains(record.IdCard));
                    if (record.MobilePhone != null) data = data.Where(x => x.MobilePhone.Contains(record.MobilePhone));
                    //可见  激活
                    if (record.IsVisible != -1) data = data.Where(x => x.IsVisible == record.IsVisible);
                    if (record.IsEnable != -1) data = data.Where(x => x.IsEnable==record.IsEnable);
                    total = data.Count();
                    list = data.OrderByDescending(x=>x.CreateTime).Skip((rows * page - rows)).Take(rows).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Json(new { total = total, rows = Json(list).Data });
            //return Json("{\"total\":\"" + total + "\",\"rows\":" + Newtonsoft.Json.JsonConvert.SerializeObject(list) + "}");
        }

        #region 基础增删改查
        public ActionResult Insert(UserInfo record)
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    record.Password = ERPWeb.Web.Helper.MD5Security.MD5Encrypt32(record.Password);
                    record.CreateTime = DateTime.Now;
                    db.UserInfo.Add(record);
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
                    UserInfo record = db.UserInfo.Find(Id);
                    db.UserInfo.Remove(record);
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
        public ActionResult Update(UserInfo record)
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    UserInfo tempRecord = db.UserInfo.Find(record.UserId);
                    tempRecord.UserNo = record.UserNo;
                    tempRecord.UserName = record.UserName;
                    tempRecord.RelationIdList = record.RelationIdList;
                    tempRecord.Tel = record.Tel;
                    tempRecord.Email = record.Email;
                    tempRecord.Address = record.Address;

                    tempRecord.IdCard = record.IdCard;
                    tempRecord.MobilePhone = record.MobilePhone;
                    tempRecord.BirthDay = record.BirthDay;
                    tempRecord.IsVisible = record.IsVisible;
                    tempRecord.IsEnable = record.IsEnable;

                    tempRecord.Password = ERPWeb.Web.Helper.MD5Security.MD5Encrypt32(record.Password);
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
            UserInfo record = null;
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    record = db.UserInfo.Find(Id);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Json(record);
        }
        public UserInfo SelectByCode(string UserNo) {
            UserInfo ui = null;
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    IQueryable<UserInfo> list = db.UserInfo.Where(x => x.UserNo.Equals(UserNo));
                    ui = list.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return ui;
        }
        #endregion

        
    }
}