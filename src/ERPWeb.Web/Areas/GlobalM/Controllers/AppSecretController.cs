using ERPWeb.Business.GlobalM;
using ERPWeb.Entity.GlobalM;
using ERPWeb.Util;
using System;
using System.Web.Mvc;

namespace ERPWeb.Web
{
    public class AppSecretController : BaseMvcController
    {
        AppSecretBusiness _base_AppSecretBusiness = new AppSecretBusiness();

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new Base_AppSecret() : _base_AppSecretBusiness.GetTheData(id);

            return View(theData);
        }

        public ActionResult PermissionForm(string appId)
        {
            ViewData["appId"] = appId;

            return View();
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public ActionResult GetDataList(string condition, string keyword, Pagination pagination)
        {
            var dataList = _base_AppSecretBusiness.GetDataList(condition, keyword, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(Base_AppSecret theData)
        {
            if(theData.Id.IsNullOrEmpty())
            {
                theData.Id = Guid.NewGuid().ToSequentialGuid();

                _base_AppSecretBusiness.AddData(theData);

                WriteSysLog($"添加应用Id:{theData.AppId}", EnumType.LogType.接口密钥管理);
            }
            else
            {
                _base_AppSecretBusiness.UpdateData(theData);
                WriteSysLog($"更改应用Id:{theData.AppId}", EnumType.LogType.接口密钥管理);
            }

            return Success();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {
            var idList = ids.ToList<string>();
            _base_AppSecretBusiness.DeleteData(idList);
            WriteSysLog($"删除自然主键为:{string.Join(",", idList)}的应用Id数据", EnumType.LogType.接口密钥管理);

            return Success("删除成功！");
        }

        public ActionResult SavePermission(string appId, string permissions)
        {
            PermissionManage.SetAppIdPermission(appId, permissions.ToList<string>());

            return Success();
        }

        #endregion
    }
}