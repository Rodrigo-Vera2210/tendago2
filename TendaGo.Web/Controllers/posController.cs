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

namespace TendaGo.Web.Controllers
{

    [Authorize]
    public class posController : Controller
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
            ViewBag.ListaFormaPagoSri = new SelectList(MedioPagoAppService.ObtenerFormasPagoSri(), "Id", "Descripcion");
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
            return await Task.FromResult((PedidoActual.DetalleNotaPedido?.Sum(x => x.BaseCero) ?? 0).ToString("#,##0.00"));
        }

        public async Task<string> valoriva()
        {
            return await Task.FromResult((PedidoActual.DetalleNotaPedido?.Sum(x => x.IVA) ?? 0).ToCustomFormatString());
        }

        public async Task<string> subtotal()
        {
            return await Task.FromResult((PedidoActual.DetalleNotaPedido?.Sum(x => x.Subtotal + x.IVA) ?? 0).ToCustomFormatString());
        }

        public async Task<int> idCliente()
        {
            return await Task.FromResult((PedidoActual.IdCliente));
        }

        public async Task<string> NombreCliente()
        {
            return await Task.FromResult((PedidoActual.NombreCliente));
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
            ViewBag.ListaFormaPagoSri = new SelectList(MedioPagoAppService.ObtenerFormasPagoSri(), "Id", "Descripcion");
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
                notaPedidoDto.FechaHoraEntregaPropuesta = fechaEntrega.Add(horaEntrega);
                notaPedidoDto.IdLocal = IdLocalSelected;
                notaPedidoDto.TipoTransaccion = "NP"; // Nota de Pedido
                notaPedidoDto.IdVendedor = User.Identity.Name;
                notaPedidoDto.UsuarioIngreso = User.Identity.Name;
                notaPedidoDto.FechaIngreso = DateTime.Now;
                notaPedidoDto.IpIngreso = AppServiceBase.ObtenerIp();
                notaPedidoDto.DetalleNotaPedido = detalleNotaPedidoDto;
                notaPedidoDto.IdFormaPago = model.formapago;
                notaPedidoDto.Plazo = model.plazo;
                notaPedidoDto.Cuotas = model.cuotas;
                notaPedidoDto.Ruc = model.ruc;

                //if (AppServiceBase.Empresa.IncluyeIVA)
                //{
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
                //}

                if (AppServiceBase.Empresa.FlujoInventario)
                {
                    notaPedidoDto.EstadoActual = "APROBADA";
                }
                else
                {
                     
                    if (AppServiceBase.Empresa.FacturaPOS && !string.IsNullOrEmpty(notaPedidoDto.Ruc)) // Empresa.FacturaPOS
                    {
                        notaPedidoDto.Factura = notaPedidoDto.ToDocumentDto();
                        notaPedidoDto.Factura.IdFormaPagoSri = model.formaPagoSri;
                        notaPedidoDto.EstadoActual = "FACTURADA";
                    }
                    else
                    {
                        notaPedidoDto.EstadoActual = "ENTREGADA";
                    }


                    if (model.pagado > notaPedidoDto.Total)
                    {
                        model.pagado = notaPedidoDto.Total;
                    }
                }

                //notaPedidoDto.CuentasPorCobrar = GenerarCuentasPorCobrar(model.formapago, model.cuotas, model.plazo, notaPedidoDto.Total, model.pagado);
                //notaPedidoDto.CuentasPorCobrar.FirstOrDefault().IdMedioPago = model.medioPago;
                notaPedidoDto.CuentasPorCobrar = new List<ReceivableDto>();
                foreach (var metodo in model.CuentasPorCobrar)
                {
                    notaPedidoDto.CuentasPorCobrar.Add(new ReceivableDto() {
                        Id=metodo.Id,
                        Numero=metodo.Numero,
                        FechaVencimiento=metodo.FechaVencimiento,
                        Valor=metodo.Valor,
                        ValorPagado=metodo.ValorPagado,
                        Saldo=metodo.Saldo
                    });
                                        
                    if (metodo.MetodosPago != null)
                    {
                        notaPedidoDto.CuentasPorCobrar[0].MetodosPago = new List<ReceivablePayMethodDto>();
                        foreach (var me in metodo.MetodosPago)
                        {
                            var detalle = new ReceivablePayMethodDto
                            {
                                IdMedioPago = me.IdMedioPago,
                                Valor = me.Valor,
                                Descripcion = me.Descripcion,
                            };
                            notaPedidoDto.CuentasPorCobrar[0].MetodosPago.Add(detalle);
                        }
                    }

                }

                if (model.InformacionAdicional!=null)
                {
                    notaPedidoDto.InformacionAdicional = new List<AditionalInfoDto>();
                    foreach (var info in model.InformacionAdicional)
                    {
                        if (!string.IsNullOrEmpty(info.TituloInfoAdicional) && !string.IsNullOrEmpty(info.InfoAdicional)) 
                        {
                            notaPedidoDto.InformacionAdicional.Add(new AditionalInfoDto()
                            {
                                TituloInfoAdicional = info.TituloInfoAdicional.ToUpper(),
                                InfoAdicional = info.InfoAdicional.ToUpper()
                            });
                        }                        
                    }
                }

                //Descuento
                if (model.Descuento > 0)
                {
                    notaPedidoDto.Descuento = model.Descuento;
                    notaPedidoDto.Total = notaPedidoDto.Total - model.ValorDescuento;
                }

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
                     Url.Action("Descargar", "NotaPedido", new { id = notaResult.Id, format = "PDF" }) :
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

        public List<ReceivableDto> GenerarCuentasPorCobrar(int formaPago, int numeroPagos, int plazoDias, decimal valorNotaPedido, decimal valorPagado)
        {
            var detallePagos = new List<ReceivableDto>();
            DateTime fechaInicial = DateTime.Today;

            decimal valorNotaPedidoReal = valorNotaPedido;
            decimal valorCuota0 = valorPagado;

            if (formaPago == 2 && valorPagado >= valorNotaPedido)
            {
                formaPago = 1;
            }

            ReceivableDto abono;

            if (formaPago == 1)
            {
                if (valorPagado == 0)
                {
                    valorPagado = valorNotaPedido;
                }

                if (valorPagado < valorNotaPedido)
                {
                    formaPago = 2;
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

                if (valorNotaPedidoReal < 0)
                {
                    valorNotaPedidoReal = 0;
                }

                abono = new ReceivableDto
                {
                    Id = 0,
                    Numero = 0,
                    Valor = valorCuota0,
                    ValorPagado = valorPagado,
                    Saldo = valorPagado - valorCuota0,
                    FechaVencimiento = fechaInicial
                };

                detallePagos.Add(abono);
            }

            decimal valorCuotaEstimada = (numeroPagos > 0) ? Math.Round(valorNotaPedidoReal / numeroPagos, 2) : valorNotaPedidoReal;

            //decimal valorPagadoReal = 0;

            for (int i = 1; valorCuotaEstimada > 0 && i <= numeroPagos; i++)
            {
                fechaInicial = fechaInicial.AddDays(plazoDias);

                detallePagos.Add(new ReceivableDto { Id = i, Numero = i, Valor = valorCuotaEstimada, ValorPagado = 0, Saldo = valorCuotaEstimada, FechaVencimiento = fechaInicial });
            }
             
            return detallePagos;
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
            //retornar para poder asignar a los identificadores del cliente en el JS
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }

        public class posSaveModel
        {
            public string ruc { get; set; }
            public string fecha { get; set; }
            public string hora { get; set; }
            public string observaciones { get; set; }
            public int formapago { get; set; }
            public int formaPagoSri { get; set; }
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
            public decimal Descuento { get; set; }
            public decimal ValorDescuento { get; set; }

            public List<CuentasPorCobrarViewModel> CuentasPorCobrar { get; set; }
            public List<AditionalInfoDto> InformacionAdicional { get; set; }
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
                if(stock == null || (stock.TipoProducto =="PRODUCTO" && stock.Stock <= (stock.StockMinimo ?? 0)))
                {
                    return "stock";
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
                    Precio = precio,
                    PrecioSugerido = precio,
                    CostoVenta = stock.Costo,
                    IncluyeIva = tipounidad.IncluyeIva ?? false,
                    CobraIva = stock.CobraIva ?? false,
                    Rise = PedidoActual.Rise, 
                    Descuento = stock.Descuento ?? 0 ,
                    IdTarifaImpuesto = stock.IdTarifaImpuesto,
                    PorcentajeTarifaImpuesto = stock.porcentajeTarifaImpuesto
                };

                PedidoActual.DetalleNotaPedido.Add(detalle);
            }
            else
            {
                var stock = ProductosAppService.ObtenerStockProducto(id, IdLocalSelected);

                var newQty = detalle.Cantidad + 1;

                if (stock == null || (stock.TipoProducto == "PRODUCTO" && (stock.Stock - newQty) <= (stock.StockMinimo ?? 0)))
                {
                    return "stock";
                }

                detalle.Cantidad = newQty;
            }

            return "ok";
        }
    }



}