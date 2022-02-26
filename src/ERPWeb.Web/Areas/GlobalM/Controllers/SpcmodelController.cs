using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPWeb.Business.Lot;
using ERPWeb.Util;

namespace ERPWeb.Web.Areas.GlobalM.Controllers
{
    public class SpcmodelController : Controller
    {
        SpcmodelBiz _UnitBiz = new SpcmodelBiz();
        // GET: GlobalM/MUnit
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Change()
        {
            return View();
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public ActionResult GetDataList(string keyword, Pagination pagination)
        {
            var dataList = _UnitBiz.GetDataList("name", keyword, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

    }
}