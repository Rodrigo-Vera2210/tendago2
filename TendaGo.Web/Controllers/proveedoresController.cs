using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using Newtonsoft.Json;
using TendaGo.Common;
using TendaGo.Web.Models;
using TendaGo.Web.Infraestructura;
using TendaGo.Web.ApplicationServices;

namespace TendaGo.Web.Controllers
{
    [Authorize]
    public class proveedoresController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConsultarProveedores()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult ConsultarProveedores(string textoBusqueda, string filtroBusqueda)
        {
            try
            {
                bool identification = (filtroBusqueda == "1");

                var providers = ProveedorAppService.ConsultarProveedores(textoBusqueda, identification);

                return PartialView("ProveedoresResult", providers);
            }
            catch (ApplicationServicesException ex)
            {
                return Json(new { Success = false, Mensaje = ex.ApiErrorResponse.UserMessage, Error = ex.ApiErrorResponse }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Mensaje = ex.Message, Error = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
             
        }

        public ActionResult MantProveedor(int id)
        {
            if (id != 0)
            {
                
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult MantProveedor(ProveedorModel model)
        {
            return View();
        }

        public ActionResult NuevoProveedor()
        {
            ViewBag.CountriesList = new SelectList(GetCountries(), "Id", "Pais");
            return PartialView(new ProveedorModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NuevoProveedor(ProveedorModel model)
        {
            var providerDto = new ProviderDto
            {
                Id = model.Id,
                TipoIdentificacion = model.TipoIdentificacion,
                Identificacion = model.Identificacion,
                RazonSocial = model.RazonSocial,
                Direccion = model.Direccion,
                Celular = model.Celular,
                Correo = model.Correo,
                IdPais = model.IdPais,
                IpIngreso = AppServiceBase.ObtenerIp(),
                UsuarioIngreso = User.Identity.Name,
                IdEstado = model.IdEstado ? (short)1 : (short)0
            };
            return SaveProveedor(providerDto);
        }

        private JsonResult SaveProveedor(ProviderDto model)
        {
            try
            {
                var providerSaved = ProveedorAppService.GuardarProveedor(model);

                return Json(new { Success = true, Mensaje = "Registro Guardado", prov = providerSaved }, JsonRequestBehavior.AllowGet);
            }
            catch (ApplicationServicesException ex)
            {
                return Json(new { Success = false, Mensaje = ex.ApiErrorResponse.UserMessage, Error = ex.ApiErrorResponse }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Mensaje = ex.Message, Error = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        private List<CountryDto> GetCountries()
        {
            return CatalogosAppService.ObtenerPaises();
        }
        
    }
}