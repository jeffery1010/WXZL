using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.IMGX
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("m_xpath")]
    public class XPath
    {
        [Key]
        public int id { get; set; }
        public int macid { get; set; }
        public string xpath { get; set; }
        public string topath { get; set; }
        public string extend { get; set; }
        public int rate { get; set; }
        public DateTime chtime { get; set; }
        public string userno { get; set; }
        public int isvalid { get; set; }


        public int ratemin { get; set; }
        public int ratemax { get; set; }

        public string macname { get; set; }
        public string macip4 { get; set; }
    }
}
