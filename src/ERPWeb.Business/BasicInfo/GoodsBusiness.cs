using ERPWeb.Business.EntityExtend;
using ERPWeb.Entity.BasicInfo;
using ERPWeb.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace ERPWeb.Business.BasicInfo
{
    public class GoodsBusiness : BaseBusiness<Goods>
    {
        #region �ⲿ�ӿ�

        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <param name="keyword">�ؼ���</param>
        /// <returns></returns>
        public List<GoodsExtend> GetDataList(string condition, string keyword, Pagination pagination)
        {
            var q = GetIQueryable();

            //ģ����ѯ
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
                q = q.Where($@"{condition}.Contains(@0)", keyword);

            return EntityExtend.GoodsExtend.GetGoodsExtendList(q.GetPagination(pagination)).ToList();
        }

        /// <summary>
        /// ��ȡָ���ĵ�������
        /// </summary>
        /// <param name="id">����</param>
        /// <returns></returns>
        public GoodsExtend GetTheData(int id)
        {
            Goods go= GetEntity(id);
            GoodsExtend ge = EntityExtend.GoodsExtend.GetGoodsExtend(go);
            return ge;
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="newData">����</param>
        public void AddData(Goods newData)
        {
            Insert(newData);
        }

        /// <summary>
        /// ��������
        /// </summary>
        public void UpdateData(Goods theData)
        {
            Update(theData);
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="theData">ɾ��������</param>
        public void DeleteData(List<string> ids)
        {
            Delete(ids);
        }

        #endregion

        #region ˽�г�Ա

        #endregion

        #region ����ģ��

        #endregion
    }
}