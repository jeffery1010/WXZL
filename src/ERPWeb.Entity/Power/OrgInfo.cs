using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.Power
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OrgInfo", Schema = "dbo")]
    public class OrgInfo
    {
        /// <summary>
        /// 组织编号
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 组织名称
        /// </summary>
        [MaxLength(64)]
        public string Name { get; set; }
        /// <summary>
        /// 组织编码
        /// </summary>
        [MaxLength(64)]
        public string Code { get; set; }
        /// <summary>
        /// 组织备注
        /// </summary>
        [MaxLength(1024)]
        public string Remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 父节点编号
        /// </summary>
        public string ParentCode { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        [MaxLength(64)]
        public string VersionCode { get; set; }
        /// <summary>
        /// 应用程序编号
        /// </summary>
        [MaxLength(64)]
        public string AppCode { get; set; }
        /// <summary>
        /// 是否可见
        /// </summary>
        public int IsVisible { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnable { get; set; }

        /// <summary>
        /// 子集合
        /// ////// attention——Remember that child nodes must use "children"
        /// </summary>
        public List<OrgInfo> children = new List<OrgInfo>();
    }
}
