using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPWeb.DataRepository;
using ERPWeb.Entity.Base_SysManage;
using ERPWeb.Util;

namespace ERPWeb.Business.Base_SysManage
{
    public class TestBiz : CustomBusiness<Test>
    {
        public AjaxResult GetTestData(string name)
        {
            //if (name.IsNullOrEmpty())
            //    return Error("名称不能为空！"); 
            //var result = GetIQueryable().Where(x => x.name == name).FirstOrDefault();
            //if (result != null)
            //{ 
            //    return Success();
            //}
            //else
                return Error("名称不正确！");
        }
        public void NewTestData()
        {
            Test test = new Test() { name = "dog" };
            test.ProcParmList = new Dictionary<string, object>();
            test.ProcParmList.Add("@name", "dog");
            AjaxResult ar = SubmitChange(test);
            
        }
         
         
    }
}
