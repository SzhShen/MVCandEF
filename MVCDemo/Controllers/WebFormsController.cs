using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemo.Controllers
{
    public class WebFormsController : Controller
    {
        //
        // GET: /WebForms/
        public ActionResult Index()
        {
            return View("WebForm1");
        }
        public ActionResult WebForm1()
        {
            return View();
        }
	}
}