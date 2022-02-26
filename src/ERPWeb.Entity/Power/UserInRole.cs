using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.Power
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("UserInRole", Schema = "dbo")]
    public class UserInRole
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }
    }
}
