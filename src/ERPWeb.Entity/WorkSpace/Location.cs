using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ERPWeb.Entity.IMGX
{ 
    [Table("m_location")]
    public class Location
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }

        /// <summary>
        /// 特性
        /// </summary>
        public string features { get; set; }
    }
}
