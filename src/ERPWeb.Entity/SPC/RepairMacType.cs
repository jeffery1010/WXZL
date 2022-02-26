using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.SPC
{ 
    [Table("t_repairMacType")]
    public class RepairMacType
    {
        /// <summary>
        /// 代理主键
        /// </summary>
        [Key, Column(Order = 1)]
        public int id { get; set; }
        public int rsid { get; set; }
        public int mtid { get; set; } 
    }
}
