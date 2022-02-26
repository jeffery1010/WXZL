using ERPWeb.Business.GlobalM;
using ERPWeb.Util;

namespace ERPWeb.Business.Common
{
    /// <summary>
    /// 操作者
    /// </summary>
    public static class Operator
    {
        /// <summary>
        /// 当前操作者UserId
        /// </summary>
        public static string UserId
        {
            get
            {
                if (GlobalSwitch.RunModel == RunModel.LocalTest)
                    return "Admin";
                else
                    return SessionHelper.Session["UserId"]?.ToString();
            }
        }

        public static Base_UserModel Property { get => UserBusiness.GetTheUser(UserId); }

        #region 操作方法

        /// <summary>
        /// 是否已登录
        /// </summary>
        /// <returns></returns>
        public static bool Logged()
        {
            return !UserId.IsNullOrEmpty();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="UserId">用户逻辑主键Id</param>
        public static void Login(string UserId)
        {
            SessionHelper.Session["UserId"] = UserId;
        }

        /// <summary>
        /// 注销
        /// </summary>
        public static void Logout()
        {
            SessionHelper.Session["UserId"] = null;
        }

        /// <summary>
        /// 判断是否为超级管理员
        /// </summary>
        /// <returns></returns>
        public static bool IsAdmin()
        {
            return UserId == "Admin";
        }

        #endregion
    }
}
