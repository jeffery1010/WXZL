using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.Power
{
    [Table("PowerInRole", Schema = "dbo")]
    public class PowerInRole
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Key]
        public int Id { get; set; }

        public int RoleId { get; set; }

        public int PowerId { get; set; }
    }
}
