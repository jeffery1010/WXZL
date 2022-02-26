using System.Web.Mvc;

namespace ERPWeb.Web
{
    public class GlobalMAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GlobalM";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        { 
            //if(!context.Routes.Contains(new System.Web.Routing.Route("GlobalM/{controller}/{action}/{id}",null)))
            context.MapRoute(
                "GlobalM_default",
                "GlobalM/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}