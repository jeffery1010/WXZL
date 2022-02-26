using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPWeb.Entity.GlobalM
{
    /// <summary>
    /// 系统，用户表
    /// </summary>
    //[Table("UserInfo")]
    public class Base_UserX
    {

        /// <summary>
        /// 代理主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        [Column("UserNo")]
        /// <summary>
        /// 用户Id,逻辑主键
        /// </summary>
        public String UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        //[Column("UserNo")]
        //public String UserNo { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public String Password { get; set; }

        [Column("UserName")]
        /// <summary>
        /// 真实姓名
        /// </summary>
        public String RealName { get; set; }

        /// <summary>
        /// 性别(1为男，0为女)
        /// </summary>
        public Int32? Sex { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }
    }
}