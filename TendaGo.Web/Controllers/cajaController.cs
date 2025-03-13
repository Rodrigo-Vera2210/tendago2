using AutoMapper;
using TendaGo.Common;
using TendaGo.Web.ApplicationServices;
using TendaGo.Web.Infraestructura;
using TendaGo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Threading.Tasks;
using System.IdentityModel.Claims;

namespace TendaGo.Web.Controllers
{
    [Authorize]
    public class cajaController : Controller
    {
        // GET: caja
        public ActionResult Index()
        {
            return RedirectToAction("cierre");
        }

        public ActionResult cierre()
        {
            var model = DetallePagos();
            model.FechaApertura = DateTime.Now;

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult cierre(CierreCajaViewModel model)
        {
            try
            {
                //var model2 = DetallePagos(model.FechaApertura.Add(model.HoraApertura), model.SaldoInicial, model.TotalEgresos);

                if (model.Detalles == null || model.Detalles.Count == 0)
                {
                    throw new Exception("No existen ventas en las fechas seleccionadas!");
                }

                if (model.FechaCierre > DateTime.Now)
                {
                    throw new Exception("La fecha de cierre no puede ser mayor a la fecha actual!");
                }

                model.FechaCierre = model.FechaCierre ?? DateTime.Now;

                if (model.FechaApertura > model.FechaCierre)
                {
                    throw new Exception("La fecha de apertura no puede ser mayor a la fecha de cierre!");
                }
                 
                var request = Mapper.Map<CashBalanceRequestModel>(model);
                request.FechaIngreso = DateTime.Now;
                request.UsuarioIngreso = User.Identity.Name;
                request.IpIngreso = AppServiceBase.ObtenerIp();

                var result = CobrosAppService.GuardarCierreCaja(request);

                // Despues de guardar debe redireccionar a la pagina de impresion:
                return Json(new { success = 1, message = "El documento se guardo con exito!", result }, JsonRequestBehavior.AllowGet);
            
            }
            catch (Exception ex)
            {
                return Json(new { success = 0, message = ex.Message, result = default(CierreCajaViewModel), error = ex.ToString() }, JsonRequestBehavior.AllowGet);
                //throw new Exception("No se pudo guardar el recibo. ", ex);
            } 
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> ConsultarCierreReporte(DateTime? fechaApertura, TimeSpan? horaApertura, string format = "EXCEL")
        {
            if(fechaApertura == null)
            {
                fechaApertura = DateTime.Today;
            }

            var idcajero = AppServiceBase.Empresa.FlujoInventario ? string.Empty : UserAppService.AppUser.Id;
            var idEmpresa = User.Identity.GetIdEmpresa();
            var idLocal = Session.GetIdLocal() ?? 0;
            var fechaDesde = fechaApertura.Value.Add(horaApertura.Value).ToString();
            var lastDay = new DateTime(fechaApertura.Value.Year, fechaApertura.Value.Month, fechaApertura.Value.Day, 23, 59, 59);
            var fechaFin = AppServiceBase.Empresa.Id == 1 ? lastDay : DateTime.Now;
            var fechaHasta = fechaFin.ToString();

            var response = await CobrosAppService.DownloadCierreCajaAsync(idEmpresa, idLocal, fechaDesde, fechaHasta, idcajero, format);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                var contentType = response.Content.Headers.ContentType?.MediaType;
                return File(result, contentType);
            }

            return Content(await response.Content.ReadAsStringAsync(), response.Content.Headers.ContentType?.MediaType);
        }

        [HttpPost]
        public ActionResult detalle(DateTime FechaApertura, TimeSpan HoraApertura, decimal SaldoInicial = 0M)
        {
            var model = DetallePagos(FechaApertura.Add(HoraApertura), SaldoInicial);

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult resumen(DateTime FechaApertura, TimeSpan HoraApertura, decimal SaldoInicial = 0M, decimal TotalEgresos = 0M)
        {
            var model = DetallePagos(FechaApertura.Add(HoraApertura), SaldoInicial, TotalEgresos);

            return PartialView(model);
        }

        private CierreCajaViewModel DetallePagos(DateTime? fechainicio = default, decimal SaldoInicial = 0M, decimal TotalEgresos = 0M)
        {
            if (fechainicio == null)
            {
                fechainicio = DateTime.Today;
            }

            var idcajero = AppServiceBase.Empresa.FlujoInventario ? string.Empty : UserAppService.AppUser.Id;
            var idLocal = Session.GetIdLocal() ?? 0;
            var lastDay = new DateTime(fechainicio.Value.Year, fechainicio.Value.Month, fechainicio.Value.Day, 23, 59, 59);
            var fechafin = AppServiceBase.Empresa.Id == 1 ? lastDay : DateTime.Now;
            var result = CobrosAppService.ResumenPagos(idLocal, fechainicio, fechafin, idcajero);
            var fecha = result.Count > 0 ? result.Min(x => x.Fecha) : DateTime.Today;

            // Crear una cultura personalizada con el formato de fecha deseado
            CultureInfo customCulture = new CultureInfo(CultureInfo.CurrentCulture.Name);
            customCulture.DateTimeFormat.ShortDatePattern = "d/M/yyyy H:mm:ss";
            // Establecer la cultura personalizada como cultura actual
            CultureInfo.CurrentCulture = customCulture;


            var model = new CierreCajaViewModel
            {
                IdLocal = idLocal,
                FechaApertura = fecha.Date,
                HoraApertura = fecha.TimeOfDay,
                IdCajero = AppServiceBase.AppUser.Id,
                IdEmpresa = AppServiceBase.Empresa.Id,
                SaldoInicial = SaldoInicial,
                TotalEgresos = TotalEgresos,
                Detalles = Mapper.Map<List<DetalleRecibosViewModel>>(result)
            };

            return model;
        }
    }
}