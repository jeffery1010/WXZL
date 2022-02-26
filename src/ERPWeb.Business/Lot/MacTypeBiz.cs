using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions; 
using ERPWeb.Entity.SPC;
using ERPWeb.Util;

namespace ERPWeb.Business.Lot
{
    public class MacTypeBiz : BaseBusiness<MacType>
    {

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<MacType> GetDataList(int id)
        {
            var where = LinqHelper.True<MacTypeModel>();
            Expression<Func<MacType, MacTypeModel>> selectExpre = a => new MacTypeModel
            {

            };
            selectExpre = selectExpre.BuildExtendSelectExpre();

            var q = from a in GetIQueryable().AsExpandable()
                    where a.id==id || id==0
                    select a;
             
            var list = q.ToList();
            
            return list; 
        }

    }
    public class MacTypeModel : MacType
    { 

    }
}
