using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPWeb.Entity.SPC
{
    [Table("m_machine")]
    public class Machine
    {
        public Machine()
        {
        }
        /// <summary>
        /// 代理主键
        /// </summary>
        [Key, Column(Order = 1)]
        public int id { get; set; }
        public DateTime chtime { get; set; }
        public string code { get; set; }
        public int flag { get; set; }
        public int flag1 { get; set; }
        
        public int modeid { get; set; }
        /// <summary>
        /// 机种
        /// </summary>
        public int model { get; set; }
        public string name { get; set; }
        public int statusid { get; set; }
        public string userno { get; set; }

    }
}
