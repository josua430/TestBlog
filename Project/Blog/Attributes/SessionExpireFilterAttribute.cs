using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Attributes
{
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            // check if session is supported
            if (ctx == null || ctx.Session["Usuario"] == null)
            {
                // check if a new session id was generated
                filterContext.Result = new RedirectResult("~/Login/Index");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}