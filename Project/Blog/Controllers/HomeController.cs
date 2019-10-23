using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            if (TempData["Error"] != null && !String.IsNullOrEmpty(TempData["Error"].ToString()))
            {
                ViewBag.Error = TempData["Error"].ToString();
            }
            if (TempData["Success"] != null && !String.IsNullOrEmpty(TempData["Success"].ToString()))
            {
                ViewBag.Success = TempData["Success"].ToString();
            }

            return View();
        }

    }
}