using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPWeb.DataRepository;
using ERPWeb.Util;

namespace ERPWeb.Entity.Base_SysManage
{
    [Table("t_Test")]
    public class Test : IModelDecorator<Test>
    {
        /// <summary>
        /// 代理主键
        /// </summary>
        [Key, Column(Order = 1)]
        public int id { get; set; }
        //public Test() { }

        /// <summary>
        /// 用户名
        /// </summary>
        public String name { get; set; }
        public DateTime? chtime { get; set; }
        string _ProcName = "wp_test_new";
        Dictionary<string, object> _ProcParameters;
        

        public string ProcName { get { return _ProcName; } set { _ProcName = value; } }
        public Dictionary<string, object> ProcParmList { get { return _ProcParameters; } set { _ProcParameters = value; } }

        public Test PopModel(DataTable ds)
        {
            throw new NotImplementedException();
        }

        public List<Test> PopModelList(DataTable ds)
        {
            throw new NotImplementedException();
        }

        public AjaxResult ParseSubmitResult(DataTable result)
        {
            throw new NotImplementedException();
        }
    }
}
