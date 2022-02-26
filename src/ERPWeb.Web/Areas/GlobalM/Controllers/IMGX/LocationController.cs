using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPWeb.DAL.IMGX;
using ERPWeb.Entity.IMGX;

namespace ERPWeb.Web.Areas.GlobalM.Controllers.IMGX
{
    public class LocationController : Controller
    {
        // GET: GlobalM/Location
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View("~/Areas/GlobalM/Views/Location/Search.cshtml");
        }
        [HttpPost]
        public ActionResult GetLocation(int page, int rows, Location record)
        {
            try
            {
                record.id = 0;
                if (string.IsNullOrEmpty(record.name)) record.name = "";
                if (string.IsNullOrEmpty(record.features)) record.features = "";
                List<Location> allList = LocationDAL.getRecord(record);
                List<Location> resultList = allList;
                resultList = resultList.Skip((rows * page - rows)).Take(rows).ToList<Location>();
                return Json(new { total = allList.Count, rows = Json(resultList).Data });
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult InsertLocation(Location record)
        {
            try
            {
                RBAC response = LocationDAL.putRecord(record, "INSERT");
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(new { Code = 500, Message = "新增失败：" + e.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdateLocation(Location record)
        {
            try
            {
                RBAC response = LocationDAL.putRecord(record, "UPDATE");
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(new { Code = 500, Message = "修改失败：" + e.Message });
            }
        }
    }
}