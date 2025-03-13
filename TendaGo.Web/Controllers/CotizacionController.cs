using TendaGo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TendaGo.Web.ApplicationServices;
using AutoMapper;
using TendaGo.Common;
using System.Text;
using TendaGo.Web.Infraestructura;
using System.Web.Caching;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TendaGo.Web.Controllers
{

    [Authorize]
    public class CotizacionController : Controller
    {
        public int IdLocalSelected => Session.GetIdLocal() ?? 0;

        public Cache Cache { get { return HttpContext.Cache; } }
        public string Token { get { return User.Identity.GetTokenUsuario(); } }
        public List<PedidoViewModel> Pedidos
        {
            get
            {
                
                var pedidos = Cache[$"{Token}-Pedidos"] as List<PedidoViewModel>;

                if (pedidos == null)
                {
                    pedidos = new List<PedidoViewModel>();
                    pedidos.Add(new PedidoViewModel
                    {
                        NombreCliente = Session.GetConsumidorFinal()?.RazonSocial,
                        IdCliente = Session.GetConsumidorFinal()?.Id ?? 0,
                        Actual = true,
                        Fecha = DateTime.Now,
                        FechaEntrega = DateTime.Now.AddHours(4).Date.ToString("dd/MM/yyyy"),
                        HoraEntrega = DateTime.Now.AddHours(4).TimeOfDay.ToString()
                    });

                    Cache[$"{Token}-Pedidos"] = pedidos;
                }

                return pedidos;
            }
            set
            {
                Cache[$"{Token}-Pedidos"] = value;
            }
        }

        public PedidoViewModel PedidoActual
        {
            get
            {
                var pedido = Pedidos.FirstOrDefault(m => m.Actual) ?? Pedidos.FirstOrDefault();

                if (pedido == null)
                {
                    AddHold();
                    pedido = Pedidos.FirstOrDefault();
                }
                else
                {
                    // Actualizamos el pedido:
                    PedidoActual = pedido;
                }

                if (pedido.DetalleNotaPedido == null)
                {
                    pedido.DetalleNotaPedido = new List<DetalleNotaPedidoViewModel>();
                }

                return pedido;
            }
            set
            {
                if (value != null)
                {
                    value.Actual = true;

                    Pedidos.Where(m => m != value)
                        .ToList()
                        .ForEach(x => x.Actual = false);

                    if (!Pedidos.Contains(value))
                    {
                        Pedidos.Add(value);
                    }
                }
            }
        }
        public List<LiteProductDto> ListaProductos
        {
            get
            {
                var productos = Session["Lista-Productos"] as List<LiteProductDto>;

                if (productos == null)
                {
                    Session["Lista-Productos"] = productos = new List<LiteProductDto>();
                }

                return productos;
            }
        }

        // GET: pos
        public async Task<ActionResult> Index()
        {
            ViewBag.ListaMediosPago = new SelectList(MedioPagoAppService.ObtenerMediosDePago(), "Id", "MedioPago");
            return await Task.FromResult(View(Pedidos));
        }

        /// <summary>
        /// Carga el pedido Actual
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> details()
        {
            return await Task.FromResult(PartialView(PedidoActual));
        }


        public List<LiteProductDto> Products
        {
            get
            {
                var products = Session["MyProducts"] as List<LiteProductDto>;
                
                if (products == null)
                {
                    Session["MyProducts"] = products = new List<LiteProductDto>();
                }

                return products;
            }
            set
            {
                Session["MyProducts"] = value;
            }
        }
        /// <summary>
        /// Carga los productos segun el dato
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> products(string id)
        {
            Products = InventarioAppService.SearchProductsByLocal(IdLocalSelected,Url.Encode(id));

            return await Task.FromResult(PartialView(Products.Select(x => x.Id)));
        }

        public async Task<ActionResult> product(int id)
        {
            var product = Products.FirstOrDefault(x => x.Id == id);

            return await Task.FromResult(PartialView(product));
        }


        public async Task<decimal> totitems()
        { 
            return await Task.FromResult(PedidoActual.DetalleNotaPedido?.Count ?? 0);
        }

        public async Task<string> baseiva()
        {
            //return await Task.FromResult((PedidoActual.DetalleNotaPedido?.Sum(x => x.BaseIVA) ?? 0).ToCustomFormatString());
            return await Task.FromResult((PedidoActual.DetalleNotaPedido?.Sum(x => x.BaseIVA) ?? 0).ToString("#,##0.00"));
        }

        public async Task<string> basecero()
        {
            return await Task.FromResult((PedidoActual.DetalleNotaPedido?.Sum(x => x.BaseCero) ?? 0).ToCustomFormatString());
        }

        public async Task<string> valoriva()
        {
            return await Task.FromResult((PedidoActual.DetalleNotaPedido?.Sum(x => x.IVA) ?? 0).ToCustomFormatString());
        }

        public async Task<string> subtotal()
        {
            return await Task.FromResult((PedidoActual.DetalleNotaPedido?.Sum(x => x.Subtotal + x.IVA) ?? 0).ToCustomFormatString());
        }

        public async Task<string> descuento()
        {
            return await Task.FromResult((PedidoActual.DetalleNotaPedido?.Sum(x => x.ValorDescuento) ?? 0).ToCustomFormatString());
        }

        public int findproduct(string id)
        {
            var producto = ListaProductos.FirstOrDefault(x => string.Equals(x.CodigoInterno, id, StringComparison.InvariantCultureIgnoreCase));

            if (producto == null)
            {
                producto = ProductosAppService.GetProductByInternalCode(Url.Encode(id));

                if (producto != null)
                {
                    ListaProductos.Add(producto);
                }
            }

            return producto?.Id ?? 0;
        }

        public ActionResult holdList()
        {
            return PartialView(Pedidos);
        }
         
        public ActionResult SelectHold(int id)
        {
            if (Pedidos.Count > id)
            {
                PedidoActual = Pedidos[id];
            }
            else
            {
                PedidoActual = Pedidos.LastOrDefault();
            }

            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveHold(int id)
        {
            if (Pedidos.Count > id)
            {
                var select = false;
                if (Pedidos[id].Actual)
                {
                    select = true;
                }

                Pedidos.RemoveAt(id);

                if (Pedidos.Count == 0)
                {
                    Pedidos.Add(new PedidoViewModel
                    {
                        Actual = true,
                        Fecha = DateTime.Now,
                        FechaEntrega = DateTime.Now.AddHours(4).Date.ToString("dd/MM/yyyy"),
                        HoraEntrega = DateTime.Now.AddHours(4).TimeOfDay.ToString()
                    });
                }
                
                if (select) PedidoActual = Pedidos.FirstOrDefault();
            }

            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddHold()
        {
            var nuevoPedido = new PedidoViewModel
            {
                NombreCliente = Session.GetConsumidorFinal()?.RazonSocial,
                IdCliente = Session.GetConsumidorFinal()?.Id ?? 0,
                Actual = true,
                Fecha = DateTime.Now,
                FechaEntrega = DateTime.Now.AddHours(4).Date.ToString("dd/MM/yyyy"),
                HoraEntrega = DateTime.Now.AddHours(4).TimeOfDay.ToString()
            };

            Pedidos.Add(nuevoPedido);

            PedidoActual = nuevoPedido;

            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult modal_save()
        {
            ViewBag.ListaMediosPago = new SelectList(MedioPagoAppService.ObtenerMediosDePago(), "Id", "MedioPago");
            return PartialView(PedidoActual);
        }

        [HttpPost]
        public ActionResult save(posSaveModel model)
        {
            try
            { 
                var pedidoView = PedidoActual;

                if ((pedidoView.DetalleNotaPedido?.Count ?? 0) == 0)
                {
                    throw new Exception("La orden esta vacia!");
                }

                if (pedidoView.IdCliente == 0)
                {
                    throw new Exception("Debe seleccionar un cliente de la lista!");
                }

                var fechaEntrega = DateTime.Today.Date;
                var horaEntrega = new TimeSpan();
                DateTime.TryParseExact(model.fecha, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fechaEntrega);
                TimeSpan.TryParseExact(model.hora, "g", System.Globalization.CultureInfo.InvariantCulture, out horaEntrega);

                StringBuilder sb = new StringBuilder();

                var notaPedidoDto = Mapper.Map<OutputDto>(pedidoView);
                var detalleNotaPedidoDto = Mapper.Map<List<OutputDetailDto>>(pedidoView.DetalleNotaPedido);
            
                notaPedidoDto.Subtotal0 = detalleNotaPedidoDto.Sum(x => x.CobraIva ? 0M : x.Subtotal ?? 0M);
                notaPedidoDto.SubtotalIva = detalleNotaPedidoDto.Sum(x => x.CobraIva ? x.Subtotal ?? 0M : 0M);
                notaPedidoDto.Total = detalleNotaPedidoDto.Sum(x => x.Subtotal ?? 0M);
                notaPedidoDto.Observaciones = model.observaciones;
                notaPedidoDto.FechaHoraEntregaPropuesta = DateTime.Now;
                notaPedidoDto.IdLocal = IdLocalSelected;
                notaPedidoDto.TipoTransaccion = "CT"; // Cotizacion
                notaPedidoDto.IdVendedor = User.Identity.Name;
                notaPedidoDto.UsuarioIngreso = User.Identity.Name;
                notaPedidoDto.FechaIngreso = DateTime.Now;
                notaPedidoDto.IpIngreso = AppServiceBase.ObtenerIp();
                notaPedidoDto.DetalleNotaPedido = detalleNotaPedidoDto;
                notaPedidoDto.IdFormaPago = model.formapago;
                notaPedidoDto.Plazo = model.plazo;
                notaPedidoDto.Cuotas = model.cuotas;
                notaPedidoDto.Ruc = model.ruc;
                notaPedidoDto.EstadoActual = "COTIZACION";


                foreach (var item in notaPedidoDto.DetalleNotaPedido)
                {
                    if (item.CobraIva)
                    {
                        if (item.IncluyeIva)
                        {
                            decimal ivaDecimal = Convert.ToDecimal(item.PorcentajeTarifaImpuesto);
                            var iva = 1 + (ivaDecimal / 100);// 1.porcentajeIva
                            item.Precio *= iva;
                        }

                        item.Subtotal = decimal.Round((item.Subtotal ?? 0M) + item.IVA, 2);
                    }
                }

                notaPedidoDto.Total = decimal.Round(detalleNotaPedidoDto.Sum(x => x.Subtotal ?? 0M), 2);

                var notaResult = NotaPedidoAppService.SaveOutput(notaPedidoDto);

                if (notaResult != null)
                {
                    Pedidos.Remove(pedidoView);

                    if (Pedidos.Count == 0)
                    {
                        AddHold();
                    }
                    else
                    {
                        PedidoActual = Pedidos.FirstOrDefault();
                    }

                    sb.AppendLine("La nota de pedido [" + notaResult.Id + "] ha sigo guardada correctamente.");
                    /**
                        AGREGAR CONDICION PARA IMPRIMIR EN PUNTO DE VENTA 
                     **/
                    // Si se facturo entonces devuelvo la url de la factura:

                    var url = string.IsNullOrEmpty(notaResult?.Factura?.Id) ?
                     Url.Action("Descargar", "cotizacion", new { id = notaResult.Id, format = "PDF" }) :
                     (string.IsNullOrEmpty(notaResult?.Id) ? "" : Url.Action("Factura", "NotaPedido", new { id = notaResult?.Factura?.Id }));
                    
                    if (model.slim)
                    {
                        url = Url.Action("DescargarSlim", "NotaPedido", new { id = notaResult.Id, format = "PDF" });
                    }

                    return Json(new { status = true, message = sb.ToString(), url }, JsonRequestBehavior.AllowGet);
                }

                throw new Exception("Hubo un error al guardar el pedido!");
            }
            catch (ApplicationServicesException ex)
            {
                return Json(new { status = false, message = ex.ApiErrorResponse?.UserMessage ?? ex.Message, error = ex.ApiErrorResponse }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message, error = new ApiErrorResponse { ErrorCode = "1000", TechnicalMessage = ex.ToString(), UserMessage = ex.Message } }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult customer()
        {
            return Json(new { id = PedidoActual.IdCliente, text = PedidoActual.NombreCliente ?? "Por favor seleccione un cliente" }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult customers(string term, int page)
        {
            var result = ClientesAppService.BuscarClientes(term, lite: true, page: page);

            return Json(new Select2ViewModel(result.Select(x => new Select2Result { id = x.Id.ToString(), text = $"{x.Identificacion} {x.RazonSocial}" }), result.Count > 0), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult customer(int id, string name = null)
        {
            PedidoActual.IdCliente = id;
            PedidoActual.NombreCliente = name;
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }

        public class posSaveModel
        {
            public string ruc { get; set; }
            public string fecha { get; set; }
            public string hora { get; set; }
            public string observaciones { get; set; }
            public int formapago { get; set; }
            public decimal pagado { get; set; }

            public int cuotas { get; set; }
            public int plazo { get; set; }
            public int medioPago { get; set; }

            public string numeroTarjeta { get; set; }
            public string holderTarjeta { get; set; }
            public string mesTarjeta { get; set; }
            public string anioTarjeta { get; set; }
            public string codigocvTarjeta { get; set; }
            public string chequenum { get; set; }
            public bool slim { get; set; }

            public List<CuentasPorCobrarViewModel> CuentasPorCobrar { get; set; }
        }

        public class posEditModel
        {
            public decimal qt { get; set; }
            public int und { get; set; }
            public decimal? prc { get; set; }
        }

        [HttpPost]
        public string edit(int id, posEditModel model)
        {
            var detalle = PedidoActual.DetalleNotaPedido?.FirstOrDefault(x => x.IdProducto == id);

            if (detalle != null)
            {
                var stock = ProductosAppService.ObtenerStockProducto(id, IdLocalSelected);
                
                if (stock == null || (stock.Stock- model.qt) < (stock.StockMinimo ?? 0))
                {
                    if (stock.TipoProducto.Trim() == "PRODUCTO")
                    {
                        return "stock";
                    }
                }

                var unidad = detalle.TipoUnidades?.FirstOrDefault(x => x.Id == model.und);

                if (unidad == null)
                {
                    unidad = detalle.TipoUnidades?.FirstOrDefault();
                }

                var oldprice = detalle.Precio;

                // Si cambia de tipo de unidad cambia el precio:
                if (model.und != detalle.IdTipoUnidad && unidad?.Precio > 0)
                {
                    detalle.Precio = unidad.Precio ?? 0M;
                    
                    if (stock.CobraIva == true && unidad.IncluyeIva == true)
                    {
                        decimal ivaDecimal = Convert.ToDecimal(detalle.PorcentajeTarifaImpuesto);
                        var iva = 1 + (ivaDecimal / 100);// 1.porcentajeIva
                        detalle.Precio /= iva;
                    }
                }
                else if (model.prc != decimal.Round(detalle.Precio,2))
                { 
                    detalle.Precio = model.prc ?? 0M;
                }

                detalle.CostoVenta = stock.Costo;
                detalle.CobraIva = stock.CobraIva ?? false;
                detalle.IncluyeIva = unidad.IncluyeIva ?? false;
                detalle.IdTipoUnidad = unidad?.Id ?? 0;
                detalle.Rise = PedidoActual.Rise;
                detalle.Cantidad = model.qt;

            }
            else
            {
                return "stock";
            }

            return "ok";
        }


        [HttpPost]
        public ActionResult ResetPos()
        {
            Pedidos = new List<PedidoViewModel>();

            return AddHold();
        }


        [HttpPost]
        public JsonResult ruc(RucDtoLite model)
        {
            if (model?.Ruc != null)
            {
                PedidoActual.Ruc = model;
            }
            else
            {
                PedidoActual.Ruc = null;
            }

            foreach (var detalle in PedidoActual.DetalleNotaPedido)
            {
                detalle.Rise = PedidoActual.Rise;
            }

            return Json(model?.Ruc, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string delete(int id)
        {
            var detalle = PedidoActual.DetalleNotaPedido?.FirstOrDefault(x => x.IdProducto == id);

            if (detalle != null)
            {
                PedidoActual.DetalleNotaPedido.Remove(detalle);
            }

            return "ok";
        }

        [HttpPost]
        public string addpdc(int id )
        { 
            // Buscamos el item a agregar:
            var detalle = PedidoActual.DetalleNotaPedido?.FirstOrDefault(x => x.IdProducto == id);
            if (detalle == null)
            {
                var stock = ProductosAppService.ObtenerStockProducto(id, IdLocalSelected);               
                if (stock == null || stock.Stock <= (stock.StockMinimo ?? 0))
                {
                    if (stock.TipoProducto.Trim() == "PRODUCTO")
                    {
                        return "stock";
                    }
                }


                var unidades = TipoUnidadAppService.ObtenerTiposUnidadPorProducto(id);

                if (unidades.Count == 0)
                {
                    return "unidad";
                }

                var tipounidad = unidades.FirstOrDefault() ?? new UnitTypeDto();
                var precio = tipounidad.Precio ?? 0M;

                if ((stock.CobraIva ?? false) && (tipounidad.IncluyeIva ?? false))
                {
                    decimal ivaDecimal = Convert.ToDecimal(stock.porcentajeTarifaImpuesto);
                    var iva = 1 + (ivaDecimal / 100);// 1.porcentajeIva
                    precio /= iva;
                }

                detalle = new DetalleNotaPedidoViewModel
                {
                    Cantidad = 1M,
                    CantidadMinima = (stock.StockMinimo ?? 0) > 0 ? stock.StockMinimo.Value : 1M,
                    CodigoInterno = stock.CodigoInterno,
                    DescripcionProducto = stock.Descripcion,
                    IdProducto = stock.Id,
                    Nombre = stock.Producto,
                    NombreProducto = stock.Producto,
                    TipoUnidades = unidades,
                    CantidadTotalDisponible = stock.Stock,
                    UnidadMedida = tipounidad.UnidadMedida,
                    DesTipoUnidad = tipounidad.TipoUnidad,
                    IdTipoUnidad = tipounidad.Id,
                    Precio = precio-((precio* stock.Descuento ?? 0) /100),
                    PrecioSugerido = precio - ((precio * stock.Descuento ?? 0) / 100),
                    CostoVenta = stock.Costo,
                    IncluyeIva = tipounidad.IncluyeIva ?? false,
                    CobraIva = stock.CobraIva ?? false,
                    Rise = PedidoActual.Rise,
                    Descuento = stock.Descuento ?? 0
                   // ValorDescuento = (decimal)((stock.Descuento > 0) ?  (precio* stock.Descuento) /100  : 0)
                };

                PedidoActual.DetalleNotaPedido.Add(detalle);
            }
            else
            {
                var stock = ProductosAppService.ObtenerStockProducto(id, IdLocalSelected);

                var newQty = detalle.Cantidad + 1;

                if (stock == null || (stock.Stock - newQty) <= (stock.StockMinimo ?? 0))
                {
                    if (stock.TipoProducto.Trim() == "PRODUCTO")
                    {
                        return "stock";
                    }
                }

                detalle.Cantidad = newQty;
            }

            return "ok";
        }

        #region Cotizacion

        [HttpGet]
        public ActionResult Aprobar()
        {
            var notasPedidoDto = NotaPedidoAppService
                .GetOutputsByLocal(IdLocalSelected, TransactionStatus.Cotizacion, TransactionType.Cotizacion)
                .OrderBy(m => m.FechaHoraEntregaPropuesta)
                .ToList();

            var notasPedidoViewModel = Mapper.Map<List<NotaPedidoViewModel>>(notasPedidoDto);
            ViewBag.Accion = "Aprobar Cotizacion";
            return View("ConsultarCotizacion", notasPedidoViewModel);
        }
        [HttpGet]
        public ActionResult Consultar()
        {
            ViewBag.Accion = "Aprobar";
            ViewBag.Mantenimiento = true;

            var notasPedidoViewModel = new List<NotaPedidoViewModel>();
            return View("ConsultarCotizacion", notasPedidoViewModel);
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
        private List<NotaPedidoViewModel> GetOutputs(OrderSearchModel model)
        {
            model.tipoTransaccion = TransactionType.NotaPedido;

            var notasPedidoDto = InventarioAppService.SearchOutputs(IdLocalSelected, model) ?? new List<OutputDto>();

            return Mapper.Map<List<NotaPedidoViewModel>>(notasPedidoDto)?.OrderByDescending(m => m.Id).ToList();

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

        private string ObtenerTiposPaquete()
        {
            var tiposPaquetes = CatalogosAppService.ObtenerTiposPaquete();
            return JsonConvert.SerializeObject(tiposPaquetes.Select(x => new { Id = x.Id, Nombre = x.TipoPaquete }));
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
        private List<RucDtoLite> ObtenerRucs()
        {
            return RucsAppService.ObtenerRucs();
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
                    return Json(new { success = true, mensaje = "La contización [" + id + "] ha sido anulada correctamente." }, JsonRequestBehavior.AllowGet);
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

        #endregion
    }

}