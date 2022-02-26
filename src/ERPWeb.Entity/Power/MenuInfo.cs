using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.Power
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("ResInfo", Schema = "dbo")]
    public class MenuInfo
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 菜单编号
        /// </summary>
        [MaxLength(64)]
        public string Code { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [MaxLength(64)]
        public string Name { get; set; }
        /// <summary>
        /// 菜单备注
        /// </summary>
        [MaxLength(1024)]
        public string Remark { get; set; }
        /// <summary>
        /// 父节点编号
        /// </summary>
        [MaxLength(64)]
        public string ParentCode { get; set; }
        /// <summary>
        /// 菜单地址
        /// </summary>
        [MaxLength(64)]
        public string XPath { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否可见
        /// </summary>
        public int IsVisible { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnable { get; set; }
        public string Icon { get; set; }
        public string Permission { get; set; }

        public int Category { get; set; }
        /// <summary>
        /// 子集
        /// </summary>
        public List<MenuInfo> children = new List<MenuInfo>();
    }
}
