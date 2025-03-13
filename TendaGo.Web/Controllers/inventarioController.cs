using AutoMapper;
using TendaGo.Common;
using TendaGo.Web.ApplicationServices;
using TendaGo.Web.Infraestructura;
using TendaGo.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using System.Net;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.XSSF.UserModel;

namespace TendaGo.Web.Controllers
{
    [Authorize]
    public class inventarioController : Controller
    {
        public int IdLocalSelected => Session.GetIdLocal() ?? 0;

        // GET: inventario
        public ActionResult Index()
        {
            return RedirectToAction("Consultar");
        }


        [HttpGet]
        public ActionResult Consultar()
        {
            ViewBag.Accion = "Consultar";
            ViewBag.Mantenimiento = true;

            var datos = new List<EntradaViewModel>();
            return View("ConsultarEntradas", datos);
        }

        [HttpPost]
        public ActionResult Consultar(OrderSearchModel model)
        {
            ViewBag.Accion = model.accion;
            ViewBag.Mantenimiento = true;

            var pedidos = GetInputs(model);

            Session[$"InventarioSearchModel_{ViewBag.Accion}"] = model;

            return PartialView(pedidos);
        }

        private List<EntradaViewModel> GetInputs(OrderSearchModel model)
        {
            model.tipoTransaccion = TransactionType.EntradaBodega;

            var result = InventarioAppService.SearchInputs(IdLocalSelected, model) ?? new List<InputDto>();

            return Mapper.Map<List<EntradaViewModel>>(result)?.OrderByDescending(m => m.Id).ToList();

        }


        [Authorize]
        public ActionResult Editar(string id)
        {
            var notaPedidoModel = InventarioAppService.GetInputById(id)?.ToModel();

            if (string.IsNullOrEmpty(notaPedidoModel?.Id))
            {
                ViewBag.ErrorMessage = $"No se encontró el Pedido de Bodega # {id}";
                return View("Error");
            }
            if (notaPedidoModel.TransaccionPadre != null)
            {
                OrderSearchModel modelSalida = new OrderSearchModel { transaccionPadre = notaPedidoModel.TransaccionPadre };
                var salida = InventarioAppService.SearchOutputs(0, modelSalida);


                if (salida != null && salida.Count > 0)
                {
                    var salidaEmpaquetada = NotaPedidoAppService.GetOutputById(salida[0].Id)?.ToModel();
                    if (salidaEmpaquetada.DetalleEmpaquetado != null && salidaEmpaquetada.DetalleEmpaquetado.Count() > 0)
                    {
                        foreach (var emp in salidaEmpaquetada.DetalleEmpaquetado)
                        {
                            if (notaPedidoModel.DetalleEmpaquetado == null)
                                notaPedidoModel.DetalleEmpaquetado = new List<EmpaquetadoViewModel>();

                            notaPedidoModel.DetalleEmpaquetado.Add(emp);
                        }
                    }
                    notaPedidoModel.Observaciones = salida[0].Observaciones;
                }                    
            }

            Session[id] = notaPedidoModel;

            return View("MantNotaPedido", notaPedidoModel);
        }

