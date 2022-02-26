using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPWeb.Entity.BasicInfo
{
    /// <summary>
    /// GoodSort
    /// </summary>
    [Table("GoodSort",Schema ="meta")]
    public class GoodSort
    {

        /// <summary>
        /// ID
        /// </summary>
        [Key, Column(Order = 1)]
        public Int32 Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public String Code { get; set; }

        /// <summary>
        /// ParentId
        /// </summary>
        public Int32? ParentId { get; set; }

    }
}