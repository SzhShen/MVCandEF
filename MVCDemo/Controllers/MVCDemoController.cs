using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemo.Controllers
{
    public class MVCDemoController : Controller
    {
        //
        // GET: /MVCDemo/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HelperTest()
        {
            return View();
        }
	}
}