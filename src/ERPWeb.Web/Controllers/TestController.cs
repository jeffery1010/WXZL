using ERPWeb.Business;
//using ERPWeb.DotNettyRPC;
using System.Web.Mvc;
using ERPWeb.Business.Base_SysManage;

namespace ERPWeb.Web
{
    public interface IHello
    {
        string SayHello(string msg);
    }
    public class Hello : IHello
    {
        public string SayHello(string msg)
        {
            return msg;
        }
    }

    public class TestController : BaseMvcController
    {
        TestBiz _TestBiz = new TestBiz();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            _TestBiz.NewTestData();
            return View();
        }
        [CheckParamNotEmpty("aa")]
        public ActionResult Test()
        { 
            return Success();
        }
    }
}