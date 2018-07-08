using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    // 練習使用各種 Active Result 方式
    public class ARController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewTest()
        {
            return View();
        }
    }
}