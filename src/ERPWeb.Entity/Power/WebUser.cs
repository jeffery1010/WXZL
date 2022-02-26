using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.Power
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("WebUser", Schema = "dbo")]
    public class WebUser
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string LoginHost { get; set; }
        public string LoginIp { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ChangeTime { get; set; }
    }
}
