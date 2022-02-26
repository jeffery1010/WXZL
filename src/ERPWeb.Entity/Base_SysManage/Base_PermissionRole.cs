using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMXWeb.Entity.GlobalM
{
    /// <summary>
    /// 角色权限表
    /// </summary>
    [Table("RelationRP")]
    public class RelationRP
    {

        /// <summary>
        /// 逻辑主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 角色主键Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 权限值
        /// </summary>
        public int PermissionValue { get; set; }

    }
}