using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPWeb.DAL.IMGX;
using ERPWeb.Entity.IMGX;
using ERPWeb.Web.Model;

namespace ERPWeb.Web.Areas.GlobalM.Controllers.IMGX
{
    public class PartController : Controller
    {
        // GET: GlobalM/Part
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetPart(int page, int rows, Part record)
        {
            try
            {
                if (string.IsNullOrEmpty(record.code)) record.code = "";
                List<Part> allList = PartDAL.getPartByWS(record);
                List<Part> resultList = allList;
                resultList = resultList.Skip((rows * page - rows)).Take(rows).ToList<Part>();
                return Json(new { total = allList.Count, rows = Json(resultList).Data });
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }

        public ActionResult GetPartAll(int page, int rows, Part record)
        {
            try
            {
                
                if ("-1".Equals(record.modelcode)) {
                    record.modelid = -1;
                }
                else
                {
                    Entity.IMGX.Model modelTemp = new Entity.IMGX.Model()
                    {
                        id = 0,
                        isvalid = -1,
                        code = record.modelcode,
                        name = ""
                    };
                    record.modelid = ModelDAL.getModel(modelTemp).FirstOrDefault().id;
                }
                if (string.IsNullOrEmpty(record.code)) record.code = "";
                List<Part> allList = PartDAL.getPart(record);
                List<Part> resultList = allList;
                resultList = resultList.Skip((rows * page - rows)).Take(rows).ToList<Part>();
                return Json(new { total = allList.Count, rows = Json(resultList).Data });
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult GetPartByWS(string wsidList)
        {
            RespEntity result = new RespEntity();
            try
            {
                //必须是激活的
                Part record = new Part() {
                    code = "",
                    isvalid = 1,
                    wsidList = wsidList
                };
                List<Part> allList = PartDAL.getPartByWS(record);
                result.Code = 200;
                result.Message = "SUCCESS";
                result.total = allList.Count;
                result.rows = allList;
                
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
                result.total = 0;
                result.rows = null;
            }
            return Json(result);
        }

        [HttpPost]
        public ActionResult InsertPart(Part record)
        {
            try
            {
                RBAC response = PartDAL.putPart(record, "INSERT");
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(new { Code = 500, Message = "新增失败：" + e.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdatePart(Part record)
        {
            try
            {
                RBAC response = PartDAL.putPart(record, "UPDATE");
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(new { Code = 500, Message = "修改失败：" + e.Message });
            }
        }
    }
}