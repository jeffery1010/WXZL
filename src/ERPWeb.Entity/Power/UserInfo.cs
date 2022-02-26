using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.Power
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserInfo", Schema = "dbo")]
    public class UserInfo
    {
        [Column("Id")]
        /// <summary>
        /// 人员编号
        /// </summary>
        [Key]
        public int UserId { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        [MaxLength(64)]
        public string UserNo { get; set; }
        /// <summary>
        /// 人员名称
        /// </summary>
        [MaxLength(64)]
        public string UserName { get; set; } 
 
        public int Sex { get; set; }
        /// <summary>
        /// 关系编号集合
        /// </summary>
        [MaxLength(2048)]
        public string RelationIdList { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [MaxLength(64)]
        public string Tel { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        [MaxLength(64)]
        public string Email { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(1024)]
        public string Address { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        [MaxLength(64)]
        public string IdCard { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(64)]
        public string MobilePhone { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime BirthDay { get; set; }
        /// <summary>
        /// 是否可见
        /// </summary>
        public int IsVisible { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnable { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(64)]
        public string Password { get; set; }
    }
}
