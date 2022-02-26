using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.MacEntity
{
    public class LineSubmodel
    {
        /// <summary>
        /// 线别
        /// </summary>
        public int Id { get; set; }
        public string TBName { get; set; }
        public int ModelID { get; set; }
        public string ModelName { get; set; }
        public int LatestFlag { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
