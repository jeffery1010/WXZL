using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPWeb.Entity.GlobalM
{
    /// <summary>
    /// ��Ԫ���Ա�
    /// </summary>
    [Table("Base_UnitTest")]
    public class Base_UnitTest
    {

        /// <summary>
        /// ��������
        /// </summary>
        [Key]
        public String Id { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public String UserId { get; set; }

        /// <summary>
        /// UserNo
        /// </summary>
        public String UserNo { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        public Int32? Age { get; set; }

    }
}