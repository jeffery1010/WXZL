using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPWeb.DataRepository;
using ERPWeb.Util;

namespace ERPWeb.Business
{
    public class CustomBusiness<T> : BaseBusiness<T> where T : class, IModelDecorator<T> , new()
    {
        public AjaxResult SubmitChange(IModelDecorator<T> inst)
        { 
            DataTable table = ExecuteProcedureForDataTable("", null);
            return new AjaxResult(table); 
        }
    }
}
