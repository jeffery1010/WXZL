using ERPWeb.Business.GlobalM;
using ERPWeb.Entity.GlobalM;
using ERPWeb.Util;
using System;
using System.Threading.Tasks;

namespace ERPWeb.Business.Common
{
    static public class BusHelper
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="logContent">日志内容</param>
        /// <param name="logType">日志类型</param>
        public static void WriteSysLog(string logContent, EnumType.LogType logType)
        {
            string UserNo = null;
            try
            {
                UserNo = UserBusiness.GetCurrentUser().UserNo;
            }
            catch(Exception er)
            {

            }
            Base_SysLog newLog = new Base_SysLog
            {
                Id = Guid.NewGuid().ToSequentialGuid(),
                LogType = logType.ToString(),
                LogContent = logContent.Replace("\r\n", "<br />").Replace("  ", "&nbsp;&nbsp;"),
                OpTime = DateTime.Now,
                OpUserNo = UserNo
            };
            Task.Run(() =>
            {
                try
                {
                    LoggerFactory.GetLogger().WriteSysLog(newLog);
                }
                catch
                {

                }
            });
        }

        /// <summary>
        /// 处理系统异常
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static void HandleException(Exception ex)
        {
            string msg = ExceptionHelper.GetExceptionAllMsg(ex);
            WriteSysLog(msg, EnumType.LogType.系统异常);
        }
    }
}