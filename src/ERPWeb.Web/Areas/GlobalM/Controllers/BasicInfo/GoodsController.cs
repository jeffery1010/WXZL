using ERPWeb.Business.BasicInfo;
using ERPWeb.Business.EntityExtend;
using ERPWeb.Entity.BasicInfo;
using ERPWeb.Util;
using System;
using System.Web.Mvc;

namespace ERPWeb.Web
{
    public class GoodsController : BaseMvcController
    {
        GoodsBusiness _goodsBusiness = new GoodsBusiness();

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetSortList() {
            GoodSortBusiness gsb = new GoodSortBusiness();
            return Content(gsb.GetDataList().ToJson());
        }
        public ActionResult Form(int id)
        {
            var theData = id.IsNullOrEmpty() ? new GoodsExtend() : _goodsBusiness.GetTheData(id);

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
            var dataList = _goodsBusiness.GetDataList(condition, keyword, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(GoodsExtend goods)
        {
            string b64 = "";
            
            Goods theData = ERPWeb.Util.Helper.ClassConvert.Copy<GoodsExtend, Goods>(goods);// (Goods)goods;
            theData.Chtime = DateTime.Now;
            theData.ChUserNo = Common.GlobalDataHelper.GetLoginUserNo();
           
            if (!string.IsNullOrEmpty(goods.GoodImgBase64))
            {
                b64 = Util.ImgHelper.GetBase64String(goods.GoodImgBase64);
                theData.Image = Util.ImgHelper.GetImgBytesFromBase64(b64); 
            }
            if (theData.Id.IsNullOrEmpty())
            {
                theData.Id =-1;
                theData.CreateTime = DateTime.Now;
                theData.CreateUserNo = Common.GlobalDataHelper.GetLoginUserNo();
                _goodsBusiness.AddData(theData);
            }
            else
            {
                _goodsBusiness.UpdateData(theData);
            }

            return Success();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {
            _goodsBusiness.DeleteData(ids.ToList<string>());

            return Success("删除成功！");
        }

        #endregion
    }
}