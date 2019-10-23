using Blog.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.DirectoryServices;

namespace Blog.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// GET Login
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Validate Login data
        /// </summary>
        /// <param name="data">data for input login</param>
        /// <returns>string to validate input</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public string Login(Login data)
        {
            string strUsername = data.UserName;
            string strPassword = data.Password;
            try
            {
                if (strUsername != "testBlog" && strPassword != "testBlog")
                {
                    return "0";
                }
                Session[Helpers.Constant.USUARIO] = "testBlog";
                Session[Helpers.Constant.PERFILES] = 1;
                FormsAuthentication.SetAuthCookie("testBlog", false);
                return "1";
            }
            catch
            {
                //usuario no existe o clave erronea
                return "-1";
            }
        }

        /// <summary>
        /// Process application logout 
        /// </summary>
        /// <returns>Action result</returns>
        public ActionResult LogOut()
        {
            Session[Helpers.Constant.USUARIO] = null;
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Login");

        }

    }
}