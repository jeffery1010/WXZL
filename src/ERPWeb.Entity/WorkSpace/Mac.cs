using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.IMGX
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("m_mac")]
    public class Mac
    {
        [Key]
        public int id { get; set; }
        public string ip4 { get; set; }
        public string name { get; set; }
        public int wsid { get; set; }
        public DateTime chtime { get; set; }
        public int isvalid { get; set; }
    }
}
