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
    public class RepairMacTypeBiz : BaseBusiness<RepairMacType>
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<RepairMacTypeModel> GetDataList()
        {
            var where = LinqHelper.True<RepairMacTypeModel>();

            Expression<Func<RepairMacType, RepairMacTypeModel>> selectExpre = a => new RepairMacTypeModel
            { 
            };
            selectExpre = selectExpre.BuildExtendSelectExpre();

            var q = from a in GetIQueryable().AsExpandable()
                    select selectExpre.Invoke(a);
             

            var list = q.Where(where).ToList();
            SetProperty(list);

            return list;

            void SetProperty(List<RepairMacTypeModel> rmlist)
            {
                //补充用户角色属性
                //List<string> UserIds = users.Select(x => x.UserId).ToList();
                var repairlist = (from x in rmlist 
                                  join d in Service.GetIQueryable<MacType>() on x.mtid equals d.mtid 
                                  select new RepairMacTypeModel
                                  {
                                      id = x.id,
                                      MacTypeName = d.mtname, 
                                      rsid = x.rsid,
                                      mtid = x.mtid 
                                  }).ToList();
                rmlist = repairlist;
            }

        }
    }
    public class RepairMacTypeModel : RepairMacType { 
        public string MacTypeName { get; set; }
    }
}
