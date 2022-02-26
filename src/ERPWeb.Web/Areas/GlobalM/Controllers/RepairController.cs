using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPWeb.Business.Lot;
using ERPWeb.Util;
using ERPWeb.Entity.RepairEntity;
using ERPWeb.DAL.RepairDAL;

namespace ERPWeb.Web.Areas.GlobalM.Controllers
{
    public class RepairController : BaseMvcController
    {
        // GET: GlobalM/Repair
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetRepairType(int page, int rows, RepairType record)
        {
            try
            {
                List<RepairType> allList = RepairTypeDAL.getAllRepairType();
                List<RepairType> resultList = allList;
                if (!string.IsNullOrEmpty(record.code))
                {
                    resultList = resultList.Where(x => x.code.Contains(record.code)).ToList<RepairType>();
                }
                if (!string.IsNullOrEmpty(record.name))
                {
                    resultList = resultList.Where(x => x.name.Contains(record.name)).ToList<RepairType>();
                }
                if (record.inputtype!=null)
                {
                    resultList = resultList.Where(x => x.inputtype.Contains(record.inputtype)).ToList<RepairType>();
                }
                resultList = resultList.Skip((rows * page - rows)).Take(rows).ToList<RepairType>();
                return Json(new { total = allList.Count, rows = Json(resultList).Data });
            }
            catch (Exception ex)
            {
                return Json(new { total = 0, rows = ex.Message });
            }
            
        }


        [HttpPost]
        public ActionResult GetRepairPort(int page, int rows, RepairPort record) {
            
            try
            {
                List<RepairPort> allList = RepairPortDAL.getAllepairPort();
                List<RepairPort> resultList = allList.Skip((rows * page - rows)).Take(rows).ToList<RepairPort>();
                return Json(new { total = allList.Count, rows = Json(resultList).Data });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpPost]
        public ActionResult SelectPortOne(int Id) {
            RepairType record = null;
            try
            {
                record = RepairTypeDAL.getAllRepairType().Where(x=>x.Id==Id).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Json(record);
        }

        
        public ActionResult InsertRepairType(RepairType record) {
            try
            {
                RepairTypeDAL.putRepairType(record, "INSERT");
                //using (UserManageContext db = new UserManageContext())
                //{
                //    record.CreateTime = DateTime.Now;
                //    record.CreateUserId = 0;
                //    db.RoleInfo.Add(record);
                //    db.SaveChanges();
                //}
            }
            catch (Exception e)
            {
                return Content("新增失败：" + e.Message);
            }
            return Content("YES");
        }

        public ActionResult UpdateRepairType(RepairType record)
        {
            try
            {
                RepairTypeDAL.putRepairType(record, "UPDATE");
            }
            catch (Exception e)
            {
                return Content("修改失败：" + e.Message);
            }
            return Content("YES");
        }

        public ActionResult DeleteRepairType(int id)
        {
            string result = string.Empty;
            try
            {
                result = RepairTypeDAL.delRepairType(id);
            }
            catch (Exception e)
            {
                return Content("修改失败：" + e.Message);
            }
            return Content(result);
        }



        #region 【jeffery】
        RepairBiz _RepairBiz = new RepairBiz();

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new RepairModel() : _RepairBiz.GetTheData(id);

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
            var dataList = _RepairBiz.GetDataList("name", keyword, pagination);
     
            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }
        public ActionResult SaveData(RepairModel theData)
        {
            AjaxResult ar =_RepairBiz.SaveData(theData);
            return JsonContent(ar.ToJson()); 
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {
            AjaxResult ar =_RepairBiz.DeleteData(ids);

            return JsonContent(ar.ToJson());
        }
        /// <summary>
        /// 获取角色列表
        /// 注：无分页
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMacTypeList_NoPagin(int mtid=0)
        { 
            Pagination pagination = new Pagination
            {
                PageIndex = 1,
                PageRows = int.MaxValue
            }; 
            var dataList = _RepairBiz.GetMacTypes(mtid);

            return Content(dataList.ToJson());
        }
        public ActionResult GetUnitList_NoPagin(string name="")
        {
            Pagination pagination = new Pagination
            {
                PageIndex = 1,
                PageRows = int.MaxValue
            };
            var dataList = _RepairBiz.GetUnitList("name",name,pagination);

            return Content(dataList.ToJson());
        }
        
        public ActionResult GetRepairTypeList_NoPagin(int id = 0)
        { 
            var dataList = _RepairBiz.GetRepairTypeList(id);

            return Content(dataList.ToJson());
        }
        #endregion
    }
}