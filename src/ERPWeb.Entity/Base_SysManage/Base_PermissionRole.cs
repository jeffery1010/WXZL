using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMXWeb.Entity.GlobalM
{
    /// <summary>
    /// ��ɫȨ�ޱ�
    /// </summary>
    [Table("RelationRP")]
    public class RelationRP
    {

        /// <summary>
        /// �߼�����
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// ��ɫ����Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Ȩ��ֵ
        /// </summary>
        public int PermissionValue { get; set; }

    }
}