using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPWeb.DataRepository;
using ERPWeb.Util;

namespace ERPWeb.Entity
{
    public abstract class ModelDecorator<T> : IModelDecorator<T> where T : class, new()
    {
        public AjaxResult ParseSubmitResult(DataTable result)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> PopModelList(DataTable ds)
        {
            throw new NotImplementedException();
        }

    }
}
