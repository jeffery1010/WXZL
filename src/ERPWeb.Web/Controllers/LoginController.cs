using ERPWeb.Entity.Power;
using ERPWeb.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPWeb.Web.Model.EasyUIModel;

namespace ERPWeb.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserNo, string Password)
        { 
            UserInfo ui = new UserController().SelectByCode(UserNo);
            string result = string.Empty;
            if (ui == null)
            {
                //没有这个用户
                result = "NotFindUser";
            }
            else if (!(MD5Security.MD5Encrypt32(Password).Equals(ui.Password)))
            {
                //密码错误
                result = "PasswordError";
            }
            else
            {
                //登录成功
                result = "Success";
                Util.SessionHelper.Session["UserInfo"] = ui;//保存用户
                Util.SessionHelper.Session["UserNo"] = ui.UserNo;//保存用户
                Util.SessionHelper.Session["OrgList"] = new OrganizationController().GetOrganizationByUserNo(ui.UserNo);//保存组织
            }
            return Content(result);
        }

        public RBAC CheckUserLogin(string UserNo, string Password)
        {
            UserInfo ui = new UserController().SelectByCode(UserNo);
            RBAC record = new RBAC();
            if (ui == null)
            {
                record.Code = 404;
                record.Message = "账号不存在！";
            }
            else if (!(MD5Security.MD5Encrypt32(Password).Equals(ui.Password)))
            {
                record.Code = 500;
                record.Message = "密码错误!";
            }
            else
            {
                record.Code = 200;
                record.Message = "账号校验成功！";
                record.Data = ui; 
            }
            return record;
        }
    }
}



/*
 <div id="menu">
        <div class="menu_main" id="system_menu">
            <h2><i class="icon icon_menu_catalog"></i></h2>
            @foreach (var firstMenu in menus)
            {
                if (firstMenu.IsShow)
                {
                    <a class="main_item" href="javascript:void(0)" title="@firstMenu.Name"><i class="icon @firstMenu.Icon"></i><span>@firstMenu.Name.Substring(0, 2)</span></a>
                    <div class="menu_sub">
                        <dl>
                            @foreach (var secondMenu in firstMenu.SubMenus)
                            {
                                if (secondMenu.IsShow)
                                {
                                    <dt><span>@secondMenu.Name</span></dt>
                                    <dd>
                                        @foreach (var item in secondMenu.SubMenus)
                                        {
                                            if (item.IsShow)
                                            {
                                                <a href="javascript:;" onclick="Desktop.tabs.add('_panel_@(item.Url)','@(item.Name)','@item.Url')" class="sub_item">@item.Name</a>
                                            }
                                        }
                                    </dd>
                                }
                            }
                        </dl>
                    </div>
                }
            }
        </div>
    </div>
     
     */
