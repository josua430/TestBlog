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
    public class GlosasController : Controller
    {
        // GET: Marcas
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

        //Método para cargar la grilla
        [HttpPost]
        public JsonResult LoadGrid()
        {
            try
            {
                //Lista a retornar o mostrar en la grilla
                var Lista = new List<Models.Glosas>();

                //using (var context = new Entity.Entities())
                //{
                //    //Consulta a la tabla
                //    var Glosas = context.GLOSAS.ToList();
                //    foreach (var item in Glosas)
                //    {
                //        //Se agregan a la lista
                //        Lista.Add(new Models.Glosas
                //        {
                //            __GlosaST = item.GLOSA_ST,
                //            GlosaPQR = item.GLOSA_PQR,
                //            GlosaNormalizacion = item.GLOSA_NORMALIZACION,
                //            IdGlosa = (int)item.IDGLOSA
                //        });
                //    }
                    return Json(Lista);
                //}
            }
            catch (Exception)
            {
                return null;
            }

        }

        #region Update
        [HttpGet]
        public ActionResult Update(string id)
        {
            //Se crea modelo 
            var model = new Models.Glosas();
            try
            {
                int IdElemento = int.Parse(id);
                //Se abre conexion al entity framework
                //using (var context = new Entity.Entities())
                //{
                //    var temp = context.GLOSAS.FirstOrDefault(t => t.IDGLOSA == IdElemento);
                //    if (temp == null)
                //    {
                //        return RedirectToAction("Index", "Glosas");
                //    }
                //    model.__GlosaST = temp.GLOSA_ST;
                //    model.GlosaPQR = temp.GLOSA_PQR;
                //    model.GlosaNormalizacion = temp.GLOSA_NORMALIZACION;
                //    model.IdGlosa = IdElemento;
                //}
            }
            catch (Exception)
            {

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(Models.Glosas objGlosa)
        {
            try
            {
                //var user = (Entity.USUARIO)Session[Helpers.Constant.USUARIO];
                //CultureInfo en = new CultureInfo("es-CO");
                //Thread.CurrentThread.CurrentCulture = en;
                //using (var context = new Entity.Entities())
                //{

                //    //elemento a actualizar
                //    var update = context.GLOSAS.FirstOrDefault(t => t.IDGLOSA == objGlosa.IdGlosa);
                //    //Se actualiza
                //    update.GLOSA_ST = objGlosa.__GlosaST.Trim();
                //    update.GLOSA_PQR = objGlosa.GlosaPQR.Trim();
                //    update.GLOSA_NORMALIZACION = objGlosa.GlosaNormalizacion.Trim();
                //    context.SaveChanges();
                    
                //    TempData["Success"] = "¡Glosas modificadas con exito!";
                    return RedirectToAction("Index", "Glosas");
                //}
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.InnerException;
            }
            return View(objGlosa);
        }

        #endregion
    }
}