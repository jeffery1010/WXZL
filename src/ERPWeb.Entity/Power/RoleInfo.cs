using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.Power
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RoleInfo", Schema = "dbo")]
    public class RoleInfo
    {
        /// <summary>
        /// 角色序号
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>
        [MaxLength(64)]
        public string RoleCode { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        [MaxLength(64)]
        public string RoleName { get; set; }
        /// <summary>
        /// 角色备注
        /// </summary>
        [MaxLength(1024)]
        public string RoleRemark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人编号
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

        /// <summary>
        /// 所属组织
        /// </summary>
        public int OrganizationId { get; set; }
    }
}
