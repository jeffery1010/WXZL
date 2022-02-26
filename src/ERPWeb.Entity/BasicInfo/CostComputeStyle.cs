using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPWeb.Entity.BasicInfo
{
    /// <summary>
    /// CostComputeStyle
    /// </summary>
    [Table("CostComputeStyle",Schema ="meta")]
    public class CostComputeStyle
    {

        /// <summary>
        /// Id
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// UserNo
        /// </summary>
        public String UserNo { get; set; }

        /// <summary>
        /// IsDefault
        /// </summary>
        public Boolean? IsDefault { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// ChUserNo
        /// </summary>
        public String ChUserNo { get; set; }

        /// <summary>
        /// Chtime
        /// </summary>
        public DateTime? Chtime { get; set; }

        /// <summary>
        /// ChDegree
        /// </summary>
        public Int32? ChDegree { get; set; }

    }
}