using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPWeb.Entity.GlobalM
{
    /// <summary>
    /// 单元测试表
    /// </summary>
    [Table("Base_UnitTest")]
    public class Base_UnitTest
    {

        /// <summary>
        /// 代理主键
        /// </summary>
        [Key]
        public String Id { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public String UserId { get; set; }

        /// <summary>
        /// UserNo
        /// </summary>
        public String UserNo { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        public Int32? Age { get; set; }

    }
}