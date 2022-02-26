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

    public class CellController : Controller
    {
        // GET: GlobalM/Cell
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View("~/Areas/GlobalM/Views/Cell/Search.cshtml");
        }
        public ActionResult GetLineByModel(string model) {
            RespEntity result = new RespEntity();
            try
            {
                RespEntity modelResp = new ModelController().GetModel();
                List<Model> modelList = null;
                if (modelResp.Code == 200 && modelResp.total > 0)
                {
                    modelList = modelResp.rows as List<Model>;
                    int modelid = modelList.Where(x => x.name.Equals(model)).FirstOrDefault().id;
                    Cell record = new Cell()
                    {
                        id = 0,
                        code = "",
                        name = "",
                        modelid = modelid,
                        isvalid = 1,
                    };
                    List<Cell> resultList = CellDAL.getCell(record).OrderBy(x=>x.code).ToList<Cell>();
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
        public ActionResult GetCell(int page, int rows, Cell record)
        {
            try
            {
                record.id = 0;
                if (string.IsNullOrEmpty(record.code)) record.code = "";
                if (string.IsNullOrEmpty(record.name)) record.name = "";
                List<Cell> allList = CellDAL.getCell(record);
                List<Cell> resultList = allList;
                resultList = resultList.Skip((rows * page - rows)).Take(rows).ToList<Cell>();
                return Json(new { total = allList.Count, rows = Json(resultList).Data });
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult InsertCell(Cell record)
        {
            try
            {
                RBAC response = CellDAL.putCell(record, "INSERT");
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(new { Code = 500, Message = "新增失败：" + e.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdateCell(Cell record)
        {
            try
            {
                RBAC response = CellDAL.putCell(record, "UPDATE");
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(new { Code = 500, Message = "修改失败：" + e.Message });
            }
        }
    }
}