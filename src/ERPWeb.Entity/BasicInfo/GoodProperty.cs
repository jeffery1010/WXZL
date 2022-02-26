using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPWeb.Entity.BasicInfo
{
    /// <summary>
    /// GoodProperty
    /// </summary>
    [Table("GoodProperty",Schema ="meta")]
    public class GoodProperty
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key, Column(Order = 1)]
        public Int32 Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// IsDefault
        /// </summary>
        public Boolean IsDefault { get; set; }

    }
}