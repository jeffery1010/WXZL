using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.IMGX
{
    public class Search
    {
        public string id { get; set; }
        public string model { get; set; }
        public int cellid { get; set; }
        public int mtid { get; set; }
        public string wsname { get; set; }
        public int macid { get; set; }
        public string macname { get; set; }

        public string opid { get; set; }
        public string camel { get; set; }
        public string part { get; set; }
        public string attr { get; set; }
        public int judge { get; set; }


        public Int64 inputtime { get; set; }
        public DateTime chtime { get; set; }
        public int status { get; set; }
        public string xpath { get; set; }
        public string xpathDownload { get; set; }
        public string xpathShow { get; set; }
        public string key1 { get; set; }

        public string value1 { get; set; }
        public string key2 { get; set; }
        public string value2 { get; set; }
        public string key3 { get; set; }
        public string value3 { get; set; }

    }
}
