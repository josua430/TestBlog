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
                clsDataBaseMethodsPosts objDB = new clsDataBaseMethodsPosts();
                bool blnUserIsAuthenticated = Session[Blog.Helpers.Constant.USUARIO] == null ? false : true;
                return Json(objDB.ListPosts(blnUserIsAuthenticated));
            }
            catch (Exception)
            {
                return null;
            }

        }

        #region Insert

        /// <summary>
        /// Get for Insert new Post
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Insert()
        {
            var model = new Models.Post();
            if (TempData["Error"] != null && !String.IsNullOrEmpty(TempData["Error"].ToString()))
            {
                ViewBag.Error = TempData["Error"].ToString();
            }
            if (TempData["Success"] != null && !String.IsNullOrEmpty(TempData["Success"].ToString()))
            {
                ViewBag.Success = TempData["Success"].ToString();
            }

            if (Session[Blog.Helpers.Constant.USUARIO] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View(model);
        }

        /// <summary>
        /// Insert data for new post
        /// </summary>
        /// <param name="objPost">object with data to insert</param>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult Insert(Models.Post objPost)
        {
            try
            {
                clsDataBaseMethodsPosts objDB = new clsDataBaseMethodsPosts();
                if (objDB.ExistsTitle(objPost.strTitle))
                {
                    TempData["Error"] = "¡Post title already exists!";
                    return RedirectToAction("Index", "Posts");
                }
                objDB.Insert(objPost);

                TempData["Success"] = "¡Post Created!";
                return RedirectToAction("Index", "Posts");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(objPost);
        }
        #endregion

    }
}