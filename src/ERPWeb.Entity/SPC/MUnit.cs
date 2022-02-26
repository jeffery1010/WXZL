using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPWeb.Entity.SPC
{
    [Table("m_unit")]
    public class MUnit //: ModelDecorator<MUnit>
    {
        public MUnit() {
            //ProcName= "wp_test_new";
        }
        /// <summary>
        /// 代理主键
        /// </summary>
        [Key, Column(Order = 1)]
        public int id { get; set; }
        //public Test() { }

        /// <summary>
        /// 名称
        /// </summary>
        public String name { get; set; }
        public bool isvalid { get; set; }
        public DateTime chtime { get; set; }


    } 
}
