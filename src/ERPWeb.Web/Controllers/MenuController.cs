using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPWeb.Web.Controllers
{
    using Entity.Power;
    using Web.Model;
    using Web.Model.EasyUIModel;

    public class MenuController : Controller
    {
        // GET: Relation
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetMenu(int page, int rows, MenuInfo record)
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    var data = db.MenuInfo.Where(x=>true);
                    //编号 名称 
                    if (record.Code != null) data = db.MenuInfo.Where(x => x.Code.Contains(record.Code));
                    if (record.Name != null) data = db.MenuInfo.Where(x => x.Name.Contains(record.Name));
                    //父节点编号  url
                    if (record.ParentCode != null) data = data.Where(x => x.ParentCode.Contains(record.ParentCode));
                    if (record.XPath != null) data = data.Where(x => x.XPath.Contains(record.XPath));
                    //可见  激活
                    if (record.IsVisible != -1) data = data.Where(x => x.IsVisible == record.IsVisible);
                    if (record.IsEnable != -1) data = data.Where(x => x.IsEnable == record.IsEnable);

                    //数量
                    int total = data.Count();
                    //截取
                    data = data.OrderByDescending(x=>x.CreateTime).Skip((rows * page - rows)).Take(rows);
                    //挖掘转换
                    List<MenuInfo> list = GetJsonList(data.ToList());
                    return Json(new { total = total, rows = Json(list).Data });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<MenuInfo> GetMenuList(int page, int rows, MenuInfo record)
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    var data = db.MenuInfo.Where(x => true);
                    //编号 名称 
                    if (record.Code != null) data = db.MenuInfo.Where(x => x.Code.Contains(record.Code));
                    if (record.Name != null) data = db.MenuInfo.Where(x => x.Name.Contains(record.Name));
                    //父节点编号  url
                    if (record.ParentCode != null) data = data.Where(x => x.ParentCode.Contains(record.ParentCode));
                    if (record.XPath != null) data = data.Where(x => x.XPath.Contains(record.XPath));
                    //可见  激活
                    if (record.IsVisible != -1) data = data.Where(x => x.IsVisible == record.IsVisible);
                    if (record.IsEnable != -1) data = data.Where(x => x.IsEnable == record.IsEnable);
                    
                    //挖掘转换
                    List<MenuInfo> list = GetJsonList(data.ToList());
                    return list;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPost]
        public ActionResult GetDropDownListMenu()
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    List<MenuInfo> list = new List<MenuInfo>();
                    list.Add(new MenuInfo() { Code = "-1", Name = "--选择--" });
                    list.AddRange(db.MenuInfo);
                    var data = from x in list
                               select new
                               {
                                   Id = x.Code,
                                   Text = x.Name
                               };
                    return Json(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPost]
        public ActionResult GetMenuNoSearch(int rows, int page)
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    var data = db.MenuInfo.Where(x => 1 == 1);
                    int total = data.Count();
                    data = data.OrderBy(x => x.CreateTime).Skip((rows * page - rows)).Take(rows);
                    var result = from obj in data
                                 select new
                                 {
                                     obj.Id,
                                     obj.Code,
                                     obj.Name,
                                     obj.Remark,
                                     obj.ParentCode,
                                     obj.XPath,
                                     obj.CreateTime,
                                     obj.IsVisible,
                                     obj.IsEnable,
                                 };
                    return Json(new { total = total, rows = Json(result.ToList()).Data });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<MenuTree> GetMenuByUserNo(string UserNo) {
            List<MenuInfo> list = new List<MenuInfo>();
            List<MenuTree> resultList = new List<MenuTree>();
            using (UserManageContext db=new UserManageContext())
            {
                System.Data.SqlClient.SqlParameter[] paramList = new System.Data.SqlClient.SqlParameter[1];
                paramList[0] = new System.Data.SqlClient.SqlParameter("@UserNo", UserNo);
                list = db.Database.SqlQuery<MenuInfo>("EXEC BP_GetMenuByUserNo @UserNo ",paramList).ToList();
                resultList = new MenuTree().ChangeMenuToTree(list);
            }
            return resultList;
        }
        
        [HttpPost]
        public List<MenuTree> GetMenuByOU(int organizationId,int userId)
        {
            try
            {
                List<MenuInfo> list = new List<MenuInfo>();
                List<MenuTree> resultList = new List<MenuTree>();
                if (userId == -1)
                {
                    list = new MenuController().GetMenuList(1, 1000, new MenuInfo() { IsVisible = -1, IsEnable = -1 });
                }
                else
                {
                    using (UserManageContext db = new UserManageContext())
                    {
                        System.Data.SqlClient.SqlParameter[] paramList = new System.Data.SqlClient.SqlParameter[2];
                        paramList[0] = new System.Data.SqlClient.SqlParameter("@OrganizationId", organizationId);
                        paramList[1] = new System.Data.SqlClient.SqlParameter("@UserNo", userId);
                        list = db.Database.SqlQuery<MenuInfo>("EXEC p_GetMenuByOU @OrganizationId,@UserNo ", paramList).ToList();
                    }
                }
                resultList = new MenuTree().ChangeMenuToTree(list);
                return resultList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<MenuInfo> GetMenuByPowerId(int PowerId)
        {
            List<MenuInfo> resultList = null;
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    System.Data.SqlClient.SqlParameter[] paramList = new System.Data.SqlClient.SqlParameter[1];
                    paramList[0] = new System.Data.SqlClient.SqlParameter("@PowerId", PowerId);
                    resultList = db.Database.SqlQuery<MenuInfo>("EXEC p_GetMenuByPowerId @PowerId ", paramList).ToList();
                }
            }
            catch (Exception)
            {
                resultList = null;
            }
            return resultList;
        }


        #region 基本增删改查
        public ActionResult Insert(MenuInfo record)
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    if (record.Name == null) record.Name = "";
                    record.CreateTime = DateTime.Now;
                    db.MenuInfo.Add(record);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                //复合唯一键  constraint relationUniqueRO unique(RoleId,ResId)
                throw new Exception(e.Message);
            }
            return Content("YES");
        }
        public ActionResult Delete(int Id)
        {
            RBAC record = null;
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    System.Data.SqlClient.SqlParameter[] paramList = new System.Data.SqlClient.SqlParameter[1];
                    paramList[0] = new System.Data.SqlClient.SqlParameter("@ResId", Id);
                    record = db.Database.SqlQuery<RBAC>("EXEC p_DeleteMenu @ResId ", paramList).ToList().FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                record = new RBAC()
                {
                    Code = 500,
                    Message = e.Message,
                    Data = ""
                };
            }
            return Json(record);
        }
        public ActionResult Update(MenuInfo record)
        {
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    MenuInfo tempRecord = db.MenuInfo.Find(record.Id);
                    tempRecord.Code = record.Code;
                    tempRecord.Name = record.Name;
                    tempRecord.Remark = record.Remark;
                    tempRecord.ParentCode = record.ParentCode;
                    tempRecord.XPath = record.XPath;
                    tempRecord.IsVisible = record.IsVisible;
                    tempRecord.IsEnable = record.IsEnable;
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
            MenuInfo record = null;
            try
            {
                using (UserManageContext db = new UserManageContext())
                {
                    record = db.MenuInfo.Find(Id);
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
        private List<MenuInfo> GetJsonList(List<MenuInfo> allList)
        {
            List<MenuInfo> resultList = new List<MenuInfo>();
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
        private List<MenuInfo> ReadChildList(List<MenuInfo> allList, List<MenuInfo> list)
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