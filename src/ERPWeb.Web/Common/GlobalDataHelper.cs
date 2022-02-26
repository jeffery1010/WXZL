using ERPWeb.Entity.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWeb.Web.Common
{
   public class GlobalDataHelper
    {
        public static UserInfo GetLoginUser() {
            UserInfo ui = (UserInfo)Util.SessionHelper.Session["UserInfo"];
            return ui;
        }
        public static string GetLoginUserNo()
        {
            string ui = (string)Util.SessionHelper.Session["UserNo"];
            return ui;
        }
    }
}
