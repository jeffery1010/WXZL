using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPWeb.Web.Areas.GlobalM.Controllers.IMGX
{
    using DAL.IMGX;
    using Entity.IMGX;
    public class ReadSettingController : Controller
    {
        // GET: GlobalM/ReadSetting
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetReadSetting(int page, int rows, ReadSetting record)
        {
            try
            {
                record.id = 0;
                List<ReadSetting> allList = ReadSettingDAL.getReadSetting(record);
                List<ReadSetting> resultList = allList;
                resultList = resultList.Skip((rows * page - rows)).Take(rows).ToList<ReadSetting>();
                return Json(new { total = allList.Count, rows = Json(resultList).Data });
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult InsertReadSetting(ReadSetting record)
        {
            try
            {
                RBAC response = ReadSettingDAL.putReadSetting(record, "INSERT");
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(new { Code = 500, Message = "新增失败：" + e.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdateReadSetting(ReadSetting record)
        {
            try
            {
                RBAC response = ReadSettingDAL.putReadSetting(record, "UPDATE");
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(new { Code = 500, Message = "修改失败：" + e.Message });
            }
        }
    }
}