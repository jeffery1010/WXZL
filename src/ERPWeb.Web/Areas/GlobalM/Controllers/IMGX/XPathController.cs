using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPWeb.Web.Areas.GlobalM.Controllers.IMGX
{
    using DAL.IMGX;
    using Entity.IMGX;
    public class XPathController : Controller
    {
        // GET: GlobalM/XPath
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View("~/Areas/GlobalM/Views/XPath/Search.cshtml");
        }
        [HttpPost]
        public ActionResult GetXPath(int page, int rows, XPath record)
        {
            try
            {
                record.id = 0;
                if (string.IsNullOrEmpty(record.xpath)) record.xpath = "";
                if (string.IsNullOrEmpty(record.topath)) record.topath = "";
                if (string.IsNullOrEmpty(record.extend)) record.extend = "";
                if (string.IsNullOrEmpty(record.userno)) record.userno = "";
                List<XPath> allList =  XPathDAL.getXPath(record);
                List<XPath> resultList = allList;
                resultList = resultList.Skip((rows * page - rows)).Take(rows).ToList<XPath>();
                return Json(new { total = allList.Count, rows = Json(resultList).Data });
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult InsertXPath(XPath record)
        {
            try
            {
                RBAC response = XPathDAL.putXPath(record, "INSERT");
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(new { Code = 500, Message = "新增失败：" + e.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdateXPath(XPath record)
        {
            try
            {
                RBAC response = XPathDAL.putXPath(record, "UPDATE");
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(new { Code = 500, Message = "修改失败：" + e.Message });
            }
        }
    }
}