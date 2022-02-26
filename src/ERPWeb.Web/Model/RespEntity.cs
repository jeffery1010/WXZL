using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPWeb.Web.Model
{
    public class RespEntity
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public int total { get; set; }
        public object rows { get; set; }
    }
}