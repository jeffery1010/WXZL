using ERPWeb.Business.GlobalM;
using ERPWeb.Entity.GlobalM;
using ERPWeb.Util;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ERPWeb.Web
{
    public class LogController : BaseMvcController
    {
        LogBusiness _base_SysLogBusiness = new LogBusiness();

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <param name="logContent">日志内容</param>
        /// <param name="logType">日志类型</param>
        /// <param name="opUserNo">操作人用户名</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public ActionResult GetLogList(
            string logContent,
            string logType,
            string opUserNo,
            DateTime? startTime,
            DateTime? endTime,
            Pagination pagination)
        {
            var dataList = _base_SysLogBusiness.GetLogList(logContent, logType, opUserNo, startTime, endTime, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        public ActionResult GetLogTypeList()
        {
            List<object> logTypeList = new List<object>();
            Enum.GetNames(typeof(EnumType.LogType)).ForEach(aName =>
            {
                logTypeList.Add(new { Name = aName, Value = aName });
            });

            return Content(logTypeList.ToJson());
        }

        #endregion

        #region 提交数据

        #endregion
    }
}