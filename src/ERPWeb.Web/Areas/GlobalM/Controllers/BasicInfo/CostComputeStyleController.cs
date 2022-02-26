using ERPWeb.Business.BasicInfo;
using ERPWeb.Entity.BasicInfo;
using ERPWeb.Util;
using System;
using System.Web.Mvc;

namespace ERPWeb.Web
{
    public class CostComputeStyleController : BaseMvcController
    {
        CostComputeStyleBusiness _costComputeStyleBusiness = new CostComputeStyleBusiness();

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new CostComputeStyle() : _costComputeStyleBusiness.GetTheData(id);

            return View(theData);
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public ActionResult GetDataList(string condition, string keyword, Pagination pagination)
        {
            var dataList = _costComputeStyleBusiness.GetDataList(condition, keyword, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }
        public ActionResult GetCostCompStyleList()
        { 
            return Content(_costComputeStyleBusiness.GetDataList().ToJson());
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(CostComputeStyle theData)
        {
            if(theData.Id.IsNullOrEmpty())
            {
                theData.Id = -1;

                _costComputeStyleBusiness.AddData(theData);
            }
            else
            {
                _costComputeStyleBusiness.UpdateData(theData);
            }

            return Success();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {
            _costComputeStyleBusiness.DeleteData(ids.ToList<string>());

            return Success("删除成功！");
        }

        #endregion
    }
}