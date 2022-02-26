using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPWeb.Entity.SPC
{
    [Table("m_inputType")]
    public class InputType
    {
        /// <summary>
        /// 代理主键
        /// </summary>
        [Key, Column(Order = 1)]
        public int id { get; set; } 
        public string name { get; set; } 
    }
}
