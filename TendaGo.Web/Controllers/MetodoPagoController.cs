using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TendaGo.Common;
using TendaGo.Web.ApplicationServices;
using TendaGo.Web.Models;

namespace TendaGo.Web.Controllers
{
    public class MetodoPagoController : Controller
    {
        // GET: MetodoPago
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMetodosPago()
        {
            var metodos = UserAppService.ObtenerMetodosPago();
            return PartialView(metodos);
        }

        public ActionResult PostMetodoPago()
        {
            return PartialView();
        }

        [HttpGet, Route("postMetodoPago/{id}")]
        public ActionResult PostMetodoPago(int id)
        {
            var metodo = UserAppService.ObtenerMetodosPagoByPK(id);

            return PartialView(metodo);
        }
        public ActionResult Guardar(PaymentMethodDto model)
        {
            var result = UserAppService.GuardarMetodosPago(model);
            if (result == null)
            { return Json(new { success = false, Mensaje = "Hay errores al mostrar el metodo. "  }, JsonRequestBehavior.AllowGet); }

            return Json(new { success = true, Mensaje = "Guardado con exito ",data =result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMetodoPago()
        {
            return PartialView();
        }
        public ActionResult Borrar(PaymentMethodDto model)
        {
            var result = UserAppService.BorrarMetodosPago(model);
            if (result == null)
            { return Json(new { success = false, Mensaje = "Hay errores al mostrar el metodo. " }, JsonRequestBehavior.AllowGet); }

            return Json(new { success = true, Mensaje = "Guardado con exito ", data = result }, JsonRequestBehavior.AllowGet);
        }
    }
}