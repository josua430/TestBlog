using System.Web;
using System.Web.Mvc;

namespace Blog
{
    /// <summary>
    /// FilterConfig class
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Register filters
        /// </summary>
        /// <param name="filters">GlobalFilterCollection</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
