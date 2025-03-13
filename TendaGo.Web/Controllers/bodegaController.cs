using AutoMapper;
using TendaGo.Common;
using TendaGo.Web.ApplicationServices;
using TendaGo.Web.Infraestructura;
using TendaGo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace TendaGo.Web.Controllers
{
    [Authorize]
    public class bodegaController : Controller
    {
        // GET: bodega
        public ActionResult Index()
        {
            ViewBag.Accion = "Consultar";
            ViewBag.Mantenimiento = true;

            var notasPedidoViewModel = new List<NotaPedidoViewModel>();
            return View("ConsultarNotasPedido", notasPedidoViewModel);
        }

        [HttpPost]
        public ActionResult Consultar(OrderSearchModel model)
        {

            ViewBag.Accion = model.accion;
            ViewBag.Mantenimiento = true;
            var pedidos = GetOutputs(model);

            Session[$"BodegaSearchModel_{ViewBag.Accion}"] = model;

            return PartialView(pedidos);
        }

        private List<NotaPedidoViewModel> GetOutputs(OrderSearchModel model)
        {
            model.tipoTransaccion = TransactionType.SalidaBodega;

            var notasPedidoDto = InventarioAppService.SearchOutputs(IdLocalSelected, model) ?? new List<OutputDto>();

            return Mapper.Map<List<NotaPedidoViewModel>>(notasPedidoDto)?.OrderByDescending(m => m.Id).ToList();

        }


        public int IdLocalSelected => Session.GetIdLocal() ?? 0;
        

        public ActionResult Resumen(string id)
        {
            OutputDto notaPedidoDto = NotaPedidoAppService.GetOutputById(id); 
            if (notaPedidoDto == null)
            {
                ViewBag.ErrorMessage = $"No se encontró la Nota de Pedido # {id}";
                return View("Error");
            }

            var notaPedidoModel = Mapper.Map<NotaPedidoModel>(notaPedidoDto);
            ViewBag.ActionUrl = Url.Action(Request.Form["Action"] ?? "");

            return View("MantNotaPedido_Resumen", notaPedidoModel);
        }

        public ActionResult Editar(string id)
        {
            var notaPedidoModel = NotaPedidoAppService.GetOutputById(id);
            Session[id] = notaPedidoModel;
            
            var model = notaPedidoModel?.ToModel();

            if (string.IsNullOrEmpty(model?.Id))
            {
                ViewBag.ErrorMessage = $"No se encontró el Pedido de Bodega # {id}";
                return View("Error");
            }

            model.LocalSeleccionado = UserAppService.ObtenerLocal(model.IdLocal).Local;
            
            return View("MantNotaPedido", model);
        }

        [HttpGet]
        public ActionResult Aprobar()
        {
            var notasPedidoDto = NotaPedidoAppService
                .GetOutputsByLocal(IdLocalSelected, TransactionStatus.EnProceso, TransactionType.SalidaBodega)
                .OrderBy(m => m.FechaHoraEntregaPropuesta)
                .ToList();

            var notasPedidoViewModel = Mapper.Map<List<NotaPedidoViewModel>>(notasPedidoDto);

            ViewBag.Accion = "Aprobar";
            return View("ConsultarNotasPedido", notasPedidoViewModel);
        }

        [HttpPost]
        public JsonResult Aprobar(AprobacionNotaPedidoViewModel aprobacionNotaPedido)
        {
            var mensaje = "Se ha producido un error.";
            try
            { 
                if (!ValidarStockProductos(aprobacionNotaPedido, out string msg))
                {
                    return Json(new { success = false, mensaje = msg }, JsonRequestBehavior.AllowGet);
                }

                var datosAprobacionNotaPedidoDto = Mapper.Map<SalesOrderApprovalDto>(aprobacionNotaPedido);
                datosAprobacionNotaPedidoDto.Usuario = User.Identity.Name;
                datosAprobacionNotaPedidoDto.Ip = AppServiceBase.ObtenerIp();

                var respuesta = NotaPedidoAppService.AprobarNotaPedido(datosAprobacionNotaPedidoDto);

                if (respuesta != null)
                {
                    return Json(new { success = true, mensaje = "La nota de pedido [" + aprobacionNotaPedido.Id + "] ha sido aprobada correctamente.", url = Url.Action("Resumen", new { id = respuesta.Id }) }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = false, mensaje = "Se ha producido un error." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mensaje = ex.Message, error = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }


        }



        [HttpGet]
        public ActionResult Empaquetar()
        {
            var notasPedidoDto = NotaPedidoAppService
                .GetOutputsByLocal(IdLocalSelected, TransactionStatus.Aprobada, TransactionType.SalidaBodega)
                .Where(m => m.IdEstado!=0)
                .OrderBy(m => m.FechaHoraEntregaPropuesta)
                .ToList();

            var notasPedidoViewModel = Mapper.Map<List<NotaPedidoViewModel>>(notasPedidoDto);
            ViewBag.Accion = "Empaquetar";
            return View("ConsultarNotasPedido", notasPedidoViewModel);
        }

        [HttpPost]
        public JsonResult Empaquetar(EmpaquetadoNotaPedidoViewModel empaquetadoNotaPedido)
        {
            var totalEmpaquetados = empaquetadoNotaPedido.DetalleNotaPedido.Count(m => m.Empaquetado);

            if (totalEmpaquetados != empaquetadoNotaPedido.DetalleNotaPedido.Count)
            {
                return Json(new { success = false, mensaje = "Debe seleccionar todos los articulos empaquetados." }, JsonRequestBehavior.AllowGet);
            }


            var datosEmpaquetadoNotaPedidoDto = Mapper.Map<SalesOrderPackingDto>(empaquetadoNotaPedido);
            datosEmpaquetadoNotaPedidoDto.Usuario = User.Identity.Name;
            datosEmpaquetadoNotaPedidoDto.Ip = AppServiceBase.ObtenerIp();

            var respuesta = NotaPedidoAppService.EmpaquetarNotaPedido(datosEmpaquetadoNotaPedidoDto);

            if (respuesta != null)
            {
                return Json(new { success = true, mensaje = "La nota de pedido [" + empaquetadoNotaPedido.Id + "] ha sido empaquetada correctamente.", url = Url.Action("Resumen", new { id = respuesta.Id }) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false, mensaje = "Se ha producido un error." }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Revisar()
        {
            var notasPedidoDto = NotaPedidoAppService
                .GetOutputsByLocal(IdLocalSelected, TransactionStatus.Empaquetada, TransactionType.SalidaBodega)
                .OrderBy(m => m.FechaHoraEntregaPropuesta)
                .ToList();

            var notasPedidoViewModel = Mapper.Map<List<NotaPedidoViewModel>>(notasPedidoDto);
            ViewBag.Accion = "Revisar";
            return View("ConsultarNotasPedido", notasPedidoViewModel);
        }

        [HttpPost]
        public JsonResult Revisar(RevisadoNotaPedidoViewModel revisadoNotaPedido)
        {
            var totalRevisados = revisadoNotaPedido.DetalleNotaPedido.Count(m => m.Revisado);

            if (totalRevisados != revisadoNotaPedido.DetalleNotaPedido.Count)
            {
                return Json(new { success = false, mensaje = "Debe seleccionar todos los articulos revisados." }, JsonRequestBehavior.AllowGet);
            }
            var paquetes = revisadoNotaPedido.DetalleEmpaquetado?.Count ?? 0;

            if (paquetes < 1)
            {
                return Json(new { success = false, mensaje = "Debe especificar los tipos de empaque utilizados." }, JsonRequestBehavior.AllowGet);
            }

            var datosRevisadoNotaPedidoDto = Mapper.Map<SalesOrderReviewDto>(revisadoNotaPedido);
            datosRevisadoNotaPedidoDto.Usuario = User.Identity.Name;
            datosRevisadoNotaPedidoDto.Ip = AppServiceBase.ObtenerIp();

            
            var respuesta = BodegaAppServices.RevisarNotaPedido(datosRevisadoNotaPedidoDto);

            if (respuesta != null)
            {
                return Json(new { success = true, mensaje = "La nota de pedido [" + revisadoNotaPedido.Id + "] ha sido revisada correctamente.", url = Url.Action("Resumen", new { id = respuesta.Id }) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false, mensaje = "Se ha producido un error." }, JsonRequestBehavior.AllowGet);
        }




        [HttpGet]
        public ActionResult Entregar()
        {
            var notasPedidoDto = NotaPedidoAppService
                .GetOutputsByLocal(IdLocalSelected, TransactionStatus.Revisada, TransactionType.SalidaBodega)
                .OrderBy(m => m.IdFormaPago)
                .ThenBy(m => m.FechaHoraEntregaPropuesta)
                .ToList();

            var notasPedidoViewModel = Mapper.Map<List<NotaPedidoViewModel>>(notasPedidoDto);

            ViewBag.Accion = "Entregar";
            return View("ConsultarNotasPedido", notasPedidoViewModel);
        }

        [HttpPost]
        public JsonResult Entregar(EntregaNotaPedidoViewModel entregaNotaPedido)
        {  
            var datosEntregaNotaPedidoDto = Mapper.Map<SalesOrderDeliverDto>(entregaNotaPedido);
            //var cuentasCobrarDto = Mapper.Map<List<CuentaPorCobrarDto>>(entregaNotaPedido.CuentasPorCobrar);
            //var cuentasCobrarPagadasDto = (cuentasCobrarDto != null ? cuentasCobrarDto.Where(x => x.ValorPagado > 0).ToArray() : new CuentaPorCobrarDto[0]);

            if (datosEntregaNotaPedidoDto.CuentasPorCobrar == null)
            {
                datosEntregaNotaPedidoDto.CuentasPorCobrar = new List<ReceivableDto>();
            }

            datosEntregaNotaPedidoDto.Usuario = User.Identity.Name;
            datosEntregaNotaPedidoDto.Ip = AppServiceBase.ObtenerIp();

            var respuesta = NotaPedidoAppService.EntregarNotaPedido(datosEntregaNotaPedidoDto);

            if (respuesta != null)
            {
                return Json(new { success = true, mensaje = "La nota de pedido [" + datosEntregaNotaPedidoDto.Id + "] ha sio entregada correctamente.", url = Url.Action("Resumen", new { id = respuesta.Id }) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false, mensaje = "Se ha producido un error." }, JsonRequestBehavior.AllowGet);
        }




        private bool ValidarStockProductos(AprobacionNotaPedidoViewModel model, out string msg)
        {
            foreach (var item in model.DetalleNotaPedido)
            {
                item.CantidadTotalDisponible = ObtenerStock(item.IdProducto, model.Id);

                if (item.Cantidad > item.CantidadTotalDisponible)
                {
                    msg = $"La cantidad seleccionada para el producto [{item.CodigoInterno} - {item.DescripcionProducto}], ({item.Cantidad} {item.DesTipoUnidad}) es mayor a la cantidad disponible ({item.CantidadTotalDisponible} {item.DesTipoUnidad})";
                    return false;
                }
            }

            msg = null;
            return true;
        }

        public decimal ObtenerStock(int idProducto, string idSalida = null)
        {
            var prod = ObtenerProducto(idProducto);

            // Este proceso lo realizo para corregir el problema del Stock utilizado en la nota de pedido al crearla
            // Ya que cuando esta es creada debe agregarse la cantidad existente en la salida
            if (!string.IsNullOrEmpty(idSalida))
            {
                var salida = (Session[idSalida] as OutputDto) ?? NotaPedidoAppService.GetOutputById(idSalida);
                var detalle = salida.DetalleNotaPedido.FirstOrDefault(m => m.IdProducto == idProducto);
                if (detalle != null)
                {
                    prod.Stock += detalle.Cantidad;
                }
            }

            return prod?.Stock ?? 0M;
        }

        public ProductDto ObtenerProducto(int idProducto, int? idLocal = null)
        {
            return ProductosAppService.ObtenerStockProducto(idProducto, idLocal ?? IdLocalSelected)
                ?? ProductosAppService.ObtenerProducto(idProducto);
        }

        [HttpPost]
        public JsonResult AnularPedido(string id, string motivo)
        {
            try
            {
                var model = new OutputDeleteDto
                {
                    IdSalida = id,
                    FechaIngreso = DateTime.Now,
                    UsuarioIngreso = User.Identity.Name,
                    IpIngreso = AppServiceBase.ObtenerIp(),
                    Observaciones = motivo
                };

                var result = NotaPedidoAppService.DeleteOutput(model);

                if (result)
                {
                    return Json(new { success = true, mensaje = "La nota de pedido [" + id + "] ha sido anulada correctamente." }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = false, mensaje = "Se ha producido un error." }, JsonRequestBehavior.AllowGet);
            }
            catch (ApplicationServicesException ex)
            {
                return Json(new { success = false, mensaje = ex.ApiErrorResponse.UserMessage, error = ex.ApiErrorResponse }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mensaje = ex.Message, error = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Descargar(string id, string format = "PDF")
        {
            var response = await InventarioAppService.DownloadOutputAsync(id, format);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                var contentType = response.Content.Headers.ContentType?.MediaType;
                return File(result, contentType);
            }

            return Content(await response.Content.ReadAsStringAsync(), response.Content.Headers.ContentType?.MediaType);
        }

    }
}