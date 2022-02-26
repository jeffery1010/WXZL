using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.RepairEntity
{
    public class RepairType
    {
        public int Id { get; set; }
        public int modeltype { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int maxnum { get; set; }
        public string portlist { get; set; }
        public string portnamelist { get; set; }
        public string inputtype { get; set; }
    }
}
