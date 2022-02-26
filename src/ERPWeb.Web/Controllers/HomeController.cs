using ERPWeb.Business.GlobalM;
using ERPWeb.Business.Common;
using ERPWeb.Util;
using System.Web.Mvc;
using System.Linq;
using ERPWeb.Web.Model;
using ERPWeb.Web.Model.EasyUIModel;
using System;
using ERPWeb.Web.Controllers;
using System.Diagnostics;
using System.Management;
using System.Web;
using ERPWeb.Entity.Power;
using System.Web.UI;
using System.Security.Principal;
using System.Net;
using System.Web.Security;
using System.DirectoryServices;
using ERPWeb.Web.Helper;

namespace ERPWeb.Web
{
    public class HomeController : BaseMvcController
    {
        HomeBusiness _homeBus { get; } = new HomeBusiness();

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        [IgnoreLogin]
        public ActionResult Login()
        {
            ViewData["UserNo"] = "";
            ViewData["Password"] = "";

            if (Request.Cookies["UserNo"] != null)
            {
                ViewData["UserNo"] = Request.Cookies["UserNo"].Value;
            }
            if (Request.Cookies["Password"] != null)
            {
                ViewData["Password"] = Request.Cookies["Password"].Value;
            }
            Operator.Logged();
            //if (Operator.Logged())
            //{

            //    string loginUrl = Url.Content("~/");
            //    string script = $@"    
            //                        <html>
            //                            <script>
            //                                top.location.href = '{loginUrl}';
            //                            </script>
            //                        </html>
            //                        ";
            //    return Content(script);
            //}

            return View();
        }

        public ActionResult Desktop()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        #endregion

        #region 获取数据



        #endregion

        #region 提交数据

        [IgnoreLogin]
        public ActionResult SubmitLogin(string UserNo, string password, bool IsCheck)
        { 
            AjaxResult res = new AjaxResult();
            try
            {
                //验证登录
                RBAC record = new LoginController().CheckUserLogin(UserNo, password);
                UserInfo ui = record.Data as UserInfo;

                if (record.Code == 200)
                {
                    this.RememberUser(UserNo, password, IsCheck, ui);
                    res.Count = 0;
                    res.ErrorCode = 0;
                    res.Success = true;
                    res.Msg = "请求成功！";
                }
                else
                {
                    
                        res.Count = 0;
                        res.ErrorCode = 0;
                        res.Success = false;
                        res.Msg = res.Msg; 
                }
            }
            catch (Exception er)
            {
                res.Count = 0;
                res.ErrorCode = 0;
                res.Success = false;
                res.Msg = er.Message;
            }
            return Content(res.ToJson());
        } 
        private void RememberUser(string UserNo, string password, bool IsCheck, UserInfo ui)
        {
            if (IsCheck)
            {
                Response.Cookies["UserNo"].Value = UserNo;
                Response.Cookies["UserNo"].Expires = DateTime.Now.AddDays(30);
                Response.Cookies["Password"].Value = password;
                Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
            } 
            else
            {
                Response.Cookies["UserNo"].Expires = DateTime.Now.AddSeconds(-1);
                Response.Cookies["Password"].Expires = DateTime.Now.AddSeconds(-1);
            }  
            Util.SessionHelper.Session["UserId"] = ui.UserId;
            Util.SessionHelper.Session["UserInfo"] = ui;
            Util.SessionHelper.Session["OrgList"] = new OrganizationController().GetOrganizationByUserNo(ui.UserNo);//保存组织
            Util.SessionHelper.Session["UserNo"] = ui.UserNo;
        }

        /// <summary>
        /// 注销
        /// </summary>
        public ActionResult Logout()
        {
            Operator.Logout();

            return Success("注销成功！");
        }

        #endregion
         

    }
}