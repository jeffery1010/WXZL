using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPWeb.Web.Areas.GlobalM.Controllers.IMGX
{
    using DAL.IMGX;
    using Entity.IMGX;
    public class MacController : Controller
    {
        // GET: GlobalM/Mac
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View("~/Areas/GlobalM/Views/Mac/Search.cshtml");
        }

        public ActionResult SearchForTime(string model,string wsIdList,string lineIdList)
        {
            ViewData["model"] = model;
            ViewData["wsIdList"] = wsIdList;
            ViewData["lineIdList"] = lineIdList;
            return View("~/Areas/GlobalM/Views/Mac/SearchForTime.cshtml");
        }

        [HttpPost]
        public ActionResult GetMac(int page, int rows, Mac record)
        {
            try
            {
                record.id = 0;
                if (string.IsNullOrEmpty(record.ip4)) record.ip4 = "";
                if (string.IsNullOrEmpty(record.name)) record.name = "";
                List<object> allList = MacDAL.getMacObj(record);
                List<object> resultList = allList;
                resultList = resultList.Skip((rows * page - rows)).Take(rows).ToList<object>();
                return Json(new { total = allList.Count, rows = Json(resultList).Data });
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult GetMacForTime(int page, int rows,string model,string wsIdList,string lineIdList)
        {
            try
            {
                List<object> allList = MacDAL.getMacList(model, wsIdList, lineIdList);
                List<object> resultList = allList;
                resultList = resultList.Skip((rows * page - rows)).Take(rows).ToList<object>();
                return Json(new { total = allList.Count, rows = Json(resultList).Data });
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult InsertMac(Mac record)
        {
            try
            {
                RBAC response = MacDAL.putMac(record, "INSERT");
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(new { Code = 500, Message = "新增失败：" + e.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdateMac(Mac record)
        {
            try
            {
                RBAC response = MacDAL.putMac(record, "UPDATE");
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(new { Code = 500, Message = "修改失败：" + e.Message });
            }
        }
    }
}