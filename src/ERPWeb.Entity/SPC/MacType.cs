using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPWeb.Entity.SPC
{
    [Table("m_mactype")]
    public class MacType 
    {
        public MacType()
        { 
        }
        /// <summary>
        /// 代理主键
        /// </summary>
        [Key, Column(Order = 1)]
        public int id { get; set; }
        public int modelid { get; set; }
        public int mtid { get; set; }


        /// <summary>
        /// 名称
        /// </summary>
        public String mtname { get; set; }
        public String mtno { get; set; }
        public int? sequence { get; set; }

    }
}
