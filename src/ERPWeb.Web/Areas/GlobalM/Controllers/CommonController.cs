using System.Web.Mvc;

namespace ERPWeb.Web
{
    public class CommonController : Controller
    {
        public ActionResult ShowBigImg(string url)
        {
            ViewData["url"] = url;
            return View();
        }
    }
}