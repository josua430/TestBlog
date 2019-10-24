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
    /// Post controller
    /// </summary>
    public class PostsController : Controller
    {
        /// <summary>
        /// // GET: Blogs
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Index()
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
            return View();
        }

        /// <summary>
        /// Method to load grid
        /// </summary>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult LoadGrid()
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

        #region Update
        /// <summary>
        /// Get for update
        /// </summary>
        /// <param name="id">id of post</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult Update(string id)
        {

            if (Session[Blog.Helpers.Constant.USUARIO] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            //Se crea modelo 
            var model = new Models.Post();
            try
            {
                int IdElemento = int.Parse(id);
                //Se abre conexion al entity framework
                using (var context = new Entity.blog_dbEntities())
                {
                    var objBlog = context.posts.FirstOrDefault(t => t.post_id == IdElemento);
                    if (objBlog == null)
                    {
                        return RedirectToAction("Index", "Post");
                    }
                    model.strTitle = objBlog.post_title;
                    model.strText = objBlog.post_text;
                    model.strAuthor = objBlog.post_author;
                    model.dtChange = (DateTime)objBlog.post_change;
                    model.IdPost = (int)objBlog.post_id;
                    model.blnPublished = (bool)objBlog.post_published;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(model);
        }

        /// <summary>
        /// Save data for update
        /// </summary>
        /// <param name="objPost">object with data for update blog</param>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult Update(Models.Post objPost)
        {
            try
            {
                clsDataBaseMethodsPosts objDB = new clsDataBaseMethodsPosts();
                if (objDB.ExistsTitleAndIdForUpdate(objPost.IdPost.ToString(), objPost.strTitle))
                {
                    TempData["Error"] = "¡Post title already exists!";
                    return RedirectToAction("Index", "Posts");
                }
                objDB.Update(objPost);

                TempData["Success"] = "Update successfull!";
                return RedirectToAction("Index", "Posts");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.InnerException;
            }
            return View(objPost);
        }

        #endregion

        #region Delete
        /// <summary>
        /// Delete method
        /// </summary>
        /// <param name="id">post id for delete</param>
        /// <returns>Json with message</returns>
        [HttpGet]
        public JsonResult Delete(string id)
        {
            try
            {
                clsDataBaseMethodsPosts objDB = new clsDataBaseMethodsPosts();
                if (objDB.ExistsId(id))
                {
                    return Json(new { Message = "Post not found", Type = "Error" }, JsonRequestBehavior.AllowGet);
                }

                objDB.Delete(id);

                return Json(new { Message = "Post deleted", Type = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Error:" + ex.Message, Type = "Error" }, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion
    }
}