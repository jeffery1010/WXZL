using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPWeb.Entity.SPC
{
    [Table("v_Spcmodel")]
    public class Spcmodel
    {
        /// <summary>
        /// 代理主键
        /// </summary>
        [Key, Column(Order = 1)]
        public int id { get; set; }
        public string name { get; set; }
        public int modelid { get; set; }
        public int model { get; set; }
        public int code { get; set; }
        public string bkc { get; set; } 
    }
}
