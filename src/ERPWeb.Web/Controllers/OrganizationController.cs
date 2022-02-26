using ERPWeb.Entity.Power;
using ERPWeb.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPWeb.Web.Controllers
{
    public class OrganizationController : Controller
    {
        // GET: Organization
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetOrganization(int page, int rows, OrgInfo record)
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    var data = db.OrgInfo.Where(x => true);
                    //筛选
                    if (record.Name != null) data = data.Where(x => x.Name.Contains(record.Name));
                    if (record.VersionCode != null) data = data.Where(x => x.VersionCode.Contains(record.VersionCode));
                    if (record.AppCode != null) data = data.Where(x => x.AppCode.Contains(record.AppCode));
                    //状态
                    if (record.IsVisible != -1) data = data.Where(x => x.IsVisible == record.IsVisible);
                    if (record.IsEnable != -1) data = data.Where(x => x.IsEnable == record.IsEnable);

                    int total = data.Count();
                    data = data.OrderBy(x => x.CreateTime).Skip((rows * page - rows)).Take(rows);
                    //挖掘转换
                    List<OrgInfo> list = GetJsonList(data.ToList());
                    return Json(new { total = total, rows = Json(list).Data });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        [HttpPost]
        public List<OrgInfo> GetOrganizationByUserNo(string UserNo)
        {
            List<OrgInfo> resultList = new List<OrgInfo>();
            using (UserManageContext db = new UserManageContext())
            {
                System.Data.SqlClient.SqlParameter[] paramList = new System.Data.SqlClient.SqlParameter[1];
                paramList[0] = new System.Data.SqlClient.SqlParameter("@UserNo", UserNo);
                resultList = db.Database.SqlQuery<OrgInfo>("EXEC BP_GetOrgByUserNo @UserNo ", paramList).ToList();
            }
            return resultList;
        }
        public ActionResult GetDropDownListOrganization()
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    List<OrgInfo> list = new List<OrgInfo>();
                    list.Add(new OrgInfo() { Code = "-1", Name = "--选择组织--" });
                    list.AddRange(db.OrgInfo);
                    var data = from x in list
                               select new
                               {
                                   Id = x.Code,
                                   Text = x.Name,
                                   Key = x.Id,
                               };
                    return Json(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ActionResult GetDDLOrganizationByUserNo()
        {
            UserInfo record = Util.SessionHelper.Session["UserInfo"] as UserInfo;
            try
            {
                List<OrgInfo> resultList = null;
                using (UserManageContext db = new UserManageContext())
                {
                    System.Data.SqlClient.SqlParameter[] paramList = new System.Data.SqlClient.SqlParameter[1];
                    paramList[0] = new System.Data.SqlClient.SqlParameter("@UserNo", record.UserNo);
                    resultList = db.Database.SqlQuery<OrgInfo>("EXEC p_GetOrganizationByUserNo @UserNo ", paramList).ToList();
                }

                var data = from x in resultList
                           select new
                           {
                               Id = x.Id,
                               Text = x.Name,
                           };
                return Json(Newtonsoft.Json.JsonConvert.SerializeObject(data));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #region 基础增删改查
        public ActionResult Insert(OrgInfo record)
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    record.CreateTime = DateTime.Now;
                    record.UserId = 0;
                    db.OrgInfo.Add(record);
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
                    OrgInfo record = db.OrgInfo.Find(Id);
                    db.OrgInfo.Remove(record);
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
        public ActionResult Update(OrgInfo record)
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    OrgInfo tempRecord = db.OrgInfo.Find(record.Id);
                    tempRecord.Code = record.Code;
                    tempRecord.Name = record.Name;
                    tempRecord.Remark = record.Remark;
                    tempRecord.ParentCode = record.ParentCode;
                    tempRecord.IsVisible = record.IsVisible;
                    tempRecord.IsEnable = record.IsEnable;
                    tempRecord.VersionCode = record.VersionCode;
                    tempRecord.AppCode = record.AppCode;
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
            OrgInfo record = null;
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    record = db.OrgInfo.Find(Id);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Json(record);
        }
        #endregion

        #region 将list转换成JsonList
        /// <summary>
        /// 将list转换成JsonList
        /// </summary>
        /// <param name="allList"></param>
        /// <returns></returns>
        private List<OrgInfo> GetJsonList(List<OrgInfo> allList)
        {
            List<OrgInfo> resultList = new List<OrgInfo>();
            var code = from x in allList select x.Code;
            var parentCode = from x in allList select x.ParentCode;
            List<string> rootParentCodeList = new List<string>();
            //将根父节点都找出来
            foreach (var item in parentCode)
            {
                if (!code.Contains(item)) rootParentCodeList.Add(item);
            }
            //将第一层节点先加入要返回的结果集
            resultList.AddRange(allList.Where(x => rootParentCodeList.Contains(x.ParentCode)));
            resultList = ReadChildList(allList, resultList);
            return resultList;
        }
        /// <summary>
        /// 递归挖掘子节点
        /// </summary>
        /// <param name="allList"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<OrgInfo> ReadChildList(List<OrgInfo> allList, List<OrgInfo> list)
        {
            foreach (var item in list)
            {
                item.children.AddRange(allList.Where(x => x.ParentCode.Equals(item.Code)));
                if (item.children.Count > 0)
                {
                    item.children = ReadChildList(allList, item.children);
                }
            }
            return list;
        }
        #endregion

    }
}