using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPWeb.Entity.GlobalM
{
    /// <summary>
    /// ���ݿ�����
    /// </summary>
    [Table("DatabaseLink",Schema ="dbo")]
    public class DatabaseLink
    {

        /// <summary>
        /// ��������
        /// </summary>
        [Key]
        public String Id { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public String LinkName { get; set; }

        /// <summary>
        /// �����ַ���
        /// </summary>
        public String ConnectionStr { get; set; }

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public String DbType { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public String SortNum { get; set; }

    }
}