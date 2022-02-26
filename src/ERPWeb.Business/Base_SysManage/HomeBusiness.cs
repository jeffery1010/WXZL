using ERPWeb.Business.Common;
using ERPWeb.Entity.GlobalM;
using ERPWeb.Util;
using System.Linq;
using System;
using ERPWeb.Entity.Power;

namespace ERPWeb.Business.GlobalM
{
    public class HomeBusiness:BaseBusiness<UserInfo>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserNo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AjaxResult SubmitLogin(string UserNo, string password)
        {
            try {
                if (UserNo.IsNullOrEmpty() || password.IsNullOrEmpty())
                    return Error("账号或密码不能为空！");
                password = password.ToMD5String();
                var theUser = GetIQueryable().Where(x => x.UserNo == UserNo && x.Password == password).FirstOrDefault();
                if (theUser != null)
                {
                    Operator.Login(theUser.UserNo);
                    return Success();
                }
                else
                    return Error("账号或密码不正确！");
            }
            catch (Exception er) {
                return Error("错误，"+er.Message);
            }
            
        }
    }
}
