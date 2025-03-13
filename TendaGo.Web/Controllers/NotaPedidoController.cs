using AutoMapper;
using Newtonsoft.Json;
using TendaGo.Web.Infraestructura;
using TendaGo.Web.Models;
//using TendaGo.Web.NotaPedidoService;
//using TendaGo.Web.ProductoService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TendaGo.Common;
using TendaGo.Web.ApplicationServices;
using RestSharp;
using System.Net;
using System.Text;
using Microsoft.AspNet.Identity;
using TendaGo.Common.EntitiesDto;
using TendaGo.Common.RequestModels;

namespace TendaGo.Web.Controllers
{
    [Authorize]
    public class NotaPedidoController : Controller
    {
        const string NOTA_PEDIDO_TYPE = "NP";
        private readonly IReportingService reportingService;

        public NotaPedidoController()
        {
            this.reportingService = new SSRSService();
        }

        [HttpGet]   
        public ActionResult Index()
        {
            return Consultar();
            /*
            Session["ShoppingCar"] = new List<DetalleNotaPedidoViewModel>();

            return View();
            */
        }


        [HttpGet]
        public ActionResult DetalleProducto()
        {
            return PartialView();
        }

        public int IdLocalSelected => Session.GetIdLocal() ?? 0;

        [HttpPost]
        public ActionResult MostrarProducto(int idProducto)
        {
            var model = new DetalleNotaPedidoViewModel();
            try
            {
                ProductDto productoDto = null;
                int idLocalSelected = IdLocalSelected;
                productoDto = ProductosAppService.ObtenerStockProducto(idProducto, idLocalSelected);
                if (productoDto != null)
                {
                    var producto = Mapper.Map<ProductoViewModel>(productoDto);
                    var tiposDeUnidad = ObtenerListaTiposUnidad(idProducto);
                    if (tiposDeUnidad != null)
                    {
                        var tipoUnidadesNoEditables = tiposDeUnidad.Where(tu => tu.TipoUnidad.ToUpper().Equals("UNIDAD")).ToList();
                        var selectList = new SelectList(tiposDeUnidad, "Id", "TipoUnidad");
                        ViewBag.ListaTipoUnidades = selectList;
                        ViewBag.TiposUnidad = JsonConvert.SerializeObject(tiposDeUnidad);
                        ViewBag.JsonTipoUnidadesNoEditables = JsonConvert.SerializeObject(tipoUnidadesNoEditables);
                    }
                    model = new DetalleNotaPedidoViewModel
                    {
                        IdProducto = producto.Id,
                        NombreProducto = producto.Producto,
                        DescripcionProducto = producto.Descripcion,
                        CantidadTotalDisponible = producto.Stock,
                        Cantidad = 1,
                        FotoDataUrl = producto.FotoDataUrl
                    };
                }
                else
                {                    
                    productoDto = ProductosAppService.ObtenerProducto(idProducto);
                    var producto = Mapper.Map<ProductoViewModel>(productoDto);
                    model = new DetalleNotaPedidoViewModel
                    {
                        IdProducto = producto.Id,
                        NombreProducto = producto.Producto,
                        //DescripcionProducto = "<strong style='color:red;'> Producto no cuenta con Stock Disponible para la Localidad </strong></br></br>" + producto.Descripcion,
                        DescripcionProducto = producto.Descripcion,
                        CantidadTotalDisponible = 0,
                        Cantidad = 0,
                        FotoDataUrl = producto.FotoDataUrl
                    };
                }
                if (ViewBag.ListaTipoUnidades == null)
                    model.IdTipoUnidad = -1;

                model.FotoDataUrl = SetProductImage(productoDto.PathArchivo);
                return PartialView(model);
            }
            catch (ApplicationServicesException apex)
            {
                return Json(new { Success = false, Mensaje = apex.Message, Error = apex.ApiErrorResponse }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new RespuestaViewModel { Success = false, Mensaje = "Hay errores al mostrar el producto. " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetNotasPedidoTemp()
        {
            if (Session["NotasPedido"] == null)
                Session["NotasPedido"] = 1;

            int notasPedidoTemp = (int)Session["NotasPedido"];
            var listaNotasPedidoTemp = new List<NotaPedidoTempModel>();
            for (int i = 0; i < notasPedidoTemp; i++)
            {
                listaNotasPedidoTemp.Add(new NotaPedidoTempModel { Id = i + 1 });
            }
            if (Session["NotasPedidoTemp"] == null)
                Session.Add("NotasPedidoTemp", listaNotasPedidoTemp);
            else
                Session["NotasPedidoTemp"] = listaNotasPedidoTemp;

            return Json(new { notasPedidoMultiples = (notasPedidoTemp > 1), notas = listaNotasPedidoTemp }, JsonRequestBehavior.AllowGet);
        }

        private string SetProductImage(string pathArchivo)
        {
            if (!string.IsNullOrEmpty(pathArchivo))
            {
                string realPath = Server.MapPath($"~{AppServiceBase.Empresa.RaizArchivo}{pathArchivo}");
                string pathImagen = System.IO.File.Exists(realPath)
                    ? realPath
                    : Server.MapPath("~/Images/no_imagen.png");
                return Tools.ConvertirByteArrayAImagen(System.IO.File.ReadAllBytes(pathImagen));
            }
            return Tools.ConvertirByteArrayAImagen(System.IO.File.ReadAllBytes(Server.MapPath("~/Images/no_imagen.png")));
        }

        [HttpPost]
        public ActionResult ExistenciaProducto(int idProducto)
        {
            

            //ProductoService.ProductoServiceClient servicio = new ProductoService.ProductoServiceClient();
            //var productoDto = servicio.ConsultarProductoPorId(idProducto);
            var productoDto = ProductosAppService.ObtenerProducto(idProducto);
            var producto = Mapper.Map<ProductoViewModel>(productoDto);

            var stockList = ProductosAppService.ObtenerStockProductoLocales(idProducto);

            var model = stockList.Select(m => Mapper.Map<ExistenciaProductoViewModel>(m)).ToList();

            //var model = new List<ExistenciaProductoViewModel>()
            //{
            //    new ExistenciaProductoViewModel { IdProducto = producto.Id, NombreProducto = producto.Producto, IdBodega = 1, DescripcionBodega = "Bodega 1", StockDisponible = 50},
            //    new ExistenciaProductoViewModel { IdProducto = producto.Id, NombreProducto = producto.Producto, IdBodega = 2, DescripcionBodega = "Bodega 2", StockDisponible = 404},
            //    new ExistenciaProductoViewModel { IdProducto = producto.Id, NombreProducto = producto.Producto, IdBodega = 3, DescripcionBodega = "Bodega 3", StockDisponible = 450},
            //    new ExistenciaProductoViewModel { IdProducto = producto.Id, NombreProducto = producto.Producto, IdBodega = 4, DescripcionBodega = "Bodega 4", StockDisponible = 560},
            //};

            ViewBag.Producto = producto.Producto;
            ViewBag.DescripcionProducto = producto.Descripcion;
            return PartialView(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AgregarDetalle(DetalleNotaPedidoViewModel detalleNotaPedido)
        {
            if (ModelState.IsValid)
            {
                if (Session["ShoppingCar"] == null)
                {
                    Session["ShoppingCar"] = new List<DetalleNotaPedidoViewModel>();
                }
                //var listaNotasPedidoTemp = Session["NotasPedidoTemp"] as List<NotaPedidoTempModel>;

                var shoppingCar = (List<DetalleNotaPedidoViewModel>)Session["ShoppingCar"];
                foreach (var notaTemp in detalleNotaPedido.NotasTemp)
                {
                    var det = new DetalleNotaPedidoViewModel();

                    det.IdNotaPedidoTemp = notaTemp.IdNotaPedidoTemp;
                    det.Cantidad = notaTemp.Cantidad;
                    det.IdProducto = detalleNotaPedido.IdProducto;
                    det.IdTipoUnidad = detalleNotaPedido.IdTipoUnidad;
                    det.CantidadMinima = detalleNotaPedido.CantidadMinima;
                    det.CantidadTotalDisponible = detalleNotaPedido.CantidadTotalDisponible;
                    det.DesTipoUnidad = detalleNotaPedido.DesTipoUnidad;
                    det.DescripcionProducto = detalleNotaPedido.DescripcionProducto;
                    det.Precio = detalleNotaPedido.Precio;
                    det.PrecioSugerido = detalleNotaPedido.PrecioSugerido;
                    det.NombreProducto = detalleNotaPedido.NombreProducto;
                    var detalle = shoppingCar.FirstOrDefault(x => x.IdProducto == det.IdProducto && x.IdTipoUnidad == det.IdTipoUnidad && x.IdNotaPedidoTemp == det.IdNotaPedidoTemp);
                    if (detalle == null)
                    {
                        shoppingCar.Add(det);
                    }
                    else
                    {
                        shoppingCar.Remove(detalle);
                        detalle.Cantidad = detalle.Cantidad + det.Cantidad;
                        shoppingCar.Add(detalle);
                    }
                }

                Session["ShoppingCar"] = shoppingCar;

                return Json(new RespuestaViewModel { Success = true, Mensaje = "Producto agregado correctamente a la Nota de pedido." });
            }
            return Json(new RespuestaViewModel { Success = false, Mensaje = "Datos ingresados no son válidos." });
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            if (Session["ShoppingCar"] == null)
            {
                Session["ShoppingCar"] = new List<DetalleNotaPedidoViewModel>();
            }
            var shoppingCar = (List<DetalleNotaPedidoViewModel>)Session["ShoppingCar"];
            var valorTotal = shoppingCar.Sum(x => x.Subtotal);

            var notaPedido = new NotaPedidoViewModel();
            notaPedido.DetalleNotaPedido = shoppingCar;
            notaPedido.Total = valorTotal;

            ViewBag.ListaFormaPago = ObtenerFormasPago();
            return PartialView(notaPedido);
        }

        [HttpPost]
        public JsonResult Checkout(NotaPedidoViewModel model)
        {         
            if (IdLocalSelected == 0)
                return Json(new { success = false, mensaje="Seleccione nuevamente el Local para transaccionar" }, JsonRequestBehavior.AllowGet);

            
            if (ModelState.IsValid)
            {
                var detalleNotaPedido = (List<DetalleNotaPedidoViewModel>)Session["ShoppingCar"];
                if (detalleNotaPedido != null && detalleNotaPedido.Any())
                {
                    
                    var fechaEntrega = DateTime.Today.Date;
                    var horaEntrega = new TimeSpan();
                    DateTime.TryParseExact(model.FechaEntrega, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fechaEntrega);
                    TimeSpan.TryParseExact(model.HoraEntrega, "g", System.Globalization.CultureInfo.InvariantCulture, out horaEntrega);

                    StringBuilder sb = new StringBuilder();
                    var notasPedidoTemp = (List<NotaPedidoTempModel>)Session["NotasPedidoTemp"];
                    foreach (var notaPedido in notasPedidoTemp)
                    {
                        var detallesNotaPedido = detalleNotaPedido.Where(x => x.IdNotaPedidoTemp == notaPedido.Id).ToList();

                        //var totalNotaPedido = detallesNotaPedido.Sum(x => x.Subtotal);
                        model.DetalleNotaPedido = detallesNotaPedido;
                        var notaPedidoDto = Mapper.Map<OutputDto>(model);
                        //var notaPedidoDto = Mapper.Map<DatosNotaPedidoDto>(model);

                        var detalleNotaPedidoDto = Mapper.Map<List<OutputDetailDto>>(model.DetalleNotaPedido);
                        //var detalleNotaPedidoDto = Mapper.Map<List<DetalleNotaPedidoDto>>(model.DetalleNotaPedido);
                        //notaPedidoDto.Total = totalNotaPedido;
                        //notaPedidoDto.Saldo = totalNotaPedido;
                        notaPedidoDto.FechaHoraEntregaPropuesta = fechaEntrega.Add(horaEntrega);
                        notaPedidoDto.IdLocal = IdLocalSelected;
                        notaPedidoDto.TipoTransaccion = "NP"; // Nota de Pedido
                        notaPedidoDto.IdVendedor = User.Identity.Name;
                        notaPedidoDto.UsuarioIngreso = User.Identity.Name;
                        notaPedidoDto.IpIngreso = AppServiceBase.ObtenerIp();
                        notaPedidoDto.DetalleNotaPedido = detalleNotaPedidoDto;
                        //var idNotaPedido = servicio.GuardarNotaPedido(notaPedidoDto);
                        var notaResult = NotaPedidoAppService.SaveOutput(notaPedidoDto);
                        //sb.AppendLine("La nota de pedido [" + idNotaPedido + "] ha sigo guardada correctamente.");
                        sb.AppendLine("La nota de pedido [" + notaResult.Id + "] ha sigo guardada correctamente.");
                    }
                    Session["ShoppingCar"] = new List<DetalleNotaPedidoViewModel>();
                    Session["NotasPedidoTemp"] = new List<NotaPedidoTempModel>();
                    return Json(new { success = true, mensaje = sb.ToString() }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, mensaje = "La Nota de Pedido no contiene items. Agregue items al carrito y reintente." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }


        public OutputDto GuardarTransferencia(OutputDto notaPedidoDto)
        {
            var result = new OutputDto();
            var client = new RestClient(Tools.GetApiUrlBase());
            var request = new RestRequest("/transfer/create", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("app_user", User.Identity.Name);
            request.AddHeader("app_token", User.Identity.GetTokenUsuario());
            request.AddJsonBody(notaPedidoDto);
            var restResponse = client.Execute<OutputDto>(request);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                result = restResponse.Data;
                return result;
            }

            var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(restResponse.Content);

            if (string.IsNullOrEmpty(errorResponse.TechnicalMessage))
            {
                var genericApiResponse = JsonConvert.DeserializeObject<GeneriApiErrorResponse>(restResponse.Content);
                throw new ApplicationServicesException(genericApiResponse.Message);
            }
            
            throw new ApplicationServicesException(errorResponse);
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


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult EliminarDetalleNotaPedido(int idProducto)
        {
            if (Session["ShoppingCar"] == null)
            {
                Session["ShoppingCar"] = new List<DetalleNotaPedidoViewModel>();
            }

            var shoppingCar = (List<DetalleNotaPedidoViewModel>)Session["ShoppingCar"];
            var detalle = shoppingCar.Where(x => x.IdProducto == idProducto).FirstOrDefault();
            if (detalle != null)
            {
                shoppingCar.Remove(detalle);
                var valorTotal = shoppingCar.Sum(x => x.Subtotal);
                Session["ShoppingCar"] = shoppingCar;
                return Json(new { success = true, valorTotal = valorTotal.ToCustomFormatString() });
            }
            else
            {
                return Json(new { success = false, mensaje = "Detalle no existe" });
            }
        }

        [HttpGet]
        public JsonResult CancelarNotaPedido()
        {
            if (Session["ShoppingCar"] == null)
                return Json(new { success = false, mensaje = "El carrito de compras se encuentra vacio." }, JsonRequestBehavior.AllowGet);
            var shoppingCar = (List<DetalleNotaPedidoViewModel>)Session["ShoppingCar"];
            if (shoppingCar == null)
                return Json(new { success = false, mensaje = "El carrito de compras se encuentra vacio." }, JsonRequestBehavior.AllowGet);
            Session.Remove("ShoppingCar");
            return Json(new { success = true, mensaje = "El carrito de compras fue vaciado correctamente." }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CarritoCompras()
        {
            if (Session["ShoppingCar"] == null)
            {
                Session["ShoppingCar"] = new List<DetalleNotaPedidoViewModel>();
            }

            if (Session["NotasPedido"] == null)
                Session["NotasPedido"] = 1;

            var shoppingCar = (List<DetalleNotaPedidoViewModel>)Session["ShoppingCar"];
            var cantNotasPedido = (int)Session["NotasPedido"];
            ViewBag.CantidadNotasPedido = cantNotasPedido;
            return PartialView(shoppingCar);
        }

        [HttpGet]
        public ActionResult Aprobar()
        {
            var notasPedidoDto = NotaPedidoAppService
                .GetOutputsByLocal(IdLocalSelected, TransactionStatus.EnProceso, TransactionType.NotaPedido)
                .ToList();
            
            var notasPedidoViewModel = Mapper.Map<List<NotaPedidoViewModel>>(notasPedidoDto);
            ViewBag.Accion = "Aprobar";
            return View("ConsultarNotasPedido", notasPedidoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Aprobar(AprobacionNotaPedidoViewModel model)
        {
            var mensaje = "Se ha producido un error.";
            try
            {
                if (model.CuentasPorCobrar == null || model.CuentasPorCobrar.Length == 0)
                {
                    return Json(new { success = false, mensaje = "Debe especificar el detalle de los pagos de la Cuenta Por Cobrar" }, JsonRequestBehavior.AllowGet);
                }

                if (model.IdFormaPago == 0)
                {
                    return Json(new { success = false, mensaje = "Debe especificar la forma de pago" }, JsonRequestBehavior.AllowGet);
                }

              

                //if (!ValidarStockProductos(model, out string msg))
                //{
                //    return Json(new { success = false, mensaje = msg }, JsonRequestBehavior.AllowGet);
                //}

                //Valido que los medios de pago coincidan con el valor a pagar
                decimal valorPagar = 0;
                decimal ValorMetodo = 0;
                foreach (var meth in model.CuentasPorCobrar)
                {
                    if (meth.MetodosPago != null && meth.MetodosPago.Count > 0)
                    {
                        valorPagar += meth.ValorPagado;
                        foreach (var m in meth.MetodosPago)
                        {
                            ValorMetodo += m.Valor;
                        }
                    }
                }

                //Si es contado debe ser igual a metodos de pago
                if ((model.IdFormaPago==1) && (model.Total != ValorMetodo))
                {
                    return Json(new { success = false, mensaje = "Valor a pagar debe ser igual a valor de  Nota de Pedido" }, JsonRequestBehavior.AllowGet);
                }
                //Si es credito debe ser mayor a metodos de pago
                if ((model.IdFormaPago == 2) && (model.Total <= ValorMetodo))
                {
                    return Json(new { success = false, mensaje = "Valor a pagar debe ser menor a valor de  Nota de Pedido" }, JsonRequestBehavior.AllowGet);
                }

                var datosAprobacionNotaPedidoDto = Mapper.Map<SalesOrderApprovalDto>(model);
                datosAprobacionNotaPedidoDto.Usuario = User.Identity.Name;
                datosAprobacionNotaPedidoDto.Ip = AppServiceBase.ObtenerIp();

                var respuesta = NotaPedidoAppService.AprobarNotaPedido(datosAprobacionNotaPedidoDto);
                 
                if (respuesta != null)
                {
                    return Json(new { success = true, mensaje = "La nota de pedido [" + model.Id + "] ha sido aprobada correctamente.", url = Url.Action("Resumen", new { id = respuesta.Id }) }, JsonRequestBehavior.AllowGet);
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

        private bool ValidarStockProductos(AprobacionNotaPedidoViewModel model, out string msg)
        {
            if (model?.DetalleNotaPedido != null)
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

        public ProductDto ObtenerProducto(int idProducto, int? idLocal = null )
        {
            return ProductosAppService.ObtenerStockProducto(idProducto, idLocal ?? IdLocalSelected) 
                ?? ProductosAppService.ObtenerProducto(idProducto);
        }

        [HttpGet]
        public ActionResult Empaquetar()
        {
            var notasPedidoDto = NotaPedidoAppService
                .GetOutputsByLocal(IdLocalSelected, TransactionStatus.Aprobada, TransactionType.NotaPedido)
                .ToList();

            var notasPedidoViewModel = Mapper.Map<List<NotaPedidoViewModel>>(notasPedidoDto);
            ViewBag.Accion = "Empaquetar";
            return View("ConsultarNotasPedido", notasPedidoViewModel);
        }

        [HttpPost]
        public JsonResult Empaquetar(EmpaquetadoNotaPedidoViewModel empaquetadoNotaPedido)
        {
            try
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
            catch (ApplicationServicesException ex)
            {
                return Json(new { success = false, mensaje = ex.ApiErrorResponse.UserMessage, error = ex.ApiErrorResponse }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mensaje = $"{ex.Message} {ex.InnerException?.Message}", error = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Revisar()
        {
            var notasPedidoDto = NotaPedidoAppService
                .GetOutputsByLocal(IdLocalSelected, TransactionStatus.Empaquetada, TransactionType.NotaPedido)
                .ToList();

            var notasPedidoViewModel = Mapper.Map<List<NotaPedidoViewModel>>(notasPedidoDto);
            ViewBag.Accion = "Revisar";
            return View("ConsultarNotasPedido", notasPedidoViewModel);
        }

        [HttpPost]
        public JsonResult Revisar(RevisadoNotaPedidoViewModel revisadoNotaPedido)
        {
            try
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
                 
                var respuesta = NotaPedidoAppService.RevisarNotaPedido(datosRevisadoNotaPedidoDto);

                if (respuesta != null)
                {
                    return Json(new { success = true, mensaje = "La nota de pedido [" + revisadoNotaPedido.Id + "] ha sido revisada correctamente.", url = Url.Action("Resumen", new { id = respuesta.Id }) }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = false, mensaje = "Se ha producido un error." }, JsonRequestBehavior.AllowGet);
            }
            catch (ApplicationServicesException ex)
            {
                return Json(new { success = false, mensaje = ex.ApiErrorResponse.UserMessage, error = ex.ApiErrorResponse }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mensaje = $"{ex.Message} {ex.InnerException?.Message}", error = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Consultar()
        {
            ViewBag.Accion = "Consultar";
            ViewBag.Mantenimiento = true;
            ViewBag.localSelect = IdLocalSelected;
            ViewBag.IdPerfil = User.Identity.GetClaim("IdPerfil");

            var notasPedidoViewModel = new List<NotaPedidoViewModel>();
            return View("ConsultarNotasPedido", notasPedidoViewModel);
        }

        [HttpPost]
        public ActionResult Consultar(OrderSearchModel model)
        {
            
            ViewBag.Accion = model.accion;
            ViewBag.Mantenimiento = true;
            var pedidos = GetOutputs(model);

            Session[$"NotaPedidoSearchModel_{ViewBag.Accion}"] = model;

            return PartialView(pedidos);
        }

        public ActionResult ConsultarFactura()
        {
            var model = new FacturaViewModels();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> ConsultarFacturaReporte(FacturaViewModels model, string format = "EXCEL")
        {
            var response = await InventarioAppService.DownloadFacturasAsync(model, format);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                var contentType = response.Content.Headers.ContentType?.MediaType;
                return File(result, contentType);
            }

            return Content(await response.Content.ReadAsStringAsync(), response.Content.Headers.ContentType?.MediaType);
        }

        public ActionResult ReporteVentas()
        {
            var model = new FacturaViewModels();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> ConsultarVentasReporte(FacturaViewModels model, string format = "EXCEL")
        {
            var response = await InventarioAppService.DownloadVentasAsync(model, format);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                var contentType = response.Content.Headers.ContentType?.MediaType;
                return File(result, contentType);
            }

            return Content(await response.Content.ReadAsStringAsync(), response.Content.Headers.ContentType?.MediaType);
        }

        private List<NotaPedidoViewModel> GetOutputs(OrderSearchModel model)
        {
            //var local = IdLocalSelected;
            var local = 0;
            model.tipoTransaccion = TransactionType.NotaPedido;

            if (model.idLocal != null && model.idLocal > 0)
                local = model.idLocal;

            var notasPedidoDto = InventarioAppService.SearchOutputs(local, model) ?? new List<OutputDto>();

            return Mapper.Map<List<NotaPedidoViewModel>>(notasPedidoDto)?.OrderByDescending(m => m.Id).ToList();

        }



        [HttpGet]
        public ActionResult Entregar()
        {
            var notasPedidoDto = NotaPedidoAppService
                .GetOutputsByLocal(IdLocalSelected, TransactionStatus.Revisada, TransactionType.NotaPedido)
                .OrderBy(m => m.IdFormaPago)
                .ToList(); 

            var notasPedidoViewModel = Mapper.Map<List<NotaPedidoViewModel>>(notasPedidoDto);

            var formasPago = ObtenerFormasPago(); 

            ViewBag.Accion = "Entregar";
            return View("ConsultarNotasPedido", notasPedidoViewModel);
        }

        [HttpPost]
        public JsonResult Entregar(EntregaNotaPedidoViewModel entregaNotaPedido)
        {
            try
            {
                if ((entregaNotaPedido.CuentasPorCobrar?.Count ?? 0) == 0)
                {
                    return Json(new { success = false, mensaje = "Debe especificar el detalle de los pagos de la Cuenta Por Cobrar." }, JsonRequestBehavior.AllowGet);
                }

                if (entregaNotaPedido.IdFormaPago == 0)
                {
                    return Json(new { success = false, mensaje = "Debe especificar la forma de pago" }, JsonRequestBehavior.AllowGet);
                }

                if (string.IsNullOrEmpty(entregaNotaPedido.Ruc))
                {
                    return Json(new { success = false, mensaje = "Debe seleccionar el RUC del local" }, JsonRequestBehavior.AllowGet);
                }

                if (entregaNotaPedido.CuentasPorCobrar[0].MetodosPago == null)
                {
                    entregaNotaPedido.CuentasPorCobrar[0].MetodosPago = new List<ReceivablePayMethodDto>();
                    entregaNotaPedido.CuentasPorCobrar[0].MetodosPago.Add(new ReceivablePayMethodDto()
                    {
                         Id = 0,
                         IdCobroDebito = "" ,
                         IdMedioPago = entregaNotaPedido.IdFormaPago,
                         MetodoPago = "",
                         Valor = entregaNotaPedido.CuentasPorCobrar[0].Valor,
                         Descripcion = "",
                         IdCierreCaja = "",
                         UsuarioIngreso = "",
                         FechaIngreso = null,
                         ValorOriginal = entregaNotaPedido.CuentasPorCobrar[0].Valor,
                         Estado = true
                     });
                }

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
            catch (ApplicationServicesException ex)
            {
                return Json(new { success = false, mensaje = ex.ApiErrorResponse.UserMessage, error = ex.ApiErrorResponse }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mensaje = $"{ex.Message} {ex.InnerException?.Message}", error = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Factura(string id, bool descargar = false)
        {
            if (descargar)
            {
                var file = await DocumentoAppService.DownloadFactura(id);
                var stream = await file.Content.ReadAsStreamAsync();

                return File(stream, file.Content.Headers.ContentType.MediaType);
            }

            var factura = DocumentoAppService.GetFacturaById(id);
            
            return await Task.FromResult(View(factura));
        }

        [HttpGet]
        public ActionResult Facturar()
        {
            ViewBag.Accion = "Facturar";
            ViewBag.Mantenimiento = true;

            var model = (Session[$"NotaPedidoSearchModel_{ViewBag.Accion}"] as OrderSearchModel ?? new OrderSearchModel
            {
                tipoTransaccion = TransactionType.NotaPedido,
                accion = ViewBag.Accion,
                fechaInicio = DateTime.Today.ToString("dd/MM/yyyy"),
                fechaFin = DateTime.Today.ToString("dd/MM/yyyy")
            });

            var pedidos = GetOutputs(model);
            
            return View("ConsultarNotasPedido", pedidos);
        }

        [HttpPost]
        public ActionResult Facturar(FacturaNotaPedidoViewModel model)
        { 
            try
            {
                if (model.PorcentajeFactura < 1 || model.PorcentajeFactura > 100)
                {
                    return Json(new { success = false, mensaje = "El porcentaje de facturación no es correcto" }, JsonRequestBehavior.AllowGet);
                }

                if (string.IsNullOrEmpty(model.Ruc))
                {
                    return Json(new { success = false, mensaje = "Debe seleccionar el RUC del local" }, JsonRequestBehavior.AllowGet);
                }
                var factura = JsonConvert.DeserializeObject<DocumentViewModel>(model.Factura);
                factura.Fecha = model.FechaFactura;//Aquí seteo la fecha que elije el usuario del componente fecha
                factura.IdEmpresa = User.Identity.GetIdEmpresa();
                var notaPedido = model.ToInvoiceDto(factura);
                notaPedido.Factura.ConsumidorFinal = notaPedido.ConsumidorFinal;
                notaPedido.Factura.RUC = notaPedido.Ruc;
                notaPedido.Usuario = User.Identity.Name;
                notaPedido.Ip = AppServiceBase.ObtenerIp();

                var respuesta = NotaPedidoAppService.FacturarNotaPedido(notaPedido);

                if (respuesta != null)
                {
                    return Json(new { success = true, mensaje = "Se ha generado la factura correctamente.", url = Url.Action("Factura", new { id = respuesta.Factura.Id }) }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = false, mensaje = "Se ha producido un error." }, JsonRequestBehavior.AllowGet);
            }
            catch (ApplicationServicesException ex)
            {
                return Json(new { success = false, mensaje = ex.ApiErrorResponse.UserMessage, error = ex.ApiErrorResponse }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mensaje = $"{ex.Message} {ex.InnerException?.Message}", error = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
         

        [HttpPost]
        public ActionResult ListaNotasPedido(string accion)
        {
            var estadoNotaPedido = TransactionStatus.EnProceso;

            if (accion == "Empaquetar")
            {
                estadoNotaPedido = TransactionStatus.Aprobada;
            }
            else if (accion == "Revisar")
            {
                estadoNotaPedido = TransactionStatus.Empaquetada;
            }
            else if (accion == "Entregar")
            {
                estadoNotaPedido = TransactionStatus.Revisada;
            }

            var notasPedidoDto = NotaPedidoAppService.GetOutputsByCurrentStatus(estadoNotaPedido);
            var notasPedidoViewModel = Mapper.Map<List<NotaPedidoViewModel>>(notasPedidoDto);
            ViewBag.Accion = accion;
            return PartialView(notasPedidoViewModel);
        }

        [HttpPost]
        public ActionResult DetalleNotaPedido(string idNotaPedido, string accion)
        {
            //var servicio = new NotaPedidoServiceClient();
            //NotaPedidoDto notaPedidoDto = servicio.ConsultarNotaPedidoConDetallesPorId(idNotaPedido); ;
            
            ViewBag.accion = accion;
            
            var notaPedidoDto = NotaPedidoAppService.GetOutputById(idNotaPedido);

            switch (accion)
            {
                case "Aprobar":
                    var notaPedidoAprobViewModel = Mapper.Map<AprobacionNotaPedidoViewModel>(notaPedidoDto);
                     
                    ViewBag.ListaRuc = ObtenerListaRuc();
                    ViewBag.ListaFormaPago = ObtenerFormasPago();
                    return PartialView("AprobarNotaPedido", notaPedidoAprobViewModel);
                case "Revisar":
                    var notaPedidoRevisionViewModel = Mapper.Map<EmpaquetadoNotaPedidoViewModel>(notaPedidoDto);
                     
                    ViewBag.JsonTiposPaquete = ObtenerTiposPaquete();
                    return PartialView("RevisarNotaPedido", notaPedidoRevisionViewModel);
                case "Empaquetar":
                    var notaPedidoEmpViewModel = Mapper.Map<EmpaquetadoNotaPedidoViewModel>(notaPedidoDto);
                     
                    ViewBag.JsonTiposPaquete = ObtenerTiposPaquete();
                    return PartialView("EmpaquetarNotaPedido", notaPedidoEmpViewModel);
                default: //entregar
                    var notaPedidoViewModel = Mapper.Map<EntregaNotaPedidoViewModel>(notaPedidoDto);
                    
                    ViewBag.ListaMediosPago = new SelectList(MedioPagoAppService.ObtenerMediosDePago(), "Id", "MedioPago");
                    return PartialView("EntregarNotaPedido", notaPedidoViewModel);
            }
        }


        #region Clientes
        [HttpPost]
        public ActionResult ConsultarClientesCrear(string identidad)
        {
            var clientesDto = ClientesAppService.BuscarClientesCrear(identidad);
            //var ClientesModel = Mapper.Map<List<ClienteViewModel>>(clientesDto);
            if (clientesDto.Count > 0)
            {
                ClienteViewModel ClientesModel = new ClienteViewModel();
                ClientesModel.Correo = clientesDto[0].Correo;
                ClientesModel.RazonSocial = clientesDto[0].RazonSocial;
                ClientesModel.Telefono = clientesDto[0].Telefono;
                ClientesModel.Celular = clientesDto[0].Celular;
                ClientesModel.Direccion = clientesDto[0].Direccion;
                ClientesModel.FechaNacimiento = clientesDto[0].FechaNacimiento;
                ClientesModel.EstadoCivil = clientesDto[0].EstadoCivil;
                ClientesModel.NivelEstudio = clientesDto[0].NivelEstudio;
                ClientesModel.Profesion = clientesDto[0].Profesion;

                return Json(new { success = true, client = ClientesModel }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, mensaje = $"No hay dato" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ConsultarClientes()
        {
            ViewBag.FiltrosBusqueda = ObtenerFiltrosBusquedaCliente();
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConsultarClientes(string filtroBusqueda, string textoBusqueda)
        {
            bool identification = (filtroBusqueda == "1");

            var clientesDto = ClientesAppService.BuscarClientes(textoBusqueda, identification);
            var ClientesModel = Mapper.Map<List<ClienteViewModel>>(clientesDto);
            return PartialView("ListaClientes", ClientesModel);
        }

        [HttpGet]
        public ActionResult EditarCliente(int id)
        {
            var cliente = ClientesAppService.ObtenerClientePorId(id);
            if (cliente == null)
            {
                ViewBag.ErrorMessage = "El cliente especificado no existe!";
                return RedirectToAction("CrearCliente");
            }

            if (cliente.EsConsumidorFinal)
            {
                throw new Exception("El cliente especificado no se puede modificar");                
            }

            var model = Mapper.Map<ClientDto, ClienteViewModel>(cliente);

            ViewBag.ListaPaises = ObtenerPaises();
            ViewBag.ListaSectores = ObtenerSectores();
            
            if (model.IdPais > 0)
            {
                ViewBag.ListaProvincias = CatalogosAppService
                        .ObtenerProvincias(model.IdPais)
                        .ToSelectList("Id", "Provincia"); 
            }

            if (model.IdProvincia > 0)
            {
                ViewBag.ListaCiudades = CatalogosAppService
                        .ObtenerCiudades(model.IdProvincia)
                        .ToSelectList("Id", "Ciudad");
            }

            return PartialView("CrearCliente", model);
        }

        [HttpGet]
        public ActionResult CrearCliente()
        {
            ViewBag.ListaPaises = ObtenerPaises();
            ViewBag.ListaSectores = ObtenerSectores();
            
            return PartialView(new ClienteViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CrearCliente([Bind(Exclude = "Foto")] ClienteViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpPostedFileBase poImgFile = Request.Files["file"];
                        using (var binary = new BinaryReader(poImgFile.InputStream))
                        {
                            //model.Foto = binary.ReadBytes(poImgFile.ContentLength);
                        }
                    }

                    var clienteDto = Mapper.Map<ClientDto>(model);
                    clienteDto.IdEmpresa = User.Identity.GetIdEmpresa();
                    clienteDto.UsuarioIngreso = User.Identity.Name;
                    clienteDto.FechaIngreso = DateTime.Now;
                    clienteDto.IpIngreso = AppServiceBase.ObtenerIp();
                    
                    var result = ClientesAppService.GuardarCliente(clienteDto);

                    return Json(new { success = true, idCliente = result.Id, client = result }, JsonRequestBehavior.AllowGet);
                }

                var sb = new StringBuilder();
                foreach (var item in ModelState.Values)
                { 
                    foreach (var msg in item.Errors)
                    {
                        sb.AppendLine(msg.ErrorMessage);
                    }
                }
                 
                return Json(new { success = false, mensaje= $"Debe completar los datos requeridos:\r\n{sb}" }, JsonRequestBehavior.AllowGet);
            }
            catch (ApplicationServicesException ex)
            {
                return Json(new { success = false, mensaje = ex.ApiErrorResponse.UserMessage, error = ex.ApiErrorResponse.TechnicalMessage}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mensaje=ex.Message, error=ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult ObtenerProvinciasPorPais(short idPais)
        {
            var provinciasDto = CatalogosAppService.ObtenerProvincias(idPais);
            //var provinciasViewModel = Mapper.Map<List<ProvinciaViewModel>>(provinciasDto);
            return Json(provinciasDto, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ObtenerCiudadesPorProvincia(int idProvincia)
        {
            //CiudadService.CiudadServiceClient servicio = new CiudadService.CiudadServiceClient();
            //var ciudadesDto = servicio.ConsultarCiudadesPorProvincia(idProvincia);
            var ciudadesDto = CatalogosAppService.ObtenerCiudades(idProvincia);
            //var ciudadesViewModel = Mapper.Map<List<CiudadViewModel>>(ciudadesDto);
            return Json(ciudadesDto, JsonRequestBehavior.AllowGet);
        }

        #endregion

        [HttpPost]
        public ActionResult GenerarCuentasPorCobrar(string formaPago, int numeroPagos, int plazoDias, decimal valorNotaPedido, decimal valorPagado)
        {
            var detallePagos = new List<CuentasPorCobrarViewModel>();
            DateTime fechaInicial = DateTime.Today;
            
            decimal valorNotaPedidoReal = valorNotaPedido;
            decimal valorCuota0 = valorPagado;

            if (formaPago == "2" && valorPagado >= valorNotaPedido)
            {
                formaPago = "1";
            }

            CuentasPorCobrarViewModel abono;

            if (formaPago == "1")
            {
                if (valorPagado < valorNotaPedido && valorPagado != 0)
                {
                    valorPagado = valorNotaPedido;
                }

                if (valorPagado < valorNotaPedido)
                {
                    formaPago = "2";
                    numeroPagos = 1;
                }
                else
                {
                    valorCuota0 = valorNotaPedido;
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
                valorNotaPedidoReal = valorNotaPedido - valorPagado;
                
                if (valorNotaPedidoReal<0)
                {
                    valorNotaPedidoReal = 0;
                }

                abono = new CuentasPorCobrarViewModel
                {
                    Id = 0,
                    Numero = 0,
                    Valor = valorCuota0,
                    ValorPendiente = 0,
                    ValorPagado = valorPagado,
                    FechaVencimiento = fechaInicial
                };
                 
                detallePagos.Add(abono); 
            }
            
            decimal valorCuotaEstimada = (numeroPagos > 0) ? Math.Round(valorNotaPedidoReal / numeroPagos, 2) : valorNotaPedidoReal;

            if (valorCuotaEstimada > 0)
            {
                for (int i = 1; i <= numeroPagos; i++)
                {
                    fechaInicial = fechaInicial.AddDays(plazoDias);

                    detallePagos.Add(new CuentasPorCobrarViewModel { Id = 0, Numero = i, Valor = valorCuotaEstimada, ValorPendiente = valorCuotaEstimada, ValorPagado = 0, FechaVencimiento = fechaInicial });
                }

                var lastDetalle = detallePagos.LastOrDefault();

                if (lastDetalle != null)
                {
                    lastDetalle.Valor = ((valorNotaPedido - valorPagado) - (valorCuotaEstimada * (numeroPagos - 1)));
                    lastDetalle.ValorPendiente = lastDetalle.Valor;
                }
            }


            var mediosPago = MedioPagoAppService.ObtenerMediosDePago();
            var model = new NotaPedidoModel { CuentasPorCobrar = detallePagos };
            
            ViewBag.ListaMediosPago = new SelectList(mediosPago, "Id", "MedioPago");
            ViewBag.EditarPagos = (formaPago != "1");
            
            return PartialView(model);
        }

        [Authorize]
        public async Task<ActionResult> ImprimirSalidaPDF(int? idEmpresa, string idSalida)
        {

            try
            { 
                if (string.IsNullOrEmpty(idSalida))
                {
                    throw new Exception("Debe especificar el numero de transacción");
                }
                 
                Stream report = await reportingService.ObtenerReporteSalidaEnPDF(idEmpresa, idSalida);

                if (report == null)
                {
                    throw new Exception("Error al emitir el reporte de la salida.");
                }

                var contentDisposition = new ContentDisposition
                {
                    FileName = "Salida_" + idSalida + ".pdf",
                    Inline = true
                };

                Response.AppendHeader("Content-Disposition", contentDisposition.ToString());
                return new FileStreamResult(report, "application/pdf");

            }
            catch (Exception ex)
            {
#if DEBUG
                TempData["ErrorMessage"] = ex.ToString();
#else
                TempData["ErrorMessage"] = ex.Message;
#endif 
                return RedirectToAction("ErrorPage400Manual", "Pages");

            }

        }

        public ActionResult SetNotasPedido()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CreaCantidadNotasPedido(int cantidad)
        {
            if (Session["NotasPedido"] == null)
                Session["NotasPedido"] = 1;

            int notasPedidoTemp = (int)Session["NotasPedido"];
            var shoppingCar = (List<DetalleNotaPedidoViewModel>)Session["ShoppingCar"];
            if (shoppingCar.Any())
                return Json(new { success = false, mensaje = "El carrito de compras tiene items, no es posible cambiar la cantidad de Notas de Pedido múltiples." }, JsonRequestBehavior.AllowGet);
            Session["NotasPedido"] = cantidad;
            return Json(new { success = true, mensaje = $"Se procesaran {cantidad} Notas de pedido" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Unidades(int idProducto)
        {
            return Json(new
            {
                success = true,
                data = ObtenerListaTiposUnidad(idProducto)
            });
        }


        #region Metodos Privados

        private List<TipoUnidadModel> ObtenerListaTiposUnidad(int idProducto)
        {
            var unitstype = TipoUnidadAppService.ObtenerTiposUnidadPorProducto(idProducto);

            if (unitstype.Any())
            {
                var tiposUnidadModel = Mapper.Map<List<TipoUnidadModel>>(unitstype);
                return tiposUnidadModel;
            }

            return null;
        }

        private List<RucDtoLite> ObtenerRucs()
        {
            return RucsAppService.ObtenerRucs();
        }


        private SelectList ObtenerListaRuc()
        {
            return ObtenerRucs()
                   .ToSelectList("Ruc", "Descripcion");
        }

        private List<SelectListItem> ObtenerFormasPago()
        {
            var listaFormasPago = new List<SelectListItem>(){
                new SelectListItem { Text = "CONTADO", Value = "1"},
                new SelectListItem { Text = "CREDITO", Value = "2"},
            };

            return listaFormasPago;
        }

        private List<SelectListItem> ObtenerFiltrosBusquedaCliente()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{ Text="Identificacion", Value="1"  },
                new SelectListItem{ Text="RazonSocial", Value="2"  }
            };
        }

        private SelectList ObtenerSectores()
        {
            //SectorService.SectorServiceClient servicio = new SectorService.SectorServiceClient();
            //var sectoresDto = servicio.ConsultarSectoresPorFiltro("1", "", SectorService.EstadoDto.ACTIVO);
            //var sectoresViewModel = Mapper.Map<List<SectorViewModel>>(sectoresDto);
            return CatalogosAppService
                    .ObtenerSectores()
                    .ToSelectList("Id", "Sector"); 
        }

        private SelectList ObtenerPaises()
        {
            //PaisService.PaisServiceClient servicio = new PaisService.PaisServiceClient();
            //var paisesDto = servicio.ConsultarPaisesPorFiltro("1", "", PaisService.EstadoDto.ACTIVO);
            var paisesDto = CatalogosAppService
                .ObtenerPaises();

            //var paisesViewModel = Mapper.Map<List<PaisViewModel>>(paisesDto);
            var selectList = paisesDto.ToSelectList("Id", "Pais");
            return selectList;
        }

        private string ObtenerTiposPaquete()
        {
            var tiposPaquetes = CatalogosAppService.ObtenerTiposPaquete();
            //var tiposPaquetes = new List<object>()
            //{
            //    new { Id = 1, Nombre = "CARTON"},
            //    new { Id = 2, Nombre = "FUNDA"}
            //};

            return JsonConvert.SerializeObject(tiposPaquetes.Select(x => new { Id = x.Id, Nombre = x.TipoPaquete }));
        }

        #endregion


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult GenerarFactura(FacturaNotaPedidoViewModel model)
        {
            var notapedido = Session[model.Id] as NotaPedidoModel ?? NotaPedidoAppService.GetOutputById(model.Id)?.ToModel();

            if (notapedido == null)
            {
                ViewBag.ErrorMessage = $"No se encontró la Nota de Pedido # {model.Id}";
                return View("Error");
            }

            if (model.ConsumidorFinal)
            {
                notapedido.Cliente.RazonSocial = "Consumidor Final";
                notapedido.Cliente.Identificacion = "9999999999";
            }
            else
            {
                if (notapedido.IdCliente != model.IdCliente)
                {
                    var cliente = ClientesAppService.ObtenerClientePorId(model.IdCliente);
                    notapedido.IdCliente = model.IdCliente;
                    notapedido.NombreCliente = cliente.RazonSocial;
                    notapedido.Cliente = Mapper.Map<ClientDto, ClienteViewModel>(cliente);
                }
            }

            notapedido.Ruc = model.Ruc;
            notapedido.Observaciones = model.Observaciones;
            notapedido.PorcentajeFactura = model.PorcentajeFactura;
            notapedido.ConsumidorFinal = model.ConsumidorFinal;
            
            var factura = notapedido.ToFactura();

            factura.Fecha = model.FechaFactura;
            factura.IdFormaPagoSri = factura.IdFormaPagoSri == 0 ? model.IdFormaPagoSri : factura.IdFormaPagoSri;
            notapedido.Factura = factura;

            //actualizo la forma de pago sri a la salida
            var request = new SalidaFormaPagoSriRequest
            {
                IdSalida = notapedido.Id,
                IdFormaPagoSri = factura.IdFormaPagoSri
            };
            MedioPagoAppService.ActualizarPagoSriBySalida(request);

            Session[model.Id] = notapedido;

            return PartialView("MantNotaPedido_Factura", factura);
        }

         

        [Authorize]
        public ActionResult Editar(string id)
        {
            var notaPedidoModel = NotaPedidoAppService.GetOutputById(id)?.ToModel();//servicio.ConsultarNotaPedidoConDetallesPorId(id); ;
            
            if (string.IsNullOrEmpty(notaPedidoModel?.Id))
            {
                ViewBag.ErrorMessage = $"No se encontró la Nota de Pedido # {id}";
                return View("Error");
            }

            var localPedido = BuscarLocal(notaPedidoModel.IdLocal);

            notaPedidoModel.LocalSeleccionado = localPedido.Local;
            if (notaPedidoModel.Entrega != null)
            {
                notaPedidoModel.Entrega = notaPedidoModel.Entrega.Trim();
            }
            
            var rucs = ObtenerRucs();
            
            if (string.IsNullOrEmpty(notaPedidoModel.Ruc))
            {
                var defaultRUC = rucs.FirstOrDefault(x => x.Ruc == localPedido.DefaultRUC || x.LocalPorDefecto == Session.GetIdLocal());
                if (defaultRUC != null)
                {
                    notaPedidoModel.Ruc = defaultRUC.Ruc;
                }
            }

            var FormaPagosSri = MedioPagoAppService.ObtenerFormasPagoSri();
            SalidaFormaPagoSriDto salidaFormaPago = MedioPagoAppService.ObtenerFormasPagoSriBySalida(notaPedidoModel.Id);

            notaPedidoModel.IdFormaPagoSri = salidaFormaPago?.IdFormaPagoSri ?? 0;

            if (notaPedidoModel.IdFormaPagoSri == 0 && FormaPagosSri.Any())
            {
                var defaultSri = FormaPagosSri.FirstOrDefault(x => x.descripcion.ToUpper().Contains("OTROS"));
                if (defaultSri != null)
                {
                    notaPedidoModel.IdFormaPagoSri = defaultSri.id;
                }
            }

            var listaRucs = rucs.ToSelectList("Ruc", "Descripcion");
            var listaFormaPagosSri = FormaPagosSri.ToSelectList("Id", "Descripcion");

            ViewBag.JsonTiposPaquete = ObtenerTiposPaquete();
            ViewBag.ListaRuc = listaRucs;
            ViewBag.ListaFormaPagoSri = listaFormaPagosSri;
            ViewBag.ListaFormaPago = ObtenerFormasPago();
            var mediosPago = MedioPagoAppService.ObtenerMediosDePago();
            ViewBag.ListaMediosPago = new SelectList(mediosPago, "Id", "MedioPago");
            //Session[id] = notaPedidoModel;
            if (notaPedidoModel.EstadoActual == "REVISADA")
            {
                List<string> auxRecibo = new List<string>();
                List<decimal> auxReciboValor = new List<decimal>();
                int index = 0;
                foreach (var cuenta in notaPedidoModel.CuentasPorCobrar) 
                {
                    if (cuenta.MetodosPago.Count() > 0)
                    {
                        foreach (var metodosPago in cuenta.MetodosPago)
                        {
                            if(!notaPedidoModel.Recibo.Contains(metodosPago.IdCobroDebito))
                            {    
                                notaPedidoModel.Recibo.Add(metodosPago.IdCobroDebito);

                                decimal r = 0;
                                r = metodosPago.Valor;

                                notaPedidoModel.ReciboValor.Add(r);
                            }
                            else
                            {
                                index = notaPedidoModel.Recibo.FindIndex(x => x.Contains(metodosPago.IdCobroDebito));

                                notaPedidoModel.ReciboValor[index] += metodosPago.Valor; 
                            }
                        }
                        
                    }                                
                }
            }

            if (Request.Has("cambiarlocal"))
            {
                return View("CambiarLocal", notaPedidoModel);
            }

            return View("MantNotaPedido", notaPedidoModel);
        }

        [Authorize]
        public ActionResult NotaCredito(string id)
        {
            var notaPedidoModel = NotaPedidoAppService.GetOutputById(id)?.ToModel();//servicio.ConsultarNotaPedidoConDetallesPorId(id); ;

            if (string.IsNullOrEmpty(notaPedidoModel?.Id))
            {
                ViewBag.ErrorMessage = $"No se encontró la Nota de Pedido # {id}";
                return View("Error");
            }

            var localPedido = BuscarLocal(notaPedidoModel.IdLocal);

            notaPedidoModel.LocalSeleccionado = localPedido.Local;
            if (notaPedidoModel.Entrega != null)
            {
                notaPedidoModel.Entrega = notaPedidoModel.Entrega.Trim();
            }

            var rucs = ObtenerRucs();

            if (string.IsNullOrEmpty(notaPedidoModel.Ruc))
            {
                var defaultRUC = rucs.FirstOrDefault(x => x.Ruc == localPedido.DefaultRUC || x.LocalPorDefecto == Session.GetIdLocal());
                if (defaultRUC != null)
                {
                    notaPedidoModel.Ruc = defaultRUC.Ruc;
                }
            }

            var listaRucs = rucs.ToSelectList("Ruc", "Descripcion");

            ViewBag.JsonTiposPaquete = ObtenerTiposPaquete();
            ViewBag.ListaRuc = listaRucs;
            ViewBag.ListaFormaPago = ObtenerFormasPago();
            var mediosPago = MedioPagoAppService.ObtenerMediosDePago();
            ViewBag.ListaMediosPago = new SelectList(mediosPago, "Id", "MedioPago");
            //Session[id] = notaPedidoModel;
            if (notaPedidoModel.EstadoActual == "REVISADA")
            {
                foreach (var cuenta in notaPedidoModel.CuentasPorCobrar)
                {
                    if (cuenta.MetodosPago.Count() > 0)
                    {
                        notaPedidoModel.Recibo.Add(cuenta.MetodosPago[0].IdCobroDebito);
                        decimal r = 0;
                        foreach (var val in cuenta.MetodosPago)
                        {
                            r += val.Valor;
                        }
                        notaPedidoModel.ReciboValor.Add(r);
                    }
                }
            }

            if (Request.Has("cambiarlocal"))
            {
                return View("CambiarLocal", notaPedidoModel);
            }

            return View("NotaCredito", notaPedidoModel);
        }

        [HttpPost]
        public ActionResult NotaCredito(FacturaNotaPedidoViewModel model)
        {
            try
            {
                model.Factura = model.FacturaJson;
                if (model.PorcentajeFactura < 1 || model.PorcentajeFactura > 100)
                {
                    return Json(new { success = false, mensaje = "El porcentaje de facturación no es correcto" }, JsonRequestBehavior.AllowGet);
                }

                if (string.IsNullOrEmpty(model.Ruc))
                {
                    return Json(new { success = false, mensaje = "Debe seleccionar el RUC del local" }, JsonRequestBehavior.AllowGet);
                }
                
                var factura = JsonConvert.DeserializeObject<DocumentViewModel>(model.Factura);
                factura.IdEmpresa = User.Identity.GetIdEmpresa();
                var notaPedido = model.ToInvoiceDto(factura);
                notaPedido.Factura.IdTipoDocumento = "04";
                notaPedido.Factura.ConsumidorFinal = notaPedido.ConsumidorFinal;
                notaPedido.Factura.RUC = notaPedido.Ruc;
                notaPedido.Usuario = User.Identity.Name;
                notaPedido.Ip = AppServiceBase.ObtenerIp();

                var respuesta = NotaPedidoAppService.NotaCreditoElectronica(notaPedido);

                if (respuesta != null)
                {
                    return Json(new { success = true, mensaje = "Se ha generado la factura correctamente.", url = Url.Action("Factura", new { id = respuesta.Factura.Id }) }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = false, mensaje = "Se ha producido un error." }, JsonRequestBehavior.AllowGet);
            }
            catch (ApplicationServicesException ex)
            {
                return Json(new { success = false, mensaje = ex.ApiErrorResponse.UserMessage, error = ex.ApiErrorResponse }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mensaje = $"{ex.Message} {ex.InnerException?.Message}", error = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        private List<WarehouseDto> Locales
        {
            get
            {
                return Session.GetLocales();
            } 
        }

        public WarehouseDto BuscarLocal(int idLocal)
        {
            return Session.GetLocal(idLocal);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Descargar(string id, string format = "PDF")
        {
            var response = await NotaPedidoAppService.DownloadOutputAsync(id, format);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                var contentType = response.Content.Headers.ContentType?.MediaType;
                return File(result, contentType);
            }
            
            return Content(await response.Content.ReadAsStringAsync(), response.Content.Headers.ContentType?.MediaType);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> DescargarSlim(string id, string format = "PDF")
        {
            var response = await NotaPedidoAppService.DownloadOutputSlimAsync(id, format);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStreamAsync();
                var contentType = response.Content.Headers.ContentType?.MediaType;
                return File(result, contentType);
            }

            return Content(await response.Content.ReadAsStringAsync(), response.Content.Headers.ContentType?.MediaType);
        }

        [Authorize]
        public ActionResult Resumen(string id)
        {
            var notaPedidoDto = NotaPedidoAppService.GetOutputById(id);//servicio.ConsultarNotaPedidoConDetallesPorId(id); ;

            if (notaPedidoDto == null)
            {
                ViewBag.ErrorMessage = $"No se encontró la Nota de Pedido # {id}";
                return View("Error");
            }

            foreach (var cxc in notaPedidoDto.CuentasPorCobrar ?? new List<ReceivableDto>())
            {
                cxc.ValorPagado = cxc.Valor - cxc.Saldo;
            }

            var notaPedidoModel = Mapper.Map<NotaPedidoModel>(notaPedidoDto);

            ViewBag.JsonTiposPaquete = ObtenerTiposPaquete();
            ViewBag.ListaRuc = ObtenerListaRuc();
            ViewBag.ListaFormaPago = ObtenerFormasPago();
            ViewBag.ActionUrl = Url.Action(Request.Form["Action"] ?? "");

            return View("MantNotaPedido_Resumen", notaPedidoModel);
        }

        public ActionResult Shopping()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GenerarTransferencia(NotaPedidoViewModel model)
        {
             
            return PartialView("MantNotaPedido_Transferencia", model);
        }


        [HttpGet]
        public JsonResult BuscarVendedor(string searchTerm)
        {
            var result = UserAppService.BuscarVendedores(searchTerm);

            return Json(
                new
                {
                    results = result.Select(data => new { id = data.Id, text = $"{data.Identificacion}  {data.Nombres}", data }).ToArray(),
                    pagination = false
                }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult BuscarCliente(string searchTerm)
        {
            var result = ClientesAppService.BuscarClientes(searchTerm, false, false);

            return Json(
                new
                {
                    results = result.Select(data => new { id = data.Id, text = $"{data.Identificacion}  {data.RazonSocial}", data }).ToArray(),
                    pagination = false
                }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetDetalle(OutputDetailDto model)
        {
            try
            {
                if (model != null)
                {
                    var salida = Session[model.IdSalida] as OutputDto;
                    var detalle = salida?.DetalleNotaPedido?.FirstOrDefault(x => x.Id == model.Id);

                    if (detalle?.Empaquetado != model?.Empaquetado || detalle?.Revisado != model?.Revisado)
                    {
                        detalle.Empaquetado = model?.Empaquetado ?? detalle?.Empaquetado ?? false;
                        detalle.Revisado = model?.Revisado ?? detalle?.Revisado ?? false;
                        NotaPedidoAppService.SetDetail(detalle.Id, revisado: detalle.Revisado, empaquetado: detalle.Empaquetado);
                    }

                    return Json(new { success = true, mensaje = "OK", id = model?.Id }, JsonRequestBehavior.AllowGet);
                }

                throw new ArgumentNullException("pedido");
            }
            catch (ApplicationServicesException ex)
            {
                return Json(new { success = false, mensaje = ex.ApiErrorResponse.UserMessage, id=model?.Id, error = ex.ApiErrorResponse }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mensaje = $"{ex.Message} {ex.InnerException?.Message}", id = model?.Id, error = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult CambiarLocal(NotaPedidoModel model)
        {
            try
            {
                if (model.DetalleNotaPedido == null || model.DetalleNotaPedido.Count == 0 || model.DetalleNotaPedido.Any(x => x.IdProducto == 0))
                {
                    return Json(new RespuestaViewModel { Success = false, Mensaje = "Debe seleccionar algun articulo o producto para generar el cambio." }, JsonRequestBehavior.AllowGet);
                }
                
                if (model.DetalleNotaPedido.Any(x => (x.IdTipoUnidad == 0)))
                {
                    return Json(new RespuestaViewModel { Success = false, Mensaje = "Debe indicar la cantidad de articulos y el tipo de unidad para generar el cambio" }, JsonRequestBehavior.AllowGet);
                }
                //Creamos la nueva salida
                var notaPedidoDto = Mapper.Map<OutputDto>(model);
                var salida = NotaPedidoAppService.SaveOutpuChanget(notaPedidoDto, model.Id, IdLocalSelected);


                if (salida == null)
                {
                    return Json(new RespuestaViewModel { Success = false, Mensaje = "Hubo un error al guardar la transferencia de bodega" }, JsonRequestBehavior.AllowGet);
                }

                //Eliminamos la anterior
                //NotaPedidoAppService.ChangeCuentasPorCobrar(model.Id, salida.Id);
                //AnularPedido(model.Id, "Por cambio de bodega");

                return Json(new { success = true, mensaje = "OK", id = salida?.Id }, JsonRequestBehavior.AllowGet);
            }
            catch (ApplicationServicesException ex)
            {
                return Json(new { status = false, mensaje = ex.ApiErrorResponse?.UserMessage ?? ex.Message, error = ex.ApiErrorResponse }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, mensaje = ex.Message, error = new ApiErrorResponse { ErrorCode = "1000", TechnicalMessage = ex.ToString(), UserMessage = ex.Message } }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ReciboVistaPreliminar(string id)
        {
            var model = CobrosAppService.ReciboPorId(id);

            return PartialView("~/Views/Cobros/ReciboVistaPreliminar.cshtml", model.ToViewModel());
        }

    }
}