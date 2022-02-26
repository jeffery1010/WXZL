using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPWeb.Business.Lot;
using ERPWeb.Business.PMG;
using ERPWeb.Entity.SPC;
using ERPWeb.Util;
using ERPWeb.Entity.MacEntity;
using ERPWeb.DAL.MacDAL;

namespace ERPWeb.Web.Areas.GlobalM.Controllers.PMG
{
    public class MachineController :  BaseMvcController
    {


        public ActionResult SubModelIndex()
        {
            return View();
        }

        public ActionResult SubModelChange() {
            return View();
        }

        public ActionResult GrtWastePercent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetGrtWastePercent(string modelLine, string shift, string checkDate,GrtWastePercent record)
        {
            try
            {
                string[] datelist = checkDate.Split(char.Parse("-"));
                string CDate = string.Empty;
                string Date = string.Empty;
                for (int i = 0; i < datelist.Length; i++)
                {
                     if (datelist[i].Length <= 1)
                    {
                        CDate = "0" + datelist[i];
                    }
                    else
                    {
                        CDate = datelist[i];
                    }
                    if (Date == "")
                    {
                        Date = CDate;
                    }
                    else
                    {
                        Date = Date + CDate;
                    }
                }
                List<GrtWastePercent> allList = MachineSubmodelDAL.getAllGrtWaste(modelLine, shift, Date);
                List<GrtWastePercent> resultList = allList;
                if (!string.IsNullOrEmpty(record.ModelLine))
                {
                    resultList = resultList.ToList();
                }
                return Json(new { total = allList.Count, rows = Json(resultList).Data });
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult GetMachineSubmodel(int page, int rows, MachineSubmodel record)
        {
            try
            {
                List<MachineSubmodel> allList = MachineSubmodelDAL.getAllMachineSubmodel();
                List<MachineSubmodel> resultList = allList;
                if (!string.IsNullOrEmpty(record.PCName))
                {
                    resultList = resultList.Where(x => x.PCName.Contains(record.PCName)).ToList<MachineSubmodel>();
                }
                resultList = resultList.Skip((rows * page - rows)).Take(rows).ToList<MachineSubmodel>();
                return Json(new { total = allList.Count, rows = Json(resultList).Data });
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult SelectMachineSubOne(int Id)
        {
            MachineSubmodel record = null;
            try
            {
                record = MachineSubmodelDAL.getAllMachineSubmodel().Where(x => x.Id == Id).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Json(record);
        }

        [HttpPost]
        public ActionResult GetMachine(int rows,int page)
        {
            try
            {
                List<Submodel> allList = SubmodelDAL.getAllSubmodel();
                List<Submodel> resultList = allList;
                resultList = resultList.Skip((rows * page - rows)).Take(rows).ToList<Submodel>();
                return Json(new { total = allList.Count, rows = Json(resultList).Data });
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }


        [HttpPost]
        public ActionResult PutMacSubRelation(int pcid,string newsubids)
        {
            try
            {
                string[] result = MachineSubmodelDAL.PutMacSubRelation(pcid, newsubids);
                return Json(new { code = result[0].ToString(), msg = result[1].ToString() });
            }
            catch (Exception ex)
            {
                return Json(new { code = "0", msg = ex.Message });
            }
        }


        [HttpPost]
        public ActionResult GetLineSubmodel(int page, int rows)
        {
            try
            {
                List<LineSubmodel> allList = MachineSubmodelDAL.getAllLineSubmodel();
                List<LineSubmodel> resultList = allList.Skip((rows * page - rows)).Take(rows).ToList<LineSubmodel>();
                
                return Json(new { total = allList.Count, rows = Json(resultList).Data });
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
        }

        public ActionResult SelectLineSubOne(int Id)
        {
            LineSubmodel record = null;
            try
            {
                record = MachineSubmodelDAL.getAllLineSubmodel().Where(x => x.Id == Id).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Json(record);
        }

        [HttpPost]
        public ActionResult PutMacLineSub(int submodel, string tbname)
        {
            try
            {
                string[] result = MachineSubmodelDAL.PutMacLineSub(submodel, tbname);
                return Json(new { code = result[0].ToString(), msg = result[1].ToString() });
            }
            catch (Exception ex)
            {
                return Json(new { code = "0", msg = ex.Message });
            }
        }



        #region jeffery
        MachineBiz _MachineBiz = new MachineBiz();
        // GET: GlobalM/Repair
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new MachineModel() : _MachineBiz.GetTheData(id);

            return View(theData);
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public ActionResult GetDataList(string keyword, Pagination pagination)
        {
            var dataList = _MachineBiz.GetDataList("name", keyword, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }
        public ActionResult SaveData(MachineModel theData)
        {
            AjaxResult ar = _MachineBiz.SaveData(theData);
            return JsonContent(ar.ToJson());
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {
            AjaxResult ar = _MachineBiz.DeleteData(ids);

            return JsonContent(ar.ToJson());
        }
        /// <summary>
        /// 获取角色列表
        /// 注：无分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMacTypeList_NoPagin(int mtid = 0)
        {  
            return Content("");
        }
        public ActionResult GetRepairTypeList_NoPagin(int id = 0)
        {
            var dataList = _MachineBiz.GetRepairTypeList(id);

            return Content(dataList.ToJson());
        }
        #endregion

    }
}