using AutoMapper;
using ERPWeb.Business.GlobalM;
using ERPWeb.DataRepository;
using ERPWeb.Entity.GlobalM;
using ERPWeb.Entity.Power;
using ERPWeb.Util;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPWeb.Web
{
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// 程序启动时执行
        /// 注：重新编译后执行
        /// </summary>
        protected void Application_Start()
        {
            //注册路由
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //注册全局异常捕捉器
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            InitAutoMapper();
            InitEF();
        }

        /// <summary>
        /// 初始化AutoMapper
        /// </summary>
        private void InitAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserInfo, Base_UserModel>();
            });
        }

        /// <summary>
        /// EF预热
        /// </summary>
        private void InitEF()
        {
            Task.Run(() =>
            {
                var db = DbFactory.GetRepository();
                //db.GetIQueryable<UserInfo>().ToList();
            });
        }
    }
}