        [HttpGet]
        public ActionResult Recibir()
        {
            ViewBag.Accion = "ENTREGAR";

            var entradas = InventarioAppService
              .GetInputsByLocal(IdLocalSelected, TransactionStatus.Aprobada)
              .OrderByDescending(m => m.Fecha)
              .ToList();

            var model = Mapper.Map<List<EntradaViewModel>>(entradas);
            
            return View("ConsultarEntradas", model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Recibir(EntregaNotaPedidoViewModel modelo)
        {
            try
            {
                //InputDto notasPedido = new InputDto();
                var request = new RestRequest($"/warehouses/inventory/{modelo.Id}/receive", Method.POST);
                var restResponse = AppServiceBase.DefaultClient.Execute<InputDto>(request);
                if (restResponse != null)
                {
                    return Json(new
                    {
                        success = true,
                        mensaje = "La nota de pedido [" + modelo.Id + "] ha sio recibida correctamente.",
                        url = Url.Action("Resumen", new { id = modelo.Id })
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, mensaje = "Se ha producido un error." }, JsonRequestBehavior.AllowGet);
            }
            catch (ApplicationServicesException ex)
            {
                return Json(new
                {
                    success = false,
                    mensaje = ex.ApiErrorResponse.UserMessage,
                    error = ex.ApiErrorResponse
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mensaje = $"{ex.Message} {ex.InnerException?.Message}", error = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult Resumen(string id)
        {
            var entrada = InventarioAppService.GetInputById(id);

            OrderSearchModel modelSalida = new OrderSearchModel{ transaccionPadre = entrada.TransaccionPadre };
            var salida = InventarioAppService.SearchOutputs(0, modelSalida);


            if (salida != null && salida.Count > 0)            
                entrada.IdLocalOrigen = salida[0].IdLocal;

                var model = entrada?.ToModel();


            if (salida != null && salida.Count > 0)
            {
                var salidaEmpaquetada = NotaPedidoAppService.GetOutputById(salida[0].Id)?.ToModel();
                if (salidaEmpaquetada.DetalleEmpaquetado != null && salidaEmpaquetada.DetalleEmpaquetado.Count() > 0)
                {
                    foreach (var emp in salidaEmpaquetada.DetalleEmpaquetado)
                    {
                        if (model.DetalleEmpaquetado == null)
                            model.DetalleEmpaquetado = new List<EmpaquetadoViewModel>();

                        model.DetalleEmpaquetado.Add(emp);
                    }
                }
                model.Observaciones = salida[0].Observaciones;
            }       
            
            if (model == null)
            {
                ViewBag.ErrorMessage = $"No se encontró la Nota de Pedido # {id}";
                return View("Error");
            }

            ViewBag.ActionUrl = Url.Action(Request.Form["Action"] ?? "");

            return View("MantNotaPedido_Resumen", model);
        }


        // GET: inventario/Pedir
        public ActionResult Pedir()
        {
            var model = new TransferenciaViewModel
            {
                IdLocalDestino = IdLocalSelected,
                IdVendedor = User.Identity.Name,
                EstadoActual = "EN PROCESO"
            };
             
            return View(model);
        }



        // GET: inventario/Pedir
        public ActionResult GetItemDetails(int id)
        {
            var model = ProductosAppService
                .ObtenerStockProductoLocales(id)?
                .Where(x => x.IdLocal != Session.GetIdLocal())?
                .ToList();

            return PartialView(model);
        }

        public JsonResult GetLocales(int id)
        {
            var model = ProductosAppService
                .ObtenerStockProductoLocales(id)?
                .Where(x => x.IdLocal != Session.GetIdLocal())?
                .ToList();

            return Json(model ?? new List<LiteStockDto>(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLocalAct(int id)
        {
            var model = ProductosAppService
                .ObtenerStockProductoLocales(id)?
                .Where(x => x.IdLocal == Session.GetIdLocal())?
                .ToList();

            return Json(model ?? new List<LiteStockDto>(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Pedir(TransferenciaViewModel model)
        {
            try
            {
                if (model.Detalle == null || model.Detalle.Count == 0 || model.Detalle.Any(x => x.Cantidad == 0))
                {
                    return Json(new { success = false, message = "Necesita agregar productos para realizar la solicitud" }, JsonRequestBehavior.AllowGet);
                }
                if (model.Detalle.Any(x => !x.ItemValido))
                {
                    return Json(new { success = false, message = "Existen registros inválidos" }, JsonRequestBehavior.AllowGet);
                }

                model.IdLocalDestino = model.IdLocalDestino > 0 ? model.IdLocalDestino : Session.GetIdLocal() ?? 0;
                if (model.IdLocalDestino == 0)
                {
                    return Json(new { success = false, message = "Debe seleccionar el Local de Destino" }, JsonRequestBehavior.AllowGet);
                }

                var request = model.ToRequest();
                request.FechaIngreso = DateTime.Now;
                request.UsuarioIngreso = User.Identity.Name;
                request.IpIngreso = AppServiceBase.ObtenerIp();
                 
                var respuesta = InventarioAppService.SaveTransfer(request);

                return Json(new { success = true, message = "Su transferencia se ha guardado con éxito!", id = respuesta.FirstOrDefault()?.Entrada?.Id, data = respuesta }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message, error=ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        // GET: inventario/Merma
        public ActionResult Merma()
        {
            return View(new MermaViewModel());
        }

        [HttpPost]
        public JsonResult Merma(MermaViewModel model)
        {
            try
            {

                if (model.Detalle == null || model.Detalle.Count == 0 || model.Detalle.Any(x => x.IdProducto == 0))
                {
                    return Json(new RespuestaViewModel { Success = false, Mensaje = "Debe seleccionar algun articulo o producto para generar la compra." }, JsonRequestBehavior.AllowGet);
                }

                if (model.Detalle.Any(x =>  (x.IdTipoUnidad == 0 )))
                {
                    return Json(new RespuestaViewModel { Success = false, Mensaje = "Debe indicar la cantidad de articulos y el tipo de unidad para generar la compra." }, JsonRequestBehavior.AllowGet);
                }

                model.IdLocal = Session.GetIdLocal();
                model.Fecha = DateTime.Now;
                model.UsuarioIngreso = User.Identity.Name;
                model.IpIngreso = AppServiceBase.ObtenerIp();

                if (model.IdLocal == null)
                {
                    return Json(new RespuestaViewModel { Success = false, Mensaje = "Debe seleccionar el local de la Compra." }, JsonRequestBehavior.AllowGet);
                }
               
                //var ajusDto = Mapper.Map<MermaDto>(model);
                

                var result = InventarioAppService.SaveMerma(model);

                if (!string.IsNullOrEmpty(result?.Id))
                {
                    return Json(new { Success = true, Mensaje = "La compra [" + result.Id + "] ha sigo guardada correctamente." }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { Success = false, Mensaje = "Hay errores al guardar el ajuste." }, JsonRequestBehavior.AllowGet);
            }
            catch (ApplicationServicesException ex)
            {
                return Json(new { Success = false, Mensaje = ex.ApiErrorResponse?.UserMessage ?? ex.Message, Error = ex.ApiErrorResponse }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Mensaje = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: inventario/Transferencia
        public ActionResult Transferencia(DateTime? desde = null, DateTime? hasta = null)
        {
            if (desde == null)
            {
                desde = DateTime.Today;
            }
            
            if (hasta == null)
            {
                hasta = DateTime.Today.AddHours(23).AddMinutes(60);
            }

            var model = new TransferenciaViewModel
            {
                IdLocalDestino = IdLocalSelected,
                IdVendedor = User.Identity.Name,
                EstadoActual = "EN PROCESO"
            };

            var ventas = InventarioAppService.GetProductSales(IdLocalSelected, desde, hasta);

            model.Detalle = ventas.Select(m =>
                  new DetalleTransferenciaViewModel
                  {
                      IdProducto = m.IdProducto,
                      CodigoInterno = m.CodigoInterno,
                      DescripcionProducto = m.DescripcionProducto,
                      IdLocal = IdLocalSelected,
                      Cantidad = m.Cantidad,
                      Stock = m.Stock,
                      CantidadMinima = m.CantidadMinima,
                      IdTipoUnidad = m.Unidades.FirstOrDefault()?.Id ?? 0,
                      TipoUnidad = m.Unidades.FirstOrDefault()?.TipoUnidad ?? "No Disponible",
                      Unidades = m.Unidades.OrderBy(x => x.Producto).ThenBy(x => x.TipoUnidad)?.ToList(),
                      StockLocales = m.StockLocales
                  })?
                  .Where(x => x.StockLocales.Any(y => y.IdLocal==15 && y.StockInventario>0))
                  .OrderBy(x => x.StockLocales.Count != 0)?
                  .ThenBy(x => x.CodigoInterno)
                  .ToList();

            ViewData["PedirVendidos"] = true;


            return View("Pedir", model);
        }

        [HttpPost]
        public JsonResult AnularPedido(string id, string motivo)
        {
            try
            {
                var notaPedidoModel = InventarioAppService.GetInputById(id)?.ToModel();

                foreach (var recor in notaPedidoModel.DetalleNotaPedido)
                {
                    var stock = ProductosAppService
                    .ObtenerStockProductoLocales(recor.IdProducto)?
                    .Where(x => x.IdLocal == Session.GetIdLocal())?
                    .ToList();
                    if (stock==null || stock.Sum(x => x.StockInventario)<=0 )
                        return Json(new { success = false, mensaje = "No existe Stock para Anular." }, JsonRequestBehavior.AllowGet);
                }

                var model = new InputDeleteDto
                {
                    IdEntrada = id,
                    FechaIngreso = DateTime.Now,
                    UsuarioIngreso = User.Identity.Name,
                    IpIngreso = AppServiceBase.ObtenerIp(),
                };

                var result = CompraAppService.DeleteInput(model);

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
            var response = await InventarioAppService.DownloadOutputAsync(id, format, "Inventario");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                var contentType = response.Content.Headers.ContentType?.MediaType;
                return File(result, contentType);
            }

            return Content(await response.Content.ReadAsStringAsync(), response.Content.Headers.ContentType?.MediaType);
        }

        public ActionResult Reporte()
        {
            var model = new ProductoViewModel();
            return View(model);
        }
        
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Resumido(ProductoViewModel model , string format = "PDF")
        {
            var response = await InventarioAppService.DownloadInventoryAsync(model,format);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                var contentType = response.Content.Headers.ContentType?.MediaType;
                return File(result, contentType);
            }

            return Content(await response.Content.ReadAsStringAsync(), response.Content.Headers.ContentType?.MediaType);
        }

        public ActionResult Kardex()
        {
            var model = new ProductoViewModel();
            model.FechaDesde = DateTime.Today.ToString("dd/MM/yyyy");
            model.FechaHasta = DateTime.Today.ToString("dd/MM/yyyy");

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Kardex(ProductoViewModel model, string format = "PDF")
        {
            model.IdLocal = IdLocalSelected;

            var response = await InventarioAppService.DownloadKardexAsync(model, format);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                var contentType = response.Content.Headers.ContentType?.MediaType;
                return File(result, contentType);
            }

            return Content(await response.Content.ReadAsStringAsync(), response.Content.Headers.ContentType?.MediaType);
        }

        public ActionResult StockPorLocal()
        {
            var model = new StockLocalViewModel();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> ReporteStockPorLocal(StockLocalViewModel model, string format = "PDF")
        {
            var response = await InventarioAppService.DownloadStockbyLocalAsync(model, format);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                var contentType = response.Content.Headers.ContentType?.MediaType;
                return File(result, contentType);
            }

            return Content(await response.Content.ReadAsStringAsync(), response.Content.Headers.ContentType?.MediaType);
        }

        public ActionResult ReporteAjustes()
        {
            var model = new ProductViewModel();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> ConsultarAjustesReporte(ProductViewModel model, string format = "EXCEL")
        {
            var response = await InventarioAppService.DownloadMovimientosInventarioAsync(model, format);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                var contentType = response.Content.Headers.ContentType?.MediaType;
                return File(result, contentType);
            }

            return Content(await response.Content.ReadAsStringAsync(), response.Content.Headers.ContentType?.MediaType);
        }

        public ActionResult Importar()
        {
            //ViewBag.FormasPago = ObtenerFormasPago();
            //ViewBag.RucList = new SelectList(ObtenerRucs(), "Ruc", "Descripcion");
            //ViewBag.MonedasList = new SelectList(GetCurrencies(), "Id", "CodigoISO");
            return View(new MermaViewModel());
        }

        [HttpPost]
        public ActionResult Importar(MermaViewModel model)
        {
            try
            {

                if (model.Detalle == null || model.Detalle.Count == 0 || model.Detalle.Any(x => x.IdProducto == 0))
                {
                    return Json(new RespuestaViewModel { Success = false, Mensaje = "Debe seleccionar algun articulo o producto para generar la compra." }, JsonRequestBehavior.AllowGet);
                }

                if (model.Detalle.Any(x => (x.IdTipoUnidad == 0)))
                {
                    return Json(new RespuestaViewModel { Success = false, Mensaje = "Debe indicar la cantidad de articulos y el tipo de unidad para generar la compra." }, JsonRequestBehavior.AllowGet);
                }

                model.IdLocal = Session.GetIdLocal();
                model.Fecha = DateTime.Now;
                model.UsuarioIngreso = User.Identity.Name;
                model.IpIngreso = AppServiceBase.ObtenerIp();

                if (model.IdLocal == null)
                {
                    return Json(new RespuestaViewModel { Success = false, Mensaje = "Debe seleccionar el local de la Compra." }, JsonRequestBehavior.AllowGet);
                }

                //var ajusDto = Mapper.Map<MermaDto>(model);


                var result = InventarioAppService.SaveMerma(model);

                if (!string.IsNullOrEmpty(result?.Id))
                {
                    return Json(new { Success = true, Mensaje = "La compra [" + result.Id + "] ha sigo guardada correctamente." }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { Success = false, Mensaje = "Hay errores al guardar el ajuste." }, JsonRequestBehavior.AllowGet);
            }
            catch (ApplicationServicesException ex)
            {
                return Json(new { Success = false, Mensaje = ex.ApiErrorResponse?.UserMessage ?? ex.Message, Error = ex.ApiErrorResponse }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Mensaje = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ReadExcelFile()
        {
            int i = 1;
            try
            {
                HttpPostedFileBase files = Request.Files[0];
                ISheet sheet;
                bool isFileValid = true;
                string filename = Path.GetFileName(Server.MapPath(files.FileName));
                var fileExt = Path.GetExtension(filename);
                if (fileExt == ".xls")
                {
                    HSSFWorkbook hssfwb = new HSSFWorkbook(files.InputStream);
                    sheet = hssfwb.GetSheetAt(0);
                }
                else
                {
                    XSSFWorkbook hssfwb = new XSSFWorkbook(files.InputStream);
                    sheet = hssfwb.GetSheetAt(0);
                }

                List<DetalleTransferenciaViewModel> rows = new List<DetalleTransferenciaViewModel>();
                for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                {
                    IRow row = sheet.GetRow(rowIndex);
                    if (row != null) //null is when the row only contains empty cells   
                    {
                        i++;

                        var detalle = new DetalleTransferenciaViewModel();
                        string CodigoInterno = row.GetCell(0).GetString();
                        var idLocal = row.GetCell(1).GetString();
                        var cantidad = row.GetCell(2).GetNumber();

                        if (!string.IsNullOrEmpty(CodigoInterno))
                        {
                            var producto = ProductosAppService.GetProductByInternalCode(CodigoInterno);
                            var unidades = TipoUnidadAppService.ObtenerTiposUnidadPorProducto(producto.Id);

                            detalle.DescripcionProducto = producto.Producto;
                            detalle.IdProducto = producto.Id;
                            detalle.CodigoInterno = CodigoInterno;
                            detalle.IdLocal = Convert.ToInt32(idLocal);
                            detalle.TipoUnidad = unidades[0].TipoUnidad;
                            detalle.IdTipoUnidad = unidades[0].Id;
                            detalle.Cantidad = Convert.ToInt32(cantidad);
                            rows.Add(detalle);
                        }
                    }
                }

                ViewBag.IsFileValid = isFileValid;
                var model = new MermaViewModel { Detalle = rows };
                return PartialView("UploadedExcelData", model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(i.ToString());
                return Json(new { success = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ImportarPedir()
        {
            return View(new TransferenciaViewModel());
        }
        [HttpPost]
        public ActionResult ReadExcelFilePedir()
        {
            int i = 1;
            try
            {
                int idLocal = Session.GetIdLocal() ?? 0;
                HttpPostedFileBase files = Request.Files[0];
                var model = InventarioAppService.CargarProductos(idLocal, files);
                //ISheet sheet;
                bool isFileValid = true;

                //HttpPostedFileBase files = Request.Files[0];
                //ISheet sheet;
                //bool isFileValid = true;
                //string filename = Path.GetFileName(Server.MapPath(files.FileName));
                //var fileExt = Path.GetExtension(filename);
                //if (fileExt == ".xls")
                //{
                //    HSSFWorkbook hssfwb = new HSSFWorkbook(files.InputStream);
                //    sheet = hssfwb.GetSheetAt(0);
                //}
                //else
                //{
                //    XSSFWorkbook hssfwb = new XSSFWorkbook(files.InputStream);
                //    sheet = hssfwb.GetSheetAt(0);
                //}

                //List<DetalleTransferenciaViewModel> rows = new List<DetalleTransferenciaViewModel>();
                //for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                //{
                //    IRow row = sheet.GetRow(rowIndex);
                //    if (row != null) //null is when the row only contains empty cells   
                //    {
                //        i++;

                //        var detalle = new DetalleTransferenciaViewModel();
                //        string CodigoInterno = row.GetCell(0).GetString();


                //        var tipoUnidad = row.GetCell(2).GetString();



                //        if (!string.IsNullOrEmpty(CodigoInterno))
                //        {
                //            var producto = ProductosAppService.GetProductByInternalCode(CodigoInterno);
                //            detalle.CodigoInterno = CodigoInterno;

                //            if (producto == null)
                //            {
                //                detalle.Observacion = "Código de producto no existe";
                //                detalle.DescripcionProducto = "Código de producto no existe";
                //                detalle.ItemValido = false;
                //                detalle.IdLocal = Session.GetIdLocal();
                //                detalle.StockLocales = new List<LiteStockDto>();
                //                rows.Add(detalle);
                //                continue;
                //            }

                //            if (row.GetCell(1).CellType != CellType.Numeric)
                //            {
                //                detalle.Observacion = "Cantidad del producto debe ser numérico";
                //                detalle.DescripcionProducto = "Cantidad de producto debe ser numérico";
                //                detalle.ItemValido = false;
                //                detalle.IdLocal = Session.GetIdLocal();
                //                detalle.StockLocales = new List<LiteStockDto>();
                //                rows.Add(detalle);
                //                continue;
                //            }

                //            var Local = row.GetCell(3).GetString();
                //            string inicioSesion = User.Identity.Name;
                //            var locales = InventarioAppService.ObtenerLocales(inicioSesion).Where(x => x.Local == Local).FirstOrDefault();

                //            if (locales == null)
                //            {
                //                detalle.Observacion = "Bodega no existe";
                //                detalle.DescripcionProducto = "Bodega no existe";
                //                detalle.ItemValido = false;
                //                detalle.IdLocal = Session.GetIdLocal();
                //                detalle.StockLocales = new List<LiteStockDto>();
                //                rows.Add(detalle);
                //                continue;
                //            }

                //            var idLocal = locales.Id;
                //            var cantidad = row.GetCell(1).GetNumber();

                //            detalle.DescripcionProducto = producto.Producto;
                //            detalle.IdProducto = producto.Id;
                //            detalle.IdLocal = Convert.ToInt32(idLocal);
                //            detalle.Cantidad = Convert.ToInt32(cantidad);

                //            var unidades = TipoUnidadAppService.ObtenerTiposUnidadPorProducto(producto.Id).Where(x => x.IdEstado == 1).ToList();
                //            var unidad = unidades.Where(x => x.TipoUnidad == tipoUnidad).FirstOrDefault();

                //            if (unidad == null)
                //            {
                //                unidad = unidades[0];
                //                detalle.Observacion = "Tipo de unidad de producto no existe";
                //                detalle.ItemValido = false;
                //                detalle.IdLocal = Session.GetIdLocal();
                //                detalle.StockLocales = new List<LiteStockDto>();
                //                rows.Add(detalle);
                //                continue;
                //            }

                //            var stock = ProductosAppService.ObtenerStockProductoLocales(producto.Id).Where(x => x.IdLocal == Session.GetIdLocal()).FirstOrDefault();
                //            detalle.TipoUnidad = unidad.TipoUnidad;
                //            detalle.IdTipoUnidad = unidad.Id;
                //            detalle.StockLocales = ProductosAppService
                //                                    .ObtenerStockProductoLocales(producto.Id)?
                //                                    .Where(x => x.IdLocal != Session.GetIdLocal())?
                //                                    .ToList();
                //            detalle.Stock = Convert.ToDecimal(stock.StockInventario);
                //            detalle.Unidades = unidades;
                //            detalle.Unidad = producto.NombreUnidadMedida;

                //            if (IdLocalSelected == idLocal)
                //            {
                //                detalle.Observacion = "Local no puede ser igual al seleccionado";
                //                detalle.ItemValido = false;
                //            }

                //            rows.Add(detalle);
                //        }
                //    }
                //}

                ViewBag.IsFileValid = isFileValid;
                //var model = new TransferenciaViewModel
                //{
                //    Detalle = rows,
                //    IdLocalDestino = IdLocalSelected,
                //    IdVendedor = User.Identity.Name,
                //    EstadoActual = "EN PROCESO"
                //};
                return PartialView("UploadedExcelDataPedir", model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(i.ToString());
                return Json(new { success = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}