using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.Power
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PowerInfo", Schema = "dbo")]
    public class PowerInfo
    {
        /// <summary>
        /// 权限序号
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 权限编号
        /// </summary>
        [MaxLength(64)]
        public string PowerCode { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        [MaxLength(64)]
        public string PowerName { get; set; }
        /// <summary>
        /// 权限备注
        /// </summary>
        [MaxLength(64)]
        public string PowerRemark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateUserId { get; set; }
        /// <summary>
        /// 是否可见
        /// </summary>
        public int IsVisible { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnable { get; set; }
    }
}
