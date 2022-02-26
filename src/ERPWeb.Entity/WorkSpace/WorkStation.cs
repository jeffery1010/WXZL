using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.IMGX
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("m_workstation")]
    public class WorkStation
    {
        [Key]
        public int id { get; set; }
        public int modelid { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public int fsequence { get; set; }
        public int status { get; set; }
        public DateTime chtime { get; set; }
    }
}
