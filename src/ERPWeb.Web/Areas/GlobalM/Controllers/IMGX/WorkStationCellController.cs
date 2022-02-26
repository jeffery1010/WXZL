using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPWeb.Web.Areas.GlobalM.Controllers.IMGX
{
    using DAL.IMGX;
    using Entity.IMGX;
    public class WorkStationCellController : Controller
    {
        // GET: GlobalM/WorkStationCell
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetWorkStationCell(int page, int rows, WorkStationCell record)
        {
            try
            {
                record.id = 0;
                List<WorkStationCell> allList = WorkStationCellDAL.getWorkStationCell(record);
                List<WorkStationCell> resultList = allList;
                resultList = resultList.Skip((rows * page - rows)).Take(rows).ToList<WorkStationCell>();
                return Json(new { total = allList.Count, rows = Json(resultList).Data });
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult InsertWorkStationCell(WorkStationCell record)
        {
            try
            {
                RBAC response = WorkStationCellDAL.putWorkStationCell(record, "INSERT");
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(new { Code = 500, Message = "新增失败：" + e.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdateWorkStationCell(WorkStationCell record)
        {
            try
            {
                RBAC response = WorkStationCellDAL.putWorkStationCell(record, "UPDATE");
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(new { Code = 500, Message = "修改失败：" + e.Message });
            }
        }
    }
}