using Newtonsoft.Json;
using RestSharp;
using TendaGo.Common;
using TendaGo.Web.ApplicationServices;
using TendaGo.Web.Infraestructura;
using TendaGo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Threading.Tasks;
using AutoMapper;

namespace TendaGo.Web.Controllers
{
    [Authorize]
    public class CobrosController : Controller
    {
         

        public ActionResult Consultar()
        {
            int idLocal = Session.GetIdLocal() ?? 0;
            var listaRecibos = CobrosAppService.ConsultaRecibos(idLocal);
            var model = listaRecibos.Select(m => m.ToViewModel()).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Consultar(ReciboSearchModel model)
        {

            int idLocal = Session.GetIdLocal() ?? 0;
            var listaRecibos = CobrosAppService.SearchRec(idLocal, model);
            var modelR = listaRecibos.Select(m => m.ToViewModel()).ToList();

            return View(modelR);
        }

        [HttpPost]
        public ActionResult ReciboReversar(string id)
        {
            int idLocal = Session.GetIdLocal() ?? 0;
            var result = CobrosAppService.ReciboReversar(id);
            var listaRecibos = CobrosAppService.ConsultaRecibos(idLocal);
            var model = listaRecibos.Select(m => m.ToViewModel()).ToList();
            return View("Consultar", model);
        }

        #region ################ Nuevo Recibo de Cobro ################ 


        // GET: Cobros
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect(Url.Content(FormsAuthentication.LoginUrl));
            }
             
            ViewBag.DocNumber = $"RECC-X-{DateTime.Now.ToString("yyyyMMdd")}-X";
  
            var model = new CobroDebitoViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult GuardarRecibo(CobroDebitoViewModel model)
        {
            try
            {
                var json = model.ToJson();

                if (ValidarRecibo(model))
                {
                    // Proceso de Guardado
                    if (this.TryValidateModel(model))
                    {
                        int idLocal = Session.GetIdLocal() ?? 0;
                        var result = CobrosAppService.GuardarCobranza(model, idLocal);

                        // Despues de guardar debe redireccionar a la pagina de impresion:
                        return Json(new { success = 1, message = "El documento se guardo con exito!", result }, JsonRequestBehavior.AllowGet);
                    }
                }

                throw new Exception("Por favor complete los datos del documento!");
            }
            catch (Exception ex)
            {
                return Json(new { success = 0, message = ex.Message, result = default(ReceiptDto), error = ex.ToString() }, JsonRequestBehavior.AllowGet);
                //throw new Exception("No se pudo guardar el recibo. ", ex);
            }
        }

        private bool ValidarRecibo(CobroDebitoViewModel model)
        {
            // PRIMERO: Elimino los detalles con cero.
            model.Detalles.RemoveAll(m => m.Valor == 0);
            model.IdEmpresa = AppServiceBase.Empresa.Id;

            // SEGUNDO: Valido que por lo menos tenga un detalle:
            if (model.Detalles == null || model.Detalles.Count == 0)
            {
                throw new Exception("Debe seleccionar al menos un detalle!");
            }

            // TERCERO: Valido que por lo menos tenga un tipo de pago:
            if (model.Pagos == null || model.Pagos.Count == 0)
            {
                throw new Exception("Debe seleccionar al menos una forma de pago!");
            }

            if (model.TotalPagos != model.Total)
            {
                throw new Exception("El saldo debe ser igual a 0");
            }

            return true;
        }


        /// <summary>
        /// Este metodo devuelve las cuotas pendientes de un cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CuotasPendientes(int id)
        {
            List<ReceivableDto> cuotas = null;
            try
            {
                cuotas = CobrosAppService.ObtenerCuotasPendientes(id);
            }
            catch (Exception ex) { ex.ToString(); }

            if (cuotas == null)
            {
                cuotas = new List<ReceivableDto>();
            }

            return PartialView("ListaCuotasPendientes", cuotas);
        }

        [HttpPost]
        public ActionResult ReciboVistaPreliminar(CobroDebitoViewModel model)
        {
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult ReciboVistaPreliminar(string id)
        {
            var model = ApplicationServices.CobrosAppService.ReciboPorId(id);
            //return PartialView(model);

            return PartialView("~/Views/Cobros/ReciboVistaPreliminar.cshtml", model.ToViewModel());
        }

        public ActionResult EstadoCuentas()
        {
            var model = new EntidadViewModel();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> DescargarCxc(EntidadViewModel model, string format = "PDF")
        {
            var response = await InventarioAppService.DownloadCxcAsync(model, format);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                var contentType = response.Content.Headers.ContentType?.MediaType;
                return File(result, contentType);
            }

            return Content(await response.Content.ReadAsStringAsync(), response.Content.Headers.ContentType?.MediaType);
        }

        #endregion

    }
}