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

        #region ��ͼ����

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

        #region ��ȡ����

        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <param name="keyword">�ؼ���</param>
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

        #region �ύ����

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="theData">���������</param>
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
        /// ɾ������
        /// </summary>
        /// <param name="theData">ɾ��������</param>
        public ActionResult DeleteData(string ids)
        {
            _goodSortBusiness.DeleteData(ids.ToList<string>());

            return Success("ɾ���ɹ���");
        }

        #endregion
    }
}