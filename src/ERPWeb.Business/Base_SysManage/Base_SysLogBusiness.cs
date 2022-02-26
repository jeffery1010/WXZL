using ERPWeb.Entity.GlobalM;
using ERPWeb.Util;
using System;
using System.Collections.Generic;

namespace ERPWeb.Business.GlobalM
{
    public class LogBusiness : BaseBusiness<Base_SysLog>
    {
        #region 外部接口

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
        public List<Base_SysLog> GetLogList(
            string logContent,
            string logType,
            string opUserNo,
            DateTime? startTime,
            DateTime? endTime,
            Pagination pagination)
        {
            return LoggerFactory.GetLogger().GetLogList(logContent, logType, opUserNo, startTime, endTime, pagination);
        }

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}