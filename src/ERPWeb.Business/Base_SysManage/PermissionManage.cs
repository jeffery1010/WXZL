using ERPWeb.Business.Common;
using ERPWeb.Entity.GlobalM;
using ERPWeb.Entity.Power;
using ERPWeb.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ERPWeb.Business.GlobalM
{
    /// <summary>
    /// 权限管理静态类
    /// </summary>
    public static class PermissionManage
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        static PermissionManage()
        {
            InitAllPermissionModules();
            InitAllPermissionValues();
        }

        #endregion

        #region 内部成员

        private static string _permissionConfigFile { get; } = "~/Config/Permission.config";
        private static List<PermissionModule> _allPermissionModules { get; set; }
        private static List<string> _allPermissionValues { get; set; }
        private static void InitAllPermissionModules()
        {
            List<PermissionModule> resList = new List<PermissionModule>();
            string filePath = HttpContext.Current.Server.MapPath(_permissionConfigFile);
            XElement xe = XElement.Load(filePath);
            xe.Elements("module")?.ForEach(aModule =>
            {
                PermissionModule newModule = new PermissionModule();
                resList.Add(newModule);

                newModule.Name = aModule.Attribute("name")?.Value;
                newModule.Value = aModule.Attribute("value")?.Value;
                newModule.Items = new List<PermissionItem>();
                aModule?.Elements("permission")?.ForEach(aItem =>
                {
                    PermissionItem newItem = new PermissionItem();
                    newModule.Items.Add(newItem);

                    newItem.Name = aItem?.Attribute("name")?.Value;
                    newItem.Value = aItem?.Attribute("value")?.Value;
                });
            });

            _allPermissionModules = resList;
        }
        private static void InitAllPermissionValues()
        {
            List<string> resList = new List<string>();

            GetAllPermissionModules()?.ForEach(aModule =>
            {
                aModule.Items?.ForEach(aItem =>
                {
                    resList.Add($"{aModule.Value}.{aItem.Value}");
                });
            });

            _allPermissionValues = resList;
        }
        private static List<PermissionModule> GetPermissionModules(List<string> hasPermissions)
        {
            var permissionModules = GetAllPermissionModules();
            permissionModules.ForEach(aModule =>
            {
                aModule.Items?.ForEach(aItem =>
                {
                    aItem.IsChecked = hasPermissions.Contains($"{aModule.Value}.{aItem.Value}");
                });
            });

            return permissionModules;
        }
        private static string _cacheKey { get; } = "Permission";
        private static string BuildCacheKey(string key)
        {
            return $"{GlobalSwitch.ProjectName}_{_cacheKey}_{key}";
        }
        
        #endregion

        #region 所有权限

        /// <summary>
        /// 获取所有权限模块
        /// </summary>
        /// <returns></returns>
        public static List<PermissionModule> GetAllPermissionModules()
        {
            return _allPermissionModules.DeepClone();
        }

        /// <summary>
        /// 获取所有权限值
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllPermissionValues()
        {
            return _allPermissionValues.DeepClone();
        }

        #endregion

        #region 角色权限

        /// <summary>
        /// 获取角色权限模块
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static List<PermissionModule> GetRolePermissionModules(string roleId)
        {
            BaseBusiness<PowerInRole> _db = new BaseBusiness<PowerInRole>();
            var hasPermissions = _db.GetIQueryable().Where(x => x.RoleId.ToString() == roleId).Select(x => x.PowerId.ToString()).ToList();

            return GetPermissionModules(hasPermissions);
        }

        #endregion

        #region AppId权限

        /// <summary>
        /// 获取AppId权限模块
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public static List<PermissionModule> GetAppIdPermissionModules(string appId)
        {
            var hasPermissions = GetAppIdPermissionValues(appId);

            return GetPermissionModules(hasPermissions);
        }

        /// <summary>
        /// 获取AppId权限值
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public static List<string> GetAppIdPermissionValues(string appId)
        {
            string cacheKey = BuildCacheKey(appId);
            var permissions = CacheHelper.Cache.GetCache<List<string>>(cacheKey);
            if (permissions == null)
            {
                BaseBusiness<Base_PermissionAppId> _db = new BaseBusiness<Base_PermissionAppId>();
                permissions = _db.GetIQueryable().Where(x => x.AppId == appId).Select(x => x.PermissionValue).ToList();

                CacheHelper.Cache.SetCache(cacheKey, permissions);
            }

            return permissions.DeepClone();
        }

        /// <summary>
        /// 设置AppId权限
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="permissions">权限值列表</param>
        public static void SetAppIdPermission(string appId,List<string> permissions)
        {
            //更新缓存
            string cacheKey = BuildCacheKey(appId);
            CacheHelper.Cache.SetCache(cacheKey, permissions);

            //更新数据库
            BaseBusiness<Base_UnitTest> _db = new BaseBusiness<Base_UnitTest>();
            var Service = _db.Service;

            Service.Delete_Sql<Base_PermissionAppId>(x => x.AppId == appId);

            List<Base_PermissionAppId> insertList = new List<Base_PermissionAppId>();
            permissions.ForEach(newPermission =>
            {
                insertList.Add(new Base_PermissionAppId
                {
                    Id = Guid.NewGuid().ToSequentialGuid(),
                    AppId = appId,
                    PermissionValue = newPermission
                });
            });

            Service.Insert(insertList);
        }

        #endregion

        #region 用户权限

        /// <summary>
        /// 获取用户权限模块
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static List<PermissionModule> GetUserPermissionModules(string UserId)
        {
            var hasPermissions = GetUserPermissionValues(UserId);

            return GetPermissionModules(hasPermissions);
        }

        /// <summary>
        /// 获取用户拥有的所有权限值
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public static List<string> GetUserPermissionValues(string UserId)
        {
            string cacheKey = BuildCacheKey(UserId);
            var permissions = CacheHelper.Cache.GetCache<List<string>>(cacheKey)?.DeepClone();

            if (permissions == null)
            {
                UpdateUserPermissionCache(UserId);
                permissions = CacheHelper.Cache.GetCache<List<string>>(cacheKey)?.DeepClone();
            }

            return permissions;
        }

        /// <summary>
        /// 设置用户权限
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <param name="permissions">权限值列表</param>
        public static void SetUserPermission(int UserId,List<string> permissions)
        {
            //更新数据库
            //BaseBusiness<UserInfo> _db = new BaseBusiness<UserInfo>();
            //var Service = _db.Service;

            //Service.Delete_Sql<PowerInRole>(x => x. == UserId);
            //var roleIdList = _db.Service.GetIQueryable<UserInRole>().Where(x => x.UserId == UserId).Select(x => x.RoleId).ToList();
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

            ////更新缓存
            //UpdateUserPermissionCache(UserId);
        }

        /// <summary>
        /// 清除所有用户权限缓存
        /// </summary>
        public static void ClearUserPermissionCache()
        {
            BaseBusiness<Base_UnitTest> _db = new BaseBusiness<Base_UnitTest>();
            var UserIdList = _db.Service.GetIQueryable<UserInfo>().Select(x => x.UserNo).ToList();
            UserIdList.ForEach(aUserId =>
            {
                CacheHelper.Cache.RemoveCache(BuildCacheKey(aUserId));
            });
        }

        /// <summary>
        /// 更新用户权限缓存
        /// </summary>
        /// <param name="UserId"><用户Id/param>
        public static void UpdateUserPermissionCache(string UserId)
        {
            string cacheKey = BuildCacheKey(UserId);
            List<string> permissions = new List<string>();

            BaseBusiness<Base_PermissionUser> _db = new BaseBusiness<Base_PermissionUser>();
            var userPermissions = _db.GetIQueryable().Where(x => x.UserId == UserId).Select(x => x.PermissionValue).ToList();
            var theUser = _db.Service.GetIQueryable<UserInfo>().Where(x => x.UserNo == UserId).FirstOrDefault();
            var roleIdList = UserBusiness.GetUserRoleIds(UserId);
            var rolePermissions = _db.Service.GetIQueryable<PowerInRole>().Where(x => roleIdList.Contains(x.RoleId.ToString())).GroupBy(x => x.PowerId.ToString()).Select(x => x.Key).ToList();
            var existsPermissions = userPermissions.Concat(rolePermissions).Distinct();

            permissions = existsPermissions.ToList();
            CacheHelper.Cache.SetCache(cacheKey, permissions);
        }

        #endregion

        #region 当前操作用户权限

        /// <summary>
        /// 获取当前操作者拥有的所有权限值
        /// </summary>
        /// <returns></returns>
        public static List<string> GetOperatorPermissionValues()
        {
            if (Operator.IsAdmin())
                return GetAllPermissionValues();
            else
                return GetUserPermissionValues(Operator.UserId);
        }

        /// <summary>
        /// 判断当前操作者是否拥有某项权限值
        /// </summary>
        /// <param name="value">权限值</param>
        /// <returns></returns>
        public static bool OperatorHasPermissionValue(string value)
        {
            return true;
            //return GetOperatorPermissionValues().Exists(x => x.ToLower() == value.ToLower());
        }

        #endregion
    }

    #region 数据模型

    public class PermissionModule
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public List<PermissionItem> Items { get; set; }
    }

    public class PermissionItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsChecked { get; set; }
    }

    #endregion
}