using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.IMGX
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("m_workstation_cell")]
    public class WorkStationCell
    {
        [Key]
        public int id { get; set; }
        public int wsid { get; set; }
        public int cellid { get; set; }
        public DateTime chtime { get; set; }
        public int isvalid { get; set; }

        public string wscode { get; set; }
        public string wsname { get; set; }
        public string cellcode { get; set; }
        public string cellname { get; set; }
    }
}
