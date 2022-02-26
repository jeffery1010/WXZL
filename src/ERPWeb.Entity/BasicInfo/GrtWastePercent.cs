using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.MacEntity
{
    public class GrtWastePercent
    {
        public int ID { get; set; }
        /// <summary>
        /// 机种和线别
        /// </summary>
        public string ModelLine { get; set; }
        /// <summary>
        /// 班别
        /// </summary>
        public string Shift { get; set; }
        /// <summary>
        /// 已投放托盘数
        /// </summary>
        public int TrayInputNum { get; set; }
        /// <summary>
        /// 已投放GRT总数
        /// </summary>
        public int GRTTotalNum { get; set; }
        /// <summary>
        /// 已消耗的GRT总数
        /// </summary>
        public int GRTShiftNum { get; set; }
        /// <summary>
        /// 浪费掉的GRT(抛料数)
        /// </summary>
        public int GRTWasteNum { get; set; }
        /// <summary>
        /// 浪费掉的GRT比例(抛料率)
        /// </summary>
        public string GRTWastePercent { get; set; }
        /// <summary>
        /// 查询时间
        /// </summary>
        public string CheckDate { get; set; }
    }
}
