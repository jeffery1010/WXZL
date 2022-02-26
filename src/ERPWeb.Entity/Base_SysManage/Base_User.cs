using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPWeb.Entity.GlobalM
{
    /// <summary>
    /// ϵͳ���û���
    /// </summary>
    //[Table("UserInfo")]
    public class Base_UserX
    {

        /// <summary>
        /// ��������
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        [Column("UserNo")]
        /// <summary>
        /// �û�Id,�߼�����
        /// </summary>
        public String UserId { get; set; }

        /// <summary>
        /// �û���
        /// </summary>
        //[Column("UserNo")]
        //public String UserNo { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public String Password { get; set; }

        [Column("UserName")]
        /// <summary>
        /// ��ʵ����
        /// </summary>
        public String RealName { get; set; }

        /// <summary>
        /// �Ա�(1Ϊ�У�0ΪŮ)
        /// </summary>
        public Int32? Sex { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime? Birthday { get; set; }
    }
}