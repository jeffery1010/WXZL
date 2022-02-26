using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPWeb.Entity.Power;
using ERPWeb.Web.Model;

namespace ERPWeb.Web.Common
{
    public class SystemUserManage
    {
        public static UserInfo GetUserInfo()
        {
            int userId = (int)Util.SessionHelper.Session["UserId"];
            UserInfo record = null;
            using (UserManageContext db =new UserManageContext())
            {
                record=db.UserInfo.Where(x => x.UserId == userId).FirstOrDefault();
            }
            return record;
        }
    }
}