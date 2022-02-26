using ERPWeb.Business.Cache;
using ERPWeb.Business.Common;
using ERPWeb.Entity.GlobalM;
using ERPWeb.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using ERPWeb.Entity.Power;

namespace ERPWeb.Business.GlobalM
{
    public class UserBusiness : BaseBusiness<UserInfo>
    {
        static Base_UserModelCache _cache { get; } = new Base_UserModelCache();

        #region 外部接口

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<Base_UserModel> GetDataList(string condition, string keyword, Pagination pagination)
        {
            var where = LinqHelper.True<Base_UserModel>();

            Expression<Func<UserInfo, Base_UserModel>> selectExpre = a => new Base_UserModel
            {

            };
            selectExpre = selectExpre.BuildExtendSelectExpre();

            var q = from a in GetIQueryable().AsExpandable()
                    select selectExpre.Invoke(a);
            
            //模糊查询
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
                q = q.Where($@"{condition}.ToString().Contains(@0)", keyword);
            pagination.SetOrderField("UserId");
            var list= q.Where(where).GetPagination(pagination).ToList();
            SetProperty(list);

            return list;

            void SetProperty(List<Base_UserModel> users)
            {
                //补充用户角色属性
                List<string> UserIds = users.Select(x => x.UserId.ToString()).ToList();
                var userRoles = (from a in Service.GetIQueryable<UserInRole>()
                                 join b in Service.GetIQueryable<RoleInfo>() on a.RoleId equals b.Id
                                 where UserIds.Contains(a.UserId.ToString())
                                 select new
                                 {
                                     a.UserId,
                                     RoleId=b.Id,
                                     b.RoleName
                                 }).ToList();
                users.ForEach(aUser =>
                {
                    aUser.RoleIdList = userRoles.Where(x => x.UserId == aUser.UserId).Select(x => x.RoleId.ToString()).ToList();
                    aUser.RoleNameList = userRoles.Where(x => x.UserId == aUser.UserId).Select(x => x.RoleName).ToList();
                });
            }
        }

        /// <summary>
        /// 获取指定的单条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public UserInfo GetTheData(string id)
        {
            return GetEntity(id);
        }

        public void AddData(UserInfo newData)
        {
            if (GetIQueryable().Any(x => x.UserNo == newData.UserNo))
                throw new Exception("该用户名已存在！");

            Insert(newData);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public void UpdateData(UserInfo theData)
        {
            if (theData.UserNo == "Admin" && Operator.UserId != theData.UserId.ToString())
                throw new Exception("禁止更改超级管理员！");

            Update(theData);
            _cache.UpdateCache(theData.UserId.ToString());
        }

        public void SetUserRole(int UserId, List<string> roleIds)
        {
            Service.Delete_Sql<UserInRole>(x => x.UserId == UserId);
            var insertList = roleIds.Select(x => new UserInRole
            {
                Id = 0,
                UserId = UserId,
                RoleId = int.Parse(x)
            }).ToList();

            Service.Insert(insertList);
            _cache.UpdateCache(UserId.ToString());
            PermissionManage.UpdateUserPermissionCache(UserId.ToString());
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public void DeleteData(List<string> ids)
        {
            var adminUser = GetTheUser("Admin");
            if (ids.Contains(adminUser.UserId.ToString()))
                throw new Exception("超级管理员是内置账号,禁止删除！");
            var UserIds = GetIQueryable().Where(x => ids.Contains(x.UserId.ToString())).Select(x => x.UserId.ToString()).ToList();

            Delete(ids);
            _cache.UpdateCache(UserIds);
        }

        /// <summary>
        /// 获取当前操作者信息
        /// </summary>
        /// <returns></returns>
        public static Base_UserModel GetCurrentUser()
        {
            return GetTheUser(Operator.UserId);
        }
        
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public static Base_UserModel GetTheUser(string UserId)
        {
            return _cache.GetCache(UserId);
        }

        public static List<string> GetUserRoleIds(string UserId="")
        {
            return GetTheUser(UserId).RoleIdList;
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="oldPwd">老密码</param>
        /// <param name="newPwd">新密码</param>
        public AjaxResult ChangePwd(string oldPwd,string newPwd)
        {
            AjaxResult res = new AjaxResult() { Success = true };
            string UserId = Operator.UserId;
            oldPwd = oldPwd.ToMD5String();
            newPwd = newPwd.ToMD5String();
            var theUser = GetIQueryable().Where(x => x.UserNo == UserId && x.Password == oldPwd).FirstOrDefault();
            if (theUser == null)
            {
                res.Success = false;
                res.Msg = "原密码不正确！";
            }
            else
            {
                theUser.Password = newPwd;
                Update(theUser);
            }

            _cache.UpdateCache(UserId);

            return res;
        }

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <param name="permissions">权限值</param>
        public void SavePermission(string UserId, List<string> permissions)
        {
            //Service.Delete_Sql<Base_PermissionUser>(x => x.UserId == UserId);
            //var roleIdList = Service.GetIQueryable<UserInRole>().Where(x => x.UserId == UserId).Select(x => x.RoleId).ToList();
            //var existsPermissions = Service.GetIQueryable<PowerInRole>()
            //    .Where(x => roleIdList.Contains(x.RoleId) && permissions.Contains(x.PermissionValue))
            //    .GroupBy(x => x.PermissionValue)
            //    .Select(x => x.Key)
            //    .ToList();
            //permissions.RemoveAll(x => existsPermissions.Contains(x));

            //List<Base_PermissionUser> insertList = new List<Base_PermissionUser>();
            //permissions.ForEach(newPermission =>
            //{
            //    insertList.Add(new Base_PermissionUser
            //    {
            //        Id = Guid.NewGuid().ToSequentialGuid(),
            //        UserId = UserId,
            //        PermissionValue = newPermission
            //    });
            //});

            //Service.Insert(insertList);
        }

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }

    public class Base_UserModel : UserInfo
    { 
        public int Id { get; set; }
        public string RoleNames { get => string.Join(",", RoleNameList); }

        public List<string> RoleIdList { get; set; }

        public List<string> RoleNameList { get; set; }

        public EnumType.RoleType RoleType
        {
            get
            {
                int type = 0;

                var values = typeof(EnumType.RoleType).GetEnumValues();
                foreach (var aValue in values)
                {
                    if (RoleNames.Contains(aValue.ToString()))
                        type += (int)aValue;
                }

                return (EnumType.RoleType)type;
            }
        }
    }
}