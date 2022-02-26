using IMXWeb.Entity.GlobalM;
using IMXWeb.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace IMXWeb.Business.GlobalM
{
    public class RoleBusiness : BaseBusiness<RoleInfo>
    {
        #region �ⲿ�ӿ�

        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <param name="keyword">�ؼ���</param>
        /// <returns></returns>
        public List<Base_SysRole> GetDataList(string condition, string keyword, Pagination pagination)
        {
            var q = GetIQueryable();

            //ģ����ѯ
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
                q = q.Where($@"{condition}.Contains(@0)", keyword);

            return q.GetPagination(pagination).ToList();
        }

        /// <summary>
        /// ��ȡָ���ĵ�������
        /// </summary>
        /// <param name="id">����</param>
        /// <returns></returns>
        public Base_SysRole GetTheData(string id)
        {
            return GetEntity(id);
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="newData">����</param>
        public void AddData(Base_SysRole newData)
        {
            Insert(newData);
        }

        /// <summary>
        /// ��������
        /// </summary>
        public void UpdateData(Base_SysRole theData)
        {
            Update(theData);
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="theData">ɾ��������</param>
        public void DeleteData(List<string> ids)
        {
            //ɾ����ɫ
            Delete(ids);
        }

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        /// <param name="roleId">��ɫId</param>
        /// <param name="permissions">Ȩ��ֵ</param>
        public void SavePermission(string roleId,List<string> permissions)
        {
            Service.Delete_Sql<RelationRP>(x => x.RoleId.ToString() == roleId);
            List<RelationRP> insertList = new List<RelationRP>();
            permissions.ForEach(newPermission =>
            {
                insertList.Add(new RelationRP
                {
                    Id=0,
                    RoleId=int.Parse(roleId),
                    PermissionValue= int.Parse(newPermission)
                });
            });

            Service.Insert(insertList);
            PermissionManage.ClearUserPermissionCache();
        }

        #endregion

        #region ˽�г�Ա

        #endregion

        #region ����ģ��

        #endregion
    }
}