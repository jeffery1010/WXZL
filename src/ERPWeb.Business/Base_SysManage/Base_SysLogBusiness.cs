using ERPWeb.Entity.GlobalM;
using ERPWeb.Util;
using System;
using System.Collections.Generic;

namespace ERPWeb.Business.GlobalM
{
    public class LogBusiness : BaseBusiness<Base_SysLog>
    {
        #region �ⲿ�ӿ�

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

        #region ˽�г�Ա

        #endregion

        #region ����ģ��

        #endregion
    }
}