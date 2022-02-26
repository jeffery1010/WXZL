using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPWeb.Entity.SPC
{
    [Table("t_repairSetting")]
    public class Repair
    {
        /// <summary>
        /// 代理主键
        /// </summary>
        [Key, Column(Order = 1)]
        public int id { get; set; } = 0;
        public int repairtypeid { get; set; } = 0;
        public int inputtypeid { get; set; } = 0;
        public string desr { get; set; } = "";
        public string UserNo { get; set; } = "";
        public DateTime chtime { get; set; } = DateTime.MinValue;
        public DateTime effecttime { get; set; } = DateTime.MinValue;
        public bool isvalid { get; set; } = false;

    }
}
