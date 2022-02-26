using ERPWeb.Entity.BasicInfo;
using ERPWeb.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace ERPWeb.Business.BasicInfo
{
    public class CostComputeStyleBusiness : BaseBusiness<CostComputeStyle>
    {
        #region 外部接口

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<CostComputeStyle> GetDataList(string condition, string keyword, Pagination pagination)
        {
            var q = GetIQueryable();

            //模糊查询
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
                q = q.Where($@"{condition}.Contains(@0)", keyword);

            return q.GetPagination(pagination).ToList();
        }
        public List<CostComputeStyle> GetDataList()
        {
            var q = GetIQueryable(); 

            return q.ToList();
        }
        /// <summary>
        /// 获取指定的单条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public CostComputeStyle GetTheData(string id)
        {
            return GetEntity(id);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="newData">数据</param>
        public void AddData(CostComputeStyle newData)
        {
            Insert(newData);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public void UpdateData(CostComputeStyle theData)
        {
            Update(theData);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public void DeleteData(List<string> ids)
        {
            Delete(ids);
        }

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}