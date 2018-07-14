using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class MBController : BaseController
    {
        // GET: MB
        public ActionResult Index()
        {
            return View();
        }

        // 示範 ViewData.Model
        public ActionResult ViewDataModelDemo()
        {
            var data = "這是 ViewData.Model Demo";
            ViewData.Model = data;
            return View();
        }

        // 示範 ViewBag.KeyName
        // 骨子裡用的是 ViewData
        public ActionResult ViewBagDemo()
        {
            ViewBag.Text = "這是 ViewBag Demo";

            ViewData["Data"] = db.Client.Take(5).ToList();

            return View();
        }

        // 示範 ViewData["KeyName"]
        public ActionResult ViewDataDemo()
        {
            ViewData["Text"] = @"這是 ViewData[""Text""] Demo";
            return View();
        }

        // 示範 TempData ["KeyName"]
        /* TempData 使用情境
         * 當表單資料送出後，先透過 TempDataSave 取得輸入的資料
         * 中間需要處理輸入的資料儲存到 DB
         * 接著在跳轉到完成頁面
        */
        public ActionResult TempDataSave()
        {
            TempData["Text"] = @"這是 TempData[""Text""] Demo";
            return RedirectToAction("TempDataDemo");
        }

        public ActionResult TempDataDemo()
        {
            return View();
        }
    }
}