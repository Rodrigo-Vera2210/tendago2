using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
//using TendaGo.Web.CompraService;
using TendaGo.Web.Infraestructura;
using TendaGo.Web.Models;
using TendaGo.Common;
//using TendaGo.Web.ProductoService;
using System.IO;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using AutoMapper;
using RestSharp;
using System.Net;
using TendaGo.Web.ApplicationServices;
using System.Collections;
using System.Threading.Tasks;

namespace TendaGo.Web.Controllers
{
    [Authorize]
    public class CompraController : Controller
    {
        public ActionResult Index()
        {
            return View(new CompraViewModel());
        }

        private IEnumerable ObtenerRucs()
        {
            var rucs = RucsAppService.ObtenerRucs();

            return rucs.Select(m => new { m.Ruc, Descripcion = string.Format("{0:D13} - {1}", m.Ruc, m.Descripcion) });
        }

        [HttpPost]
        public JsonResult Guardar(CompraViewModel model)
        {

            try
            {
                //if (ModelState.IsValid)
                //{


                // validamos los productos que se van a guardar
                //Traemos datos del producto
                //var prod = new 

                if (model.DetalleCompra == null || model.DetalleCompra.Count == 0 || model.DetalleCompra.Any(x => x.IdProducto == 0))
                {
                    return Json(new RespuestaViewModel { Success = false, Mensaje = "Debe seleccionar algun articulo o producto para generar la compra." }, JsonRequestBehavior.AllowGet);
                }

                if (model.DetalleCompra.Any(x => x.Cantidad == 0 || (x.IdTipoUnidad == 0 && x.TipoProducto.Trim()== "PRODUCTO")))
                {
                    return Json(new RespuestaViewModel { Success = false, Mensaje = "Debe indicar la cantidad de articulos y el tipo de unidad para generar la compra." }, JsonRequestBehavior.AllowGet);
                }

                model.LocalSeleccionado = Session.GetIdLocal();

                if (model.LocalSeleccionado == null)
                {
                    return Json(new RespuestaViewModel { Success = false, Mensaje = "Debe seleccionar el local de la Compra." }, JsonRequestBehavior.AllowGet);
                }

                if (model.IdProveedor < 1)
                {
                    return Json(new RespuestaViewModel { Success = false, Mensaje = "Debe seleccionar un proveedor para la compra." }, JsonRequestBehavior.AllowGet);
                }

                if (model.CuentasPorPagar == null || model.CuentasPorPagar.Count == 0)
                {
                    return Json(new RespuestaViewModel { Success = false, Mensaje = "Debe configurar la forma de pago para generar la compra." }, JsonRequestBehavior.AllowGet);
                }

                int idLocalSelected = model.LocalSeleccionado.Value;
                var compraDto = Mapper.Map<InputDto>(model);
                compraDto.Saldo = compraDto.Total;
                //compraDto.IdLocal = User.Identity.GetIdLocal();
                compraDto.IdLocal = idLocalSelected;
                compraDto.UsuarioIngreso = User.Identity.Name;
                compraDto.IpIngreso = AppServiceBase.ObtenerIp();
                compraDto.EstadoActual = "APROBADA";

                foreach (var item in compraDto.DetalleEntradaFromIdEntrada)
                {
                    if (item.FechaExpiracion == DateTime.MinValue || item.FechaExpiracion == DateTime.MaxValue)
                    {
                        item.FechaExpiracion = null;
                    }
                }

                var result = CompraAppService.SaveInput(compraDto);

                if (!string.IsNullOrEmpty(result?.Id))
                {
                    return Json(new { Success = true, Mensaje = "La compra [" + result.Id + "] ha sigo guardada correctamente." }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { Success = false, Mensaje = "Hay errores al guardar la compra." }, JsonRequestBehavior.AllowGet);
            }
            catch (ApplicationServicesException ex)
            {
                return Json(new { Success = false, Mensaje = ex.ApiErrorResponse?.UserMessage ?? ex.Message, Error=ex.ApiErrorResponse}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Mensaje = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            //}

            //return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConsultarProveedores()
        {
            ViewBag.FiltrosBusqueda = ObtenerFiltrosBusquedaProveedor();
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConsultarProveedores(string filtroBusqueda, string textoBusqueda)
        {
            bool identification = (filtroBusqueda == "1");
            
            var proveedores = ProveedorAppService.ConsultarProveedores(textoBusqueda, identification);
            var proveedoresModel = Mapper.Map<List<ProveedorViewModel>>(proveedores);
            return PartialView("ListaProveedores", proveedoresModel);
        }

        [HttpPost]
        public ActionResult GenerarCuentasPorPagar(string formaPago, int numeroPagos, int plazoDias, decimal valorCompra, decimal valorPagado)
        {
            var detallePagos = new List<CuentasPorPagarViewModel>();

            DateTime fechaInicial = DateTime.Today;

            decimal valorPendiente = valorCompra;
            decimal valorCuota0 = valorPagado;

            if (formaPago == "2" && valorPagado >= valorCompra)
            {
                formaPago = "1";
            }

            CuentasPorPagarViewModel abono;

            if (formaPago == "1")
            {
                if (valorPagado == 0)
                {
                    valorPagado = valorCompra;
                }

                if (valorPagado < valorCompra)
                {
                    formaPago = "2";
                    numeroPagos = 1;
                }
                else
                {
                    valorCuota0 = valorCompra;
                    numeroPagos = 0;
                }
            }
            else
            {
                
                if (numeroPagos < 1)
                {
                    numeroPagos = 1;
                }
            }

            if (valorPagado > 0)
            {
                valorPendiente = valorCompra - valorPagado;

                if (valorPendiente < 0)
                {
                    valorPendiente = 0;
                }

                abono = new CuentasPorPagarViewModel
                {
                    Id = 0,
                    Numero = 0,
                    Valor = valorCuota0,
                    ValorPendiente = valorCuota0,
                    ValorPagado = valorPagado,
                    FechaVencimiento = fechaInicial
                };

                detallePagos.Add(abono);
            }

            decimal valorCuotaEstimada = (numeroPagos > 0) ? Math.Round(valorPendiente / numeroPagos, 2) : valorPendiente;

            if (valorPendiente > 0)
            {
                for (int i = 1; i <= numeroPagos; i++)
                {
                    fechaInicial = fechaInicial.AddDays(plazoDias);

                    decimal valorCuota = valorCuotaEstimada;

                    if (valorPendiente < valorCuotaEstimada)
                    {
                        valorCuota = valorPendiente;
                    }

                    valorPendiente -= valorCuota;

                    detallePagos.Add(new CuentasPorPagarViewModel { Id = i, Numero = i, Valor = valorCuota, ValorPendiente = valorCuota, ValorPagado = 0, FechaVencimiento = fechaInicial });
                }


                var lastDetalle = detallePagos.LastOrDefault();

                if (lastDetalle != null)
                {
                    lastDetalle.Valor = ((valorCompra - valorPagado) - (valorCuotaEstimada * (numeroPagos - 1)));
                    lastDetalle.ValorPendiente = lastDetalle.Valor;
                }
            }

            var model = new CompraViewModel { CuentasPorPagar = detallePagos };
            ViewBag.EditarPagos = (formaPago != "1");
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult ObtenerTiposUnidadPorProducto(int idProducto)
        {
            var tiposUnidadDto = TipoUnidadAppService.ObtenerTiposUnidadPorProducto(idProducto);
            var tiposUnidad = tiposUnidadDto.Select(x => new { Id = x.Id, Nombre = x.TipoUnidad }).ToList();
            return Json(tiposUnidad, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Importar()
        {
            ViewBag.FormasPago = ObtenerFormasPago();
            ViewBag.RucList = new SelectList(ObtenerRucs(), "Ruc", "Descripcion");
            ViewBag.MonedasList = new SelectList(GetCurrencies(), "Id", "CodigoISO");
            return View(new CompraModel());
        }

        [HttpPost]
        public ActionResult Importar(CompraModel model)
        {
            try
            {
                // Validamos la compra antes de guardar:
                if (model.CuentasPorPagar==null || model.CuentasPorPagar.Count==0)
                {
                    throw new Exception("Debe configurar los pagos para esta compra!");
                }

                if (model.IdProveedor == 0)
                {
                    throw new Exception("Debe seleccionar el proveedor!");
                }

                if (model.ItemsArchivo == null || model.ItemsArchivo.Count == 0)
                {
                    throw new Exception("Debe cargar datos para realizar la importación!");
                }
                if (model.IdMonedaOrigen == null || model.IdMonedaOrigen == 0)
                {
                    throw new Exception("Debe seleccionar la moneda de la compra!");
                }

                int idLocalSelected = Session.GetIdLocal() ?? 0;
                
                var compraDto = new InputDto();
                //compraDto.IdLocal = User.Identity.GetIdLocal();
                compraDto.IdLocal = idLocalSelected; 
                compraDto.IdProveedor = model.IdProveedor;
                compraDto.IdFormaPago = model.IdFormaPago;
                compraDto.Saldo = model.Total;
                compraDto.Tasa = model.Tasa;
                compraDto.Fecha = DateTime.Now;
                compraDto.NumeroFacturaPedido = model.NumeroFacturaPedido;
                compraDto.IdMonedaOrigen = model.IdMonedaOrigen;
                compraDto.Ruc = model.Ruc;
                compraDto.ValorAdicional = model.ValorAdicional;
                compraDto.FechaIngreso = DateTime.Now;
                compraDto.UsuarioIngreso = User.Identity.Name;
                compraDto.IpIngreso = AppServiceBase.ObtenerIp();
                
                compraDto.DetalleEntradaFromIdEntrada = model.ItemsArchivo.Select(det =>
                {
                    //var producto = GetProductByProviderCode(det.CodigoProducto);
                    //var tipoUnidad = GetUnitTypeByName(producto.Id, det.TipoUnidad);
                    //var local = GetLocalWarehouse(det.IdLocal);
                    return new InputDetailDto
                    {
                        IdLocal = (det.IdLocal > 0 ? det.IdLocal : compraDto.IdLocal),
                        IdProducto = det.IdProducto,
                        IdTipoUnidad = det.IdTipoUnidad,
                        IdProveedor = compraDto.IdProveedor,
                        Periodo = compraDto.Periodo,
                        Fecha = compraDto.Fecha,
                        Cantidad = det.Cantidad,
                        CostoVenta = det.CostoVenta,
                        ValorAdicional = det.ValorAdicional,
                        CostoUnitarioImportacion = det.CostoUnitarioImportacion,
                        ValorFOB = det.ValorFob,
                        FactorDistribucion = det.FactorDistribucion,
                        FechaExpiracion = null//DateTime.Parse(det.FechaCaducidad)
                    };
                }).ToList();

                compraDto.CuentaPorPagarFromIdEntrada = model.CuentasPorPagar.Select(cta => Mapper.Map<DebtDto>(cta)).ToList();
                
                var result = CompraAppService.SaveInput(compraDto);

                if (!string.IsNullOrEmpty(result?.Id))
                {
                    return Json(new RespuestaViewModel { Success = true, Mensaje = "La compra [" + result.Id + "] ha sigo guardada correctamente." }, JsonRequestBehavior.AllowGet);
                }
            
                return Json(new RespuestaViewModel { Success = false, Mensaje = "Hay errores al guardar la compra." }, JsonRequestBehavior.AllowGet);
            }
            catch (ApplicationServicesException ex)
            {
                return Json(new RespuestaViewModel { Success = false, Mensaje = ex.ApiErrorResponse?.TechnicalMessage ?? ex.ApiErrorResponse?.UserMessage ?? ex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new RespuestaViewModel { Success = false, Mensaje = "Hay errores al guardar la compra. "+ex.Message }, JsonRequestBehavior.AllowGet);
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

                decimal totalCompra = 0;
                List<DetalleArchivoCompra> rows = new List<DetalleArchivoCompra>();
                for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                {
                    IRow row = sheet.GetRow(rowIndex);
                    if (row != null) //null is when the row only contains empty cells   
                    {
                        i++;

                        var detalle = new DetalleArchivoCompra();
                        string codProducto = row.GetCell(0).GetString();
                         
                        var idLocal = row.GetCell(1).GetString();
                        //int idLocalBodega = Convert.ToInt32(idLocal);
                        var nombreTipoUnidad = row.GetCell(2).GetString();
                        var valorFob = row.GetCell(3).GetNumber();
                        var costoImportacion = row.GetCell(4).GetNumber();
                        var factorImportacion = row.GetCell(5).GetNumber();
                        var cantidad = row.GetCell(6).GetNumber();
                        var costoVenta = row.GetCell(7).GetNumber();
                        var fechaCaducidad = row.GetCell(8).GetDate()?.ToString("dd/MM/yyyy");

                        if (!string.IsNullOrEmpty(codProducto) || cantidad > 0)
                        {
                            detalle.CodigoProducto = codProducto;
                            detalle.TipoUnidad = nombreTipoUnidad;
                            detalle.Local = idLocal;
                            var result = ValidarItemArchivoCompra(codProducto, nombreTipoUnidad, idLocal, cantidad);
                            string obs = result.IsValid ? string.Empty : $"LINEA {rowIndex}: {result.Observations}";
                            isFileValid = result.IsValid;
                            detalle.ItemValido = result.IsValid;
                            detalle.IdProducto = result.IdProduct;
                            detalle.Producto = result.ProductName;
                            detalle.IdLocal = result.IdLocal;
                            detalle.IdTipoUnidad = result.IdUnitType;
                            detalle.Observacion = obs;

                            detalle.ValorFob = Convert.ToDecimal(valorFob);
                            detalle.CostoUnitarioImportacion = Convert.ToDecimal(costoImportacion);
                            detalle.FactorDistribucion = Convert.ToDecimal(factorImportacion);
                            detalle.Cantidad = Convert.ToDecimal(cantidad);
                            detalle.CostoVenta = Convert.ToDecimal(costoVenta);
                            detalle.FechaCaducidad = fechaCaducidad;
                            totalCompra += detalle.Subtotal;
                            rows.Add(detalle);
                        }
                    }
                }
                
                ViewBag.IsFileValid = isFileValid;
                ViewBag.Total = totalCompra;
                var model = new CompraModel { ItemsArchivo = rows};
                return PartialView("UploadedExcelData", model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(i.ToString());
                return Json(new { success = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult AnularCompra(string id)
        {
            try
            {
                var model = new InputDeleteDto
                {
                    IdEntrada = id,
                    FechaIngreso = DateTime.Now,
                    UsuarioIngreso = User.Identity.Name,
                    IpIngreso = AppServiceBase.ObtenerIp()
                };

                var result = CompraAppService.DeleteInput(model);

                if (result)
                {
                    return Json(new { success = true, mensaje = "La Compra [" + id + "] ha sido anulada correctamente." }, JsonRequestBehavior.AllowGet);
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
        public ActionResult Resumen(string id)
        {
            var entrada = CompraAppService.GetInputById(id);
            var model = entrada?.ToModel();
            if (model == null)
            {
                ViewBag.ErrorMessage = $"No se encontró la Compra # {id}";
                return View("Error");
            }

            ViewBag.ActionUrl = Url.Action(Request.Form["Action"] ?? "");

            return View("MantNotaPedido_Resumen", model);
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

            var compras = GetInputs(model);

            Session[$"EntradaSearchModel_{ViewBag.Accion}"] = model;

            return PartialView(compras);
        }

        private List<EntradaViewModel> GetInputs(OrderSearchModel model)
        {
            model.tipoTransaccion = TransactionType.Compra;            
            //var idLocal = Session.GetIdLocal() ?? 0;

            //if (model.idLocal != null && model.idLocal > 0)
              var  idLocal = model.idLocal;

            var notasPedidoDto = InventarioAppService.SearchInputs(idLocal, model) ?? new List<InputDto>();

            return Mapper.Map<List<EntradaViewModel>>(notasPedidoDto)?.OrderByDescending(m => m.Id).ToList();
        }


        private LiteProductDto GetProductByProviderCode(string internalCode)
        {
            return ProductosAppService.GetProductByProviderCode(Url.Encode(internalCode));
        }

        private UnitTypeDto GetUnitTypeByName(int idProduct, string unitTypeName)
        {
            return TipoUnidadAppService.ObtenerTipoUnidad(idProduct, unitTypeName);
        }

        private WarehouseDto GetLocalWarehouse(int local)
        {
            return Session.GetLocal(local);
        }

        public ActionResult ImportarGastos()
        {
            ViewBag.FormasPago = ObtenerFormasPago();
            ViewBag.RucList = new SelectList(ObtenerRucs(), "Ruc", "Descripcion");
            ViewBag.MonedasList = new SelectList(GetCurrencies(), "Id", "CodigoISO");
            return View(new CompraModel());
        }
        public ActionResult ReadTextFile()
        {
            int i = 1;
            try
            {
                HttpPostedFileBase files = Request.Files[0];
                StringBuilder strbuild = new StringBuilder();
                string filename = Path.GetFileName(Server.MapPath(files.FileName));
                               

                int counter = 0;
                string line;
                StreamReader file = new StreamReader(files.InputStream);
                while ((line = file.ReadLine()) != null)
                {
                    System.Console.WriteLine(line);
                    counter++;
                }




                var model = new CompraModel();
                    return PartialView("UploadedTextData", model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(i.ToString());
                return Json(new { success = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ReporteCompras()
        {
            var model = new FacturaViewModels();
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Descargar(string id, string format = "PDF")
        {
            var response = await CompraAppService.DownloadInputAsync(id, format);
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                var contentType = response.Content.Headers.ContentType?.MediaType;
                return File(result, contentType);
            }

            return Content(await response.Content.ReadAsStringAsync(), response.Content.Headers.ContentType?.MediaType);
        }

        #region Metodos Privados

        //private List<SelectListItem> ObtenerFiltrosBusquedaProducto()
        //{
        //    ProductoService.ProductoServiceClient servicio = new ProductoService.ProductoServiceClient();
        //    var result = servicio.ConsultarFiltrosBusqueda();

        //    if (result != null)
        //    {
        //        return result.Select(x => new SelectListItem { Value = x.Id, Text = x.Descripcion }).ToList();
        //    }

        //    return new List<SelectListItem>();
        //}

        private List<SelectListItem> ObtenerFiltrosBusquedaProveedor()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{ Text="Identificacion", Value="1"  },
                new SelectListItem{ Text="RazonSocial", Value="2"  }
            };
        }

        private string ObtenerJsonLocalesBodegasEmpresa()
        { 
            var localesBodegasDto = Session.GetLocales();
            var localesBodegas = localesBodegasDto.Select(x => new { Id = x.Id, Nombre = x.Local }).ToList();
            return JsonConvert.SerializeObject(localesBodegas);
        }

        private List<SelectListItem> ObtenerFormasPago()
        {
            var listaFormasPago = new List<SelectListItem>(){
                new SelectListItem { Text = "CONTADO", Value = "1"},
                new SelectListItem { Text = "CREDITO", Value = "2"},
            };

            return listaFormasPago;
        }
        
        private List<CurrencyDto> GetCurrencies()
        { 
            return CatalogosAppService.ObtenerMonedas();
        }

        private ValidationInputResultModel ValidarItemArchivoCompra(string codProducto, string tipoUnidad, string idLocal, double cantidad)
        {
            var result = CompraAppService.ValidateProduct(codProducto, tipoUnidad, idLocal);
            if (result.IsValid)
            {
                if (cantidad <= 0)
                {
                    result.Observations = "La cantidad del producto debe ser mayor que cero.";
                    result.IsValid = false;
                }
            }

            return result;
        }

        #endregion

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> ConsultarEntradasReporte(FacturaViewModels model, string format = "EXCEL")
        {
            var response = await InventarioAppService.DownloadEntradasAsync(model, format);

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