using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ERPWeb.Entity.SPC;
using ERPWeb.Util;

namespace ERPWeb.Business.Lot
{
    public class MUnitBiz : BaseBusiness<MUnit>
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<MUnitModel> GetDataList(string fieldname,string keyword, Pagination pagination)
        {
            var where = LinqHelper.True<MUnitModel>();

            Expression<Func<MUnit, MUnitModel>> selectExpre = a => new MUnitModel
            {

            };
            selectExpre = selectExpre.BuildExtendSelectExpre();

            var q = from a in GetIQueryable().AsExpandable()
                    select selectExpre.Invoke(a);

            //模糊查询
            if (!keyword.IsNullOrEmpty())
                q = q.Where($@"{fieldname}.Contains(@0)", keyword);

            var list = q.Where(where).GetPagination(pagination).ToList();
            SetProperty(list);

            return list;

            void SetProperty(List<MUnitModel> unitlist)
            {
                //补充用户角色属性 
                unitlist.ForEach(unit =>
                {
                    unit.status = unit.isvalid?"有效":"无效";
                    unit.chtime2 = unit.chtime.ToString("yyyy-MM-dd");
                });
            }
        }

    }
    public class MUnitModel : MUnit {
        public string status { get; set; }
        public string chtime2 { get; set; }
    }
}

