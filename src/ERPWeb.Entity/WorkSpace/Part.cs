using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.IMGX
{
    public class Part
    {
        public int id { get; set; }

        public string code { get; set; }
        public string detail { get; set; }
        public int isvalid { get; set; }


        public int modelid { get; set; }
        public string modelcode { get; set; }

        public int wsid { get; set; }
        public string wscode { get; set; }
        
        public DateTime datetime { get; set; }

        public string wsidList { get; set; }
    }
}
