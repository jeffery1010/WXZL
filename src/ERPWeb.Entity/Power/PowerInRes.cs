using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.Power
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PowerInRes", Schema = "dbo")]
    public class PowerInRes
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Key]
        public int Id { get; set; }
        public int PowerId { get; set; }
        public int ResId { get; set; }
        /// <summary>
        /// 关联编号
        /// </summary>
        [MaxLength(64)]
        public string Code { get; set; }
        /// <summary>
        /// 关系名称
        /// </summary>
        [MaxLength(64)]
        public string RelationName { get; set; }

        /// <summary>
        /// 菜单序号集合
        /// </summary>
        [MaxLength(2048)]
        public string MenuIdList { get; set; }
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
