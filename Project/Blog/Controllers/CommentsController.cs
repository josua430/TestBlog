using Blog.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    /// <summary>
    /// Comment controller
    /// </summary>
    public class CommentsController : Controller
    {
        /// <summary>
        /// // GET: Comments
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Index(string id)
        {
            CultureInfo en = new CultureInfo("es-CO");
            Thread.CurrentThread.CurrentCulture = en;
            if (TempData["Error"] != null && !String.IsNullOrEmpty(TempData["Error"].ToString()))
            {
                ViewBag.Error = TempData["Error"].ToString();
            }
            if (TempData["Success"] != null && !String.IsNullOrEmpty(TempData["Success"].ToString()))
            {
                ViewBag.Success = TempData["Success"].ToString();
            }
            var model = new Models.Comments();
            model.intPostId = Convert.ToInt32(id);
            return View(model);
        }

        /// <summary>
        /// Method to load grid
        /// </summary>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult LoadGrid(string id)
        {
            try
            {
                clsDataBaseMethodsComments objDB = new clsDataBaseMethodsComments();
                if (id == null || id == "")
                {
                    id = "0";
                }
                int intValorId = Convert.ToInt32(id);
                return Json(objDB.ListComments(intValorId));
            }
            catch (Exception)
            {
                return null;
            }

        }

        #region Insert

        /// <summary>
        /// Get for Insert new comment
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Insert(string id)
        {
            if (TempData["Error"] != null && !String.IsNullOrEmpty(TempData["Error"].ToString()))
            {
                ViewBag.Error = TempData["Error"].ToString();
            }
            if (TempData["Success"] != null && !String.IsNullOrEmpty(TempData["Success"].ToString()))
            {
                ViewBag.Success = TempData["Success"].ToString();
            }

            var model = new Models.Comments();
            model.intPostId = Convert.ToInt32(id);
            return View(model);
        }

        /// <summary>
        /// Insert data for new comment
        /// </summary>
        /// <param name="objComment">object with data to insert</param>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult Insert(Models.Comments objComment)
        {
            try
            {
                clsDataBaseMethodsComments objDB = new clsDataBaseMethodsComments();
                objDB.Insert(objComment);

                TempData["Success"] = "¡Comment Created!";
                return RedirectToAction("Index", "Comments", new { id = objComment.intPostId });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(objComment);
        }
        #endregion

    }
}