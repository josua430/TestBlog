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
    /// <summary>
    /// Controller for Login
    /// </summary>
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
                if (strUsername == "writer" && strPassword == "writer")
                {
                    Session[Helpers.Constant.PERFIL] = "1";
                }
                else
                {
                    if (strUsername == "editor" && strPassword == "editor")
                    {
                        Session[Helpers.Constant.PERFIL] = "2";
                    }
                    else
                    {
                        Session[Helpers.Constant.USUARIO] = null;
                        Session[Helpers.Constant.PERFIL] = null;
                        return "-1";
                    }
                }
                Session[Helpers.Constant.USUARIO] = strUsername;
                FormsAuthentication.SetAuthCookie(strUsername, false);
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
            Session[Helpers.Constant.PERFIL] = null;
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}