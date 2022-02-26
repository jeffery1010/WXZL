using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPWeb.Web.Areas.GlobalM.Controllers.IMGX
{
    using DAL.IMGX;
    using Entity.IMGX;
    using ERPWeb.Web.Model;

    public class WorkStationController : Controller
    {
        // GET: GlobalM/WorkStation
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search()
        {
            ViewData["Model"] = "";//外面传进来的机种
            return View("~/Areas/GlobalM/Views/WorkStation/Search.cshtml");
        }
        public ActionResult SearchDefaultModel(string model)
        {
            ViewData["Model"] = model;//外面传进来的机种
            return View("~/Areas/GlobalM/Views/WorkStation/Search.cshtml");
        }
        [HttpPost]
        public ActionResult GetWS()
        {
            try
            {
                WorkStation record = new WorkStation()
                {
                    id = 0,
                    code = "",
                    name = "",
                    modelid=-1,
                    fsequence=-1,
                    status=-1
                };
                List<WorkStation> allList = WorkStationDAL.getWorkStation(record);
                allList.Insert(0, new WorkStation() { id = -1, code = "全选", name = "全选" });
                //return Json(new { total = allList.Count, rows = Json(resultList).Data });
                return Json(Json(allList).Data);
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }
        [HttpPost]
        public ActionResult GetWorkStation(int page, int rows, WorkStation record)
        {
            try
            {
                record.id = 0;
                if (string.IsNullOrEmpty(record.code)) record.code = "";
                if (string.IsNullOrEmpty(record.name)) record.name = "";
                List<WorkStation> allList = WorkStationDAL.getWorkStation(record);
                List<WorkStation> resultList = allList;
                resultList = resultList.Skip((rows * page - rows)).Take(rows).ToList<WorkStation>();
                return Json(new { total = allList.Count, rows = Json(resultList).Data });
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetWorkStationByModel(string model)
        {
            RespEntity result = new RespEntity();
            try
            {
                RespEntity modelResp = new ModelController().GetModel();
                List<Model> modelList = null;
                if (modelResp.Code==200&&modelResp.total>0)
                {
                    modelList = modelResp.rows as List<Model>;
                    int modelid = modelList.Where(x => x.name.Equals(model)).FirstOrDefault().id;
                    WorkStation record = new WorkStation()
                    {
                        id = 0,
                        code = "",
                        name = "",
                        modelid=modelid,
                        status=1,
                    };
                    List<WorkStation> resultList = WorkStationDAL.getWorkStation(record);
                    result.Code = 200;
                    result.Message = "SUCCESS";
                    result.total = resultList.Count;
                    result.rows = resultList;
                }
                else
                {
                    result.Code = 500;
                    result.Message = modelResp.Message;
                    result.total = 0;
                    result.rows = null;
                }
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
        public ActionResult InsertWorkStation(WorkStation record)
        {
            try
            {
                RBAC response = WorkStationDAL.putWorkStation(record, "INSERT");
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(new { Code = 500, Message = "新增失败：" + e.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdateWorkStation(WorkStation record)
        {
            try
            {
                RBAC response = WorkStationDAL.putWorkStation(record, "UPDATE");
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(new { Code = 500, Message = "修改失败：" + e.Message });
            }
        }
    }
}