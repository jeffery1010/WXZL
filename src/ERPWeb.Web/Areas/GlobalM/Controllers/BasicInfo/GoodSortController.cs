using ERPWeb.Business.BasicInfo;
using ERPWeb.Entity.BasicInfo;
using ERPWeb.Util;
using System;
using System.Web.Mvc;

namespace ERPWeb.Web
{
    public class GoodSortController : BaseMvcController
    {
        GoodSortBusiness _goodSortBusiness = new GoodSortBusiness();

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new GoodSort() : _goodSortBusiness.GetTheData(id);

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
            var dataList = _goodSortBusiness.GetDataList(condition, keyword, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }
        public ActionResult GetGoodSortList()
        {
            var dataList = _goodSortBusiness.GetDataList();

            return Content(dataList.ToJson());
        }
        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(GoodSort theData)
        {
            if(theData.Id.IsNullOrEmpty())
            {
                theData.Id =-1;

                _goodSortBusiness.AddData(theData);
            }
            else
            {
                _goodSortBusiness.UpdateData(theData);
            }

            return Success();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {
            _goodSortBusiness.DeleteData(ids.ToList<string>());

            return Success("删除成功！");
        }

        #endregion
    }
}