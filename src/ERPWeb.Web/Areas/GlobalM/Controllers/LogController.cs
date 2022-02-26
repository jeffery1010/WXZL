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

        #region ��ͼ����

        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region ��ȡ����

        /// <summary>
        /// ��ȡ��־�б�
        /// </summary>
        /// <param name="logContent">��־����</param>
        /// <param name="logType">��־����</param>
        /// <param name="opUserNo">�������û���</param>
        /// <param name="startTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="pagination">��ҳ����</param>
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

        #region �ύ����

        #endregion
    }
}