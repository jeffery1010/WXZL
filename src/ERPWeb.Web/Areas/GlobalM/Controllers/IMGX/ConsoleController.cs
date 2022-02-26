using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPWeb.Web.Areas.GlobalM.Controllers.IMGX
{
    using Entity.IMGX;
    using DAL.IMGX;
    public class ConsoleController : Controller
    {
        // GET: GlobalM/Console
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View("~/Areas/GlobalM/Views/Console/Search.cshtml");
        }

        [HttpPost]
        public ActionResult GetConsole(int page, int rows, Consoles record) {
            try
            {
                record.id = 0;
                if (string.IsNullOrEmpty(record.ip4)) record.ip4 = "";
                if (string.IsNullOrEmpty(record.name)) record.name = "";
                List<Consoles> allList = ConsoleDAL.getConsole(record);
                List<Consoles> resultList = allList;
                resultList = resultList.Skip((rows * page - rows)).Take(rows).ToList<Consoles>();
                return Json(new { total = allList.Count, rows = Json(resultList).Data });
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult InsertConsole(Consoles record) {
            try
            {
                RBAC response = ConsoleDAL.putConsole(record, "INSERT");
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(new { Code = 500, Message = "新增失败：" + e.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdateConsole(Consoles record)
        {
            try
            {
                RBAC response = ConsoleDAL.putConsole(record, "UPDATE");
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(new { Code = 500, Message = "修改失败：" + e.Message });
            }
        }
    }
}