using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPWeb.Util;

namespace ERPWeb.DataRepository
{
    public interface IModelDecorator<T> where T:class, new()
    {
        List<T> PopModelList(DataTable table);
        AjaxResult ParseSubmitResult(DataTable result);
    }
}
