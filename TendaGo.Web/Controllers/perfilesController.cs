using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using RestSharp;
using System.Web.Mvc;
using TendaGo.Common;
using TendaGo.Web.ApplicationServices;
using TendaGo.Web.Infraestructura;
using TendaGo.Web.Models;

namespace TendaGo.Web.Controllers
{
    [Authorize]
    public class perfilesController : Controller
    {
        public ActionResult index()
        {
            var modulos = ModulosAppServices.ObtenerModulos();
            return View();
        }

        public ActionResult pantallas()
        {
            return View();
        }

        public ActionResult GetProfilesForDisplays()
        {
            var profiles = UserAppService.ObtenerPerfiles();
            return PartialView(profiles);
        }

        public ActionResult GetDisplaysByModule(short idModule)
        {
            var displays = UserAppService.ObtenerPantallasPorModulo(idModule);
            return PartialView(displays);
        }

        public JsonResult GetModulos()
        {
            var modulos = ModulosAppServices.ObtenerModulos();
            return Json(modulos, JsonRequestBehavior.AllowGet);
        }
                
    }
}