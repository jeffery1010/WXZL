using ERPWeb.Entity.GlobalM;
using ERPWeb.Util;
using System;
using System.Collections.Generic;

namespace ERPWeb.Business
{
    interface ILogger
    {
        void WriteSysLog(Base_SysLog log);
        List<Base_SysLog> GetLogList(
            string logContent,
            string logType,
            string opUserNo,
            DateTime? startTime,
            DateTime? endTime,
            Pagination pagination);
    }
}
