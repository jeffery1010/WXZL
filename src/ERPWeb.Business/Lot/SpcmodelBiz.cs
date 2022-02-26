using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ERPWeb.Business.GlobalM;
using ERPWeb.Entity.SPC;
using ERPWeb.Util;

namespace ERPWeb.Business.Lot
{
   public class SpcmodelBiz : BaseBusiness<Spcmodel>
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<SpcmodelModel> GetDataList(string fieldname, string keyword, Pagination pagination)
        {
            var where = LinqHelper.True<SpcmodelModel>();

            Expression<Func<Spcmodel, SpcmodelModel>> selectExpre = a => new SpcmodelModel
            {

            };
            selectExpre = selectExpre.BuildExtendSelectExpre();

            var q = from a in GetIQueryable().AsExpandable()
                    select selectExpre.Invoke(a);

            //模糊查询
            if (!keyword.IsNullOrEmpty())
                q = q.Where($@"{fieldname}.contain(@0)", keyword);

            var list = q.Where(where).GetPagination(pagination).ToList();
            SetProperty(list);

            return list;

            void SetProperty(List<SpcmodelModel> unitlist)
            {
                //补充用户角色属性 
                unitlist.ForEach(unit =>
                {
                    unit.modelcode = (unit.code>=10 ? unit.code.ToString():"0"+ unit.code.ToString()); 
                });
            }
        }
        /// <summary>
        /// 获取指定的单条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public Spcmodel GetTheData(string id)
        {
            return GetEntity(id);
        }

        public void AddData(SpcmodelModel newData)
        {
            Insert(newData);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public void UpdateData(SpcmodelModel theData)
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

    }
    public class SpcmodelModel : Spcmodel
    { 
        public string modelcode { get; set; }
    }
}

