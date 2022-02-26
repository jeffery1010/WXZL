using ERPWeb.DataRepository;
using ERPWeb.Entity.GlobalM;
using ERPWeb.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPWeb.Business
{
    class RDBMSLogger : ILogger
    {
        private IRepository _db { get; } = DbFactory.GetRepository();
        public List<Base_SysLog> GetLogList(string logContent, string logType, string opUserNo, DateTime? startTime, DateTime? endTime, Pagination pagination)
        {
            var whereExp = LinqHelper.True<Base_SysLog>();
            if (!logContent.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.LogContent.Contains(logContent));
            if (!logType.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.LogType == logType);
            if (!opUserNo.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.OpUserNo.Contains(opUserNo));
            if (!startTime.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.OpTime >= startTime);
            if (!endTime.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.OpTime <= endTime);

            return _db.GetIQueryable<Base_SysLog>().Where(whereExp).GetPagination(pagination).ToList();
        }

        public void WriteSysLog(Base_SysLog log)
        {
            _db.Insert(log);
        }
    }
}
