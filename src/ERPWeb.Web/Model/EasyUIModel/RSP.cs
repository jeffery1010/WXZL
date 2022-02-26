using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPWeb.Web.Model.EasyUIModel
{
    public class RBAC
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public RBAC()
        {
            this.Data = "Null";
        }
    }
}