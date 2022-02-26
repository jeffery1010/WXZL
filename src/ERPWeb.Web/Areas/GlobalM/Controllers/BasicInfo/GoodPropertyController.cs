using ERPWeb.Business.BasicInfo;
using ERPWeb.Entity.BasicInfo;
using ERPWeb.Util;
using System;
using System.Web.Mvc;

namespace ERPWeb.Web
{
    public class GoodPropertyController : BaseMvcController
    {
        GoodPropertyBusiness _goodPropertyBusiness = new GoodPropertyBusiness();

        #region ��ͼ����

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new GoodProperty() : _goodPropertyBusiness.GetTheData(id);

            return View(theData);
        }

        #endregion

        #region ��ȡ����

        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <param name="keyword">�ؼ���</param>
        /// <returns></returns>
        public ActionResult GetDataList(string condition, string keyword, Pagination pagination)
        {
            var dataList = _goodPropertyBusiness.GetDataList(condition, keyword, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }
        public ActionResult GetGoodPropertyList()
        {
            var dataList = _goodPropertyBusiness.GetGoodPropertyList();

            return Content(dataList.ToJson());
        }
        
        #endregion

        #region �ύ����

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="theData">���������</param>
        public ActionResult SaveData(GoodProperty theData)
        {
            if(theData.Id.IsNullOrEmpty())
            {
                theData.Id = 0;

                _goodPropertyBusiness.AddData(theData);
            }
            else
            {
                _goodPropertyBusiness.UpdateData(theData);
            }

            return Success();
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="theData">ɾ��������</param>
        public ActionResult DeleteData(string ids)
        {
            _goodPropertyBusiness.DeleteData(ids.ToList<string>());

            return Success("ɾ���ɹ���");
        }

        #endregion
    }
}