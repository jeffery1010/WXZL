using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMXWeb.Entity.GlobalM
{
    /// <summary>
    /// RelationUR
    /// </summary>
    [Table("RelationUR")]
    public class RelationUR
    {

        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// RoleId
        /// </summary>
        public int RoleId { get; set; }

    }
}