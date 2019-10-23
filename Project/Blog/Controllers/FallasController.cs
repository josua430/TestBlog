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
    public class FallasController : Controller
    {
        // GET: Fallas
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
                var Lista = new List<Models.Fallas>();

                using (var context = new Entity.blog_dbEntities())
                {
                    ////Consulta a la tabla
                    //var Fallas = context.FALLAS.OrderBy(d => d.NOMBREFALLA).ToList();
                    //foreach (var item in Fallas)
                    //{
                    //    //Se agregan a la lista
                    //    Lista.Add(new Models.Fallas
                    //    {
                    //        NombreFalla = item.NOMBREFALLA,
                    //        IdFalla = (int)item.IDFALLA
                    //    });
                    //}
                    return Json(Lista);
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

        #region Insert
        [HttpGet]
        public ActionResult Insert()
        {
            var model = new Models.Fallas();
            if (TempData["Error"] != null && !String.IsNullOrEmpty(TempData["Error"].ToString()))
            {
                ViewBag.Error = TempData["Error"].ToString();
            }
            if (TempData["Success"] != null && !String.IsNullOrEmpty(TempData["Success"].ToString()))
            {
                ViewBag.Success = TempData["Success"].ToString();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Insert(Models.Fallas crtFalla)
        {
            try
            {
                //var user = (Entity.USUARIO)Session[Helpers.Constant.USUARIO];
                //CultureInfo en = new CultureInfo("es-CO");
                //Thread.CurrentThread.CurrentCulture = en;

                //using (var context = new Entity.Entities())
                //{
                //    //Se valida si ya existe
                //    var validate = context.FALLAS.FirstOrDefault(d => d.NOMBREFALLA == crtFalla.NombreFalla);
                //    if (validate != null)
                //    {
                //        TempData["Error"] = "¡La Falla ya existe!";
                //        return RedirectToAction("Index", "Fallas");
                //    }
                //    //Se crea modelo y se almacena en la base de datos
                //    var model = new Entity.FALLAS();
                //    model.NOMBREFALLA = crtFalla.NombreFalla;
                //    context.FALLAS.Add(model);
                //    context.SaveChanges();
                //}
                //Log

                TempData["Success"] = "¡Falla Creada!";
                return RedirectToAction("Index", "Fallas");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(crtFalla);
        }
        #endregion

        #region Update
        [HttpGet]
        public ActionResult Update(string id)
        {
            //Se crea modelo 
            var model = new Models.Fallas();
            try
            {
                int IdElemento = int.Parse(id);
                //Se abre conexion al entity framework
                //using (var context = new Entity.Entities())
                //{
                //    var temp = context.FALLAS.FirstOrDefault(t => t.IDFALLA == IdElemento);
                //    if (temp == null)
                //    {
                //        return RedirectToAction("Index", "Fallas");
                //    }
                //    model.NombreFalla = temp.NOMBREFALLA;
                //    model.IdFalla = IdElemento;
                //}
            }
            catch (Exception)
            {

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(Models.Fallas objFalla)
        {
            try
            {
                //var user = (Entity.USUARIO)Session[Helpers.Constant.USUARIO];
                //CultureInfo en = new CultureInfo("es-CO");
                //Thread.CurrentThread.CurrentCulture = en;
                //using (var context = new Entity.Entities())
                //{

                //    //elemento a actualizar
                //    var update = context.FALLAS.FirstOrDefault(t => t.IDFALLA == objFalla.IdFalla);
                //    //var oldUpdate = update.NOMBREFALLA;
                //    //validacion si ya existe un elemento con el mismo nombre
                //    var validate = context.FALLAS.FirstOrDefault(t => t.NOMBREFALLA.ToUpper().Trim() == objFalla.NombreFalla.ToUpper().Trim());
                //    if (validate != null)
                //    {

                //        if (objFalla.IdFalla != validate.IDFALLA)
                //        {
                //            TempData["Error"] = "¡Nombre de Falla ya existe!";
                //            return RedirectToAction("Index", "Fallas");
                //        }
                //    }
                //    //Se actualiza
                //    string strDatosLog = "Nombre: " + update.NOMBREFALLA + " por " + objFalla.NombreFalla.Trim();
                //    update.NOMBREFALLA = objFalla.NombreFalla.Trim();
                //    context.SaveChanges();
                    
                //    //Log
                //    using (var content = new Entity.Entities())
                //    {
                //        var log = new Entity.LOG();
                //        log.ACCION = "ACTUALIZACION DE Falla";
                //        log.OBSERVACION = "Actualizacion de Falla: " + strDatosLog;
                //        log.FECHA = DateTime.Now;
                //        log.USUARIO = user.NOMBRE_USUARIO;
                //        content.LOG.Add(log);
                //        content.SaveChanges();
                //    }
                //    TempData["Success"] = "¡Falla modificada con exito!";
                //    return RedirectToAction("Index", "Fallas");
                //}
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.InnerException;
            }
            return View(objFalla);
        }

        #endregion

        #region Delete
        [HttpGet]
        public JsonResult Delete(string id)
        {
            try
            {
                //var creator = (Entity.USUARIO)Session[Helpers.Constant.USUARIO];
                //using (var context = new Entity.Entities())
                //{
                //    int idElemento = int.Parse(id);
                //    var Falla = context.FALLAS.FirstOrDefault(p => p.IDFALLA == idElemento);

                //    if (Falla == null)
                //    {
                //        return Json(new { Message = "Falla no encontrada", Type = "Error" }, JsonRequestBehavior.AllowGet);
                //    }

                //    string strDatoFalla =  Falla.NOMBREFALLA;
                //    context.FALLAS.Remove(Falla);
                //    context.SaveChanges();

                //    var log = new Entity.LOG();
                //    log.ACCION = "ELIMINAR Falla";
                //    log.OBSERVACION = "Eliminar Falla: Nombre:" + Falla.NOMBREFALLA;
                //    log.FECHA = DateTime.Now;
                //    log.USUARIO = creator.NOMBRE_USUARIO;
                //    context.LOG.Add(log);
                //    context.SaveChanges();
                    return Json(new { Message = "Falla Eliminada", Type = "Exito" }, JsonRequestBehavior.AllowGet);

                //}
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Error:" + ex.Message, Type = "Error" }, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion
    }
}