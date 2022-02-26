using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Entity.MacEntity
{
    public class MachineSubmodel
    {
        public int Id { get; set; }
        public string PCName { get; set; }
        public string SubIds { get; set; }
        public string SubCodes { get; set; }
        public string SubInts { get; set; }
    }
}
