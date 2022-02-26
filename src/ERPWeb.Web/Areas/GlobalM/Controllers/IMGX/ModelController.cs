using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPWeb.Web.Areas.GlobalM.Controllers.IMGX
{
    using Entity.IMGX;
    using DAL.IMGX;
    using ERPWeb.Web.Model;

    public class ModelController : Controller
    {
        // GET: GlobalM/Model
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetModel(int page, int rows, Model record)
        {
            try
            {
                record.id = 0;
                if (string.IsNullOrEmpty(record.code)) record.code = "";
                if (string.IsNullOrEmpty(record.name)) record.name = "";
                List<Model> allList = ModelDAL.getModel(record);
                List<Model> resultList = allList;
                resultList = resultList.Skip((rows * page - rows)).Take(rows).ToList<Model>();
                return Json(new { total = allList.Count, rows = Json(resultList).Data });
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }

        public RespEntity GetModel()
        {
            RespEntity result = new RespEntity();
            try
            {
                Model record = new Model()
                {
                    id = 0,
                    code = "",
                    name = "",
                    isvalid = 1,
                };
                List<Model> resultList = ModelDAL.getModel(record);
                result.Code = 200;
                result.Message = "SUCCESS";
                result.total = resultList.Count;
                result.rows = resultList;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
                result.total = 0;
                result.rows = null;
            }
            return result;
        }

        [HttpPost]
        public ActionResult GetModelCombobox()
        {
            try
            {
                Model record = new Model()
                {
                    id = 0,
                    isvalid = -1,
                    code = "",
                    name = ""
                };
                List<Model> allList = ModelDAL.getModel(record);
                allList.Insert(0, new Model() { id = -1, name = "--全部--" });
                return Json(allList);
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }
        [HttpPost]
        public ActionResult GetModelComboboxNone()
        {
            try
            {
                Model record = new Model()
                {
                    id = 0,
                    isvalid = -1,
                    code = "",
                    name = ""
                };
                List<Model> allList = ModelDAL.getModel(record);
                return Json(allList);
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetModelInit(string type = "")
        {
            RespEntity result = new RespEntity();
            try
            {
                Model record = new Model()
                {
                    id = 0,
                    isvalid = -1,
                    code = "",
                    name = ""
                };

                List<Model> allList = null;
                if (string.IsNullOrEmpty(type))
                {
                    allList = ModelDAL.getModel(record);
                }
                else if ("OP".Equals(type))
                {
                    allList = ModelDAL.getModel(record).Where(x => !x.name.Equals("ACX")).ToList();
                }
                else if ("AC".Equals(type))
                {
                    allList = ModelDAL.getModel(record).Where(x => x.name.Equals("ACX")).ToList();
                }

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
        public ActionResult InsertModel(Model record)
        {
            try
            {
                RBAC response = ModelDAL.putModel(record, "INSERT");
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(new { Code = 500, Message = "新增失败：" + e.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdateModel(Model record)
        {
            try
            {
                RBAC response = ModelDAL.putModel(record, "UPDATE");
                return Json(response);
            }
            catch (Exception e)
            {
                return Json(new { Code = 500, Message = "修改失败：" + e.Message });
            }
        }
    }
}