using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.IMGX
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("m_console")]
    public class Consoles
    {
        [Key]
        public int id { get; set; }
        public string ip4 { get; set; }
        public string name { get; set; }
        public int modelid { get; set; }
        public int isvalid { get; set; }
    }
}
