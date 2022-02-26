using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.IMGX
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("m_readsetting")]
    public class ReadSetting
    {
        [Key]
        public int id { get; set; }
        public int consoleid { get; set; }
        public int xpathid { get; set; }
        public DateTime chtime { get; set; }

        public string consolename { get; set; }
        public string consoleip4 { get; set; }
        public string xpathxpath { get; set; }
        public string xpathtopath { get; set; }
        public string xpathextend { get; set; }
        public int xpathrate { get; set; }
    }
}
