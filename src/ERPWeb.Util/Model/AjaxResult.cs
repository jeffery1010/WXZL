using System;
using System.Data;

namespace ERPWeb.Util
{
    /// <summary>
    /// Ajax请求结果
    /// </summary>
    public class AjaxResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误代码：
        /// 1：未登录
        /// 其它待定义
        /// </summary>
        public int ErrorCode { get; set; }
        public int Count { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }

        public AjaxResult(DataTable dt)
        {
            if (dt!=null && dt.Rows.Count > 0)
            {
                if (dt.Columns.Contains("rcount"))
                    Count = Convert.ToInt32(dt.Rows[0]["rcount"]);
                else if (dt.Columns.Contains("tcount"))
                    Count = Convert.ToInt32(dt.Rows[0]["tcount"]);
                else if (dt.Columns.Contains("Xcount"))
                    Count = Convert.ToInt32(dt.Rows[0]["Xcount"]);
                if (dt.Columns.Contains("msg"))
                    Msg = dt.Rows[0]["msg"].ToString();
                else if (dt.Columns.Contains("Message"))
                    Msg = dt.Rows[0]["Message"].ToString();
                if (dt.Columns.Contains("Value"))
                    Data = dt.Rows[0]["Value"].ToString();
                else if (dt.Columns.Contains("ValueX"))
                    Data = dt.Rows[0]["ValueX"].ToString();
                if (dt.Columns.Contains("data"))
                    Data = dt.Rows[0]["data"].ToString();
                if (Count > 0) this.Success = true;
                else
                {
                    Success = false; 
                }
            }
        }
        public AjaxResult(int count, string msg)
        {
            Count = count;
            Msg = msg;
        }
        public AjaxResult()
        {
            Count = 0;
            Msg = "";
        }
        
    }
}
