using AutoMapper;
using TendaGo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using TendaGo.Web.ClienteService;
//using TendaGo.Web.NotaPedidoService;
//using TendaGo.Web.Infraestructura;
//using TendaGo.Web.ProveedorService;
//using TendaGo.Web.CompraService;
using TendaGo.Common;

namespace TendaGo.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {

                config.CreateMap<ReceiptSummaryDto, DetalleRecibosViewModel>();
                config.CreateMap<DetalleRecibosViewModel, ReceiptSummaryDto>();

                config.CreateMap<DetalleRecibosViewModel, CashBalanceDetailRequestModel>();
                
                //x.CreateMap<TendaGo.Web.UnidadMedidaService.UnidadMedidaDto, UnidadMedidaViewModel>();
                //x.CreateMap<TendaGo.Web.ProductoService.ProductoDto, ProductoViewModel>();
                config.CreateMap<ProductDto, ProductoViewModel>();
                //.ForMember(d => d.FotoDataUrl, opts => opts.MapFrom(s => Tools.ConvertirByteArrayAImagen(s.Foto)));
                config.CreateMap<TendaGo.Common.LiteProductDto, ProductoViewModel>();
                //config.CreateMap<ClienteService.ClienteDto, ClienteViewModel>();
                //config.CreateMap<TendaGo.Web.ProveedorService.ProveedorDto, ProveedorViewModel>();

                config.CreateMap<UserDto, Usuario>()
                        .ForMember(model => model.UserName, opts => opts.MapFrom(x => x.InicioSesion))
                        .ForMember(model => model.Nombre, opts => opts.MapFrom(x => x.Nombres))
                        .ForMember(model => model.IdLocal, opts => opts.MapFrom(x => x.DefaultLocal))
                        .ForMember(model => model.Estado, opts => opts.MapFrom(x => x.IdEstado));

                config.CreateMap<Usuario, UserDto>()
                        .ForMember(model => model.InicioSesion, opts => opts.MapFrom(x => x.UserName))
                        .ForMember(model => model.Nombres, opts => opts.MapFrom(x => x.Nombre))
                        .ForMember(model => model.IdEstado, opts => opts.MapFrom(x => x.Estado));

                config.CreateMap<ProviderDto, ProveedorViewModel>();
                config.CreateMap<ProveedorViewModel, ProviderDto>();

                config.CreateMap<ClientDto, ClienteViewModel>();
                config.CreateMap<ClienteViewModel, ClientDto>();

                config.CreateMap<TendaGo.Common.SectorDto, SectorViewModel>();
                config.CreateMap<TendaGo.Common.CountryDto, PaisViewModel>();
                config.CreateMap<TendaGo.Common.ProvinceDto, ProvinciaViewModel>();
                config.CreateMap<TendaGo.Common.CityDto, CiudadViewModel>();
                
                config.CreateMap<TendaGo.Common.EntityDto, EntidadViewModel>()
                            .ForMember(model => model.Pais, opts => opts.MapFrom(x => new PaisViewModel { Id = x.IdPais.Value, Nombre = x.Pais }))
                            .ForMember(model => model.Provincia, opts => opts.MapFrom(x => new ProvinciaViewModel { Id = x.IdProvincia.Value, Nombre = x.Provincia }))
                            .ForMember(model => model.Ciudad, opts => opts.MapFrom(x => new CiudadViewModel { Id = x.IdCiudad.Value, Nombre = x.Ciudad }))
                            .ForMember(model => model.Foto, opts => opts.Ignore());

                config.CreateMap<TendaGo.Common.EntityDto, ClienteViewModel>()
                            .ForMember(model => model.Pais, opts => opts.MapFrom(x => new PaisViewModel { Id = x.IdPais.Value, Nombre = x.Pais }))
                            .ForMember(model => model.Provincia, opts => opts.MapFrom(x => new ProvinciaViewModel { Id = x.IdProvincia.Value, Nombre = x.Provincia }))
                            .ForMember(model => model.Ciudad, opts => opts.MapFrom(x => new CiudadViewModel { Id = x.IdCiudad.Value, Nombre = x.Ciudad }))
                            .ForMember(model => model.Foto, opts => opts.Ignore());

                config.CreateMap<TendaGo.Common.EntityDto, ProveedorViewModel>()
                            .ForMember(model => model.Pais, opts => opts.MapFrom(x => new PaisViewModel { Id=x.IdPais.Value, Nombre = x.Pais }))
                            .ForMember(model => model.Provincia, opts => opts.MapFrom(x => new ProvinciaViewModel { Id = x.IdProvincia.Value, Nombre = x.Provincia }))
                            .ForMember(model => model.Ciudad, opts => opts.MapFrom(x => new CiudadViewModel { Id = x.IdCiudad.Value, Nombre = x.Ciudad }))
                            .ForMember(model => model.Foto, opts => opts.Ignore());
                
                config.CreateMap<EntidadViewModel, TendaGo.Common.EntityDto>();
                config.CreateMap<ClienteViewModel, TendaGo.Common.EntityDto>();
                config.CreateMap<ProveedorViewModel, TendaGo.Common.EntityDto>();

                config.CreateMap<EntidadViewModel, ClienteViewModel>();
                config.CreateMap<EntidadViewModel, ProveedorViewModel>();

                //x.CreateMap<TendaGo.Web.TipoUnidadService.TipoUnidadDto, TipoUnidadViewModel>();
                //x.CreateMap<TendaGo.Web.PrecioService.PrecioDto, PrecioViewModel>();

                //config.CreateMap<DetalleNotaPedidoDto, DetalleNotaPedidoViewModel>()
                //    .ForMember(d => d.NombreProducto, opts => opts.MapFrom(s => (s.Producto != null ? s.Producto.Nombre : string.Empty)))
                //    .ForMember(d => d.DescripcionProducto, opts => opts.MapFrom(s => (s.Producto != null ? s.Producto.Descripcion : string.Empty)));

                //config.CreateMap<EmpaquetadoDto, EmpaquetadoViewModel>();

                //config.CreateMap<NotaPedidoDto, NotaPedidoModel>()
                //    .ForMember(d => d.Cliente, opts => opts.MapFrom(s => s.Cliente))
                //    .ForMember(d => d.RazonSocial, opts => opts.MapFrom(s => (s.Cliente != null ? s.Cliente.RazonSocial : string.Empty)))
                //    .ForMember(d => d.DetalleNotaPedido, opts => opts.MapFrom(s => s.DetalleNotaPedido));

                //config.CreateMap<NotaPedidoDto, NotaPedidoViewModel>()
                //    .ForMember(d => d.Cliente, opts => opts.MapFrom(s => s.Cliente))
                //    .ForMember(d => d.DetalleNotaPedido, opts => opts.MapFrom(s => s.DetalleNotaPedido));


                config.CreateMap<OutputDto, NotaPedidoViewModel>()
                    .ForMember(d => d.Cliente, opts => opts.MapFrom(s => s.Cliente))
                    .ForMember(d => d.DetalleNotaPedido, opts => opts.MapFrom(s => s.DetalleNotaPedido));


                config.CreateMap<OutputDto, NotaPedidoModel>()
                    .ForMember(d => d.RazonSocial, opts => opts.MapFrom(s => (s.Cliente != null ? s.Cliente.RazonSocial : string.Empty)))
                    .ForMember(d => d.Cliente, opts => opts.MapFrom(s => s.Cliente))
                    .ForMember(d => d.DetalleNotaPedido, opts => opts.MapFrom(s => s.DetalleNotaPedido))
                    .ForMember(d => d.DetalleEmpaquetado, opts => opts.MapFrom(s => s.DetalleEmpaquetado))
                    .ForMember(d => d.CuentasPorCobrar, opts => opts.MapFrom(s => s.CuentasPorCobrar));

                //config.CreateMap<NotaPedidoDto, AprobacionNotaPedidoViewModel>()
                //    .ForMember(d => d.RazonSocial, opts => opts.MapFrom(s => (s.Cliente != null ? s.Cliente.RazonSocial : string.Empty)))
                //    .ForMember(d => d.DetalleNotaPedido, opts => opts.MapFrom(s => s.DetalleNotaPedido));

                //config.CreateMap<NotaPedidoDto, EmpaquetadoNotaPedidoViewModel>()
                //    .ForMember(d => d.RazonSocial, opts => opts.MapFrom(s => (s.Cliente != null ? s.Cliente.RazonSocial : string.Empty)))
                //    .ForMember(d => d.DetalleNotaPedido, opts => opts.MapFrom(s => s.DetalleNotaPedido));

                //config.CreateMap<NotaPedidoDto, EntregaNotaPedidoViewModel>()
                //    .ForMember(d => d.RazonSocial, opts => opts.MapFrom(s => (s.Cliente != null ? s.Cliente.RazonSocial : string.Empty)));

                //config.CreateMap<CuentaPorCobrarDto, CuentasPorCobrarViewModel>()
                //    .ForMember(d => d.ValorPendiente, opts => opts.MapFrom(s => s.Saldo))
                //    .ForMember(d => d.ValorPagado, opts => opts.MapFrom(s => s.Valor - s.Saldo));

                config.CreateMap<ReceivableDto, CuentasPorCobrarViewModel>()
                    .ForMember(d => d.ValorPendiente, opts => opts.MapFrom(s => s.Saldo))
                    .ForMember(d => d.ValorPagado, opts => opts.MapFrom(s => s.Valor - s.Saldo));

                config.CreateMap<DocumentDto, DocumentViewModel>();
                config.CreateMap<DocumentViewModel, DocumentDto>();

                config.CreateMap<OutputDetailDto, DetalleNotaPedidoViewModel>();
                config.CreateMap<SalesOrderDetailDto, DetalleNotaPedidoViewModel>();

                config.CreateMap<SalesOrderInvoiceDto, FacturaNotaPedidoViewModel>().ForMember(m => m.Factura, opts => opts.Ignore());
                config.CreateMap<FacturaNotaPedidoViewModel, SalesOrderInvoiceDto>().ForMember(m => m.Factura, opts => opts.Ignore());



                config.CreateMap<PackingDto, EmpaquetadoViewModel>()
                    .ForMember(m => m.TipoPaquete, opts => opts.MapFrom(s => s.TipoPaquete.TipoPaquete));

                //config.CreateMap<ClienteViewModel, DatosClienteDto>();

                config.CreateMap<ClienteViewModel, ClientDto>();
                config.CreateMap<ClientDto, ClienteViewModel>()
                    .ForMember(m => m.Ciudad, opts => opts.Ignore())
                    .ForMember(m => m.Provincia, opts => opts.Ignore())
                    .ForMember(m => m.Pais, opts => opts.Ignore())
                    .ForMember(m => m.Foto, opts => opts.Ignore());

                //config.CreateMap<ProveedorViewModel, DatosProveedorDto>();
                //config.CreateMap<DetalleNotaPedidoViewModel, DetalleNotaPedidoDto>();
                config.CreateMap<EmpaquetadoViewModel, PackingDto>();


                //config.CreateMap<NotaPedidoViewModel, DatosNotaPedidoDto>();
                config.CreateMap<NotaPedidoViewModel, OutputDto>();
                config.CreateMap<PedidoViewModel, OutputDto>();

                config.CreateMap<DetalleNotaPedidoViewModel, OutputDetailDto>();

                config.CreateMap<DetalleNotaPedidoViewModel, SalesOrderDetailDto>();

                config.CreateMap<CuentasPorCobrarViewModel, ReceivableDto>();

                //config.CreateMap<CuentasPorCobrarViewModel, CuentaPorCobrarDto>();
                //config.CreateMap<CuentasPorPagarViewModel, CuentaPorPagarDto>();
                //config.CreateMap<AprobacionNotaPedidoViewModel, DatosAprobacionNotaPedidoDto>();
                //config.CreateMap<EmpaquetadoViewModel, EmpaquetadoDto>();
                //config.CreateMap<EmpaquetadoNotaPedidoViewModel, DatosEmpaquetadoNotaPedidoDto>();
                //config.CreateMap<RevisadoNotaPedidoViewModel, DatosRevisadoNotaPedidoDto>();
                //config.CreateMap<EntregaNotaPedidoViewModel, DatosEntregaNotaPedidoDto>();

                config.CreateMap<EmpaquetadoViewModel, PackingDto>();

                config.CreateMap<AprobacionNotaPedidoViewModel, SalesOrderApprovalDto>()
                    .ForMember(model => model.Detalles, member => member.MapFrom(s => s.DetalleNotaPedido));

                config.CreateMap<EmpaquetadoNotaPedidoViewModel, SalesOrderPackingDto>();

                config.CreateMap<RevisadoNotaPedidoViewModel, SalesOrderReviewDto>()
                    .ForMember(model => model.Detalles, member => member.MapFrom(s => s.DetalleNotaPedido));

                config.CreateMap<EntregaNotaPedidoViewModel, SalesOrderDeliverDto>()
                    .ForMember(model => model.Detalles, member => member.MapFrom(s => s.DetalleNotaPedido));


                config.CreateMap<TransferenciaViewModel, TransferRequest>();
                config.CreateMap<DetalleTransferenciaViewModel, TransferDetailDto>();

                config.CreateMap<OutputDetailDto, TransferDetailDto>();
                config.CreateMap<InputDetailDto, TransferDetailDto>();

                config.CreateMap<TransferDetailDto, OutputDetailDto>();
                config.CreateMap<TransferDetailDto, InputDetailDto>();


                config.CreateMap<TransferRequest, TransferDto>();
                config.CreateMap<TransferDto, TransferRequest>();

                config.CreateMap<TransferDto, OutputDto>();
                config.CreateMap<TransferDto, InputDto>();

                config.CreateMap<OutputDto, TransferDto>()
                    .ForMember(x => x.Id, opts => opts.MapFrom(p => p.TransaccionPadre))
                    .ForMember(x => x.TipoTransaccion, opts => opts.MapFrom(p => p.TipoTransaccionPadre))
                    .ForMember(x => x.IdLocalOrigen, opts => opts.MapFrom(p => p.IdLocal))
                    .ForMember(x => x.Salida, opts => opts.MapFrom(p => p))
                    .ForMember(x => x.Detalle, opts => opts.MapFrom(p => p.DetalleNotaPedido));

                config.CreateMap<InputDto, TransferDto>()
                    .ForMember(x => x.Id, opts => opts.MapFrom(p => p.TransaccionPadre))
                    .ForMember(x => x.TipoTransaccion, opts => opts.MapFrom(p => p.TipoTransaccionPadre))
                    .ForMember(x => x.IdLocalDestino, opts => opts.MapFrom(p => p.IdLocal))
                    .ForMember(x => x.Entrada, opts => opts.MapFrom(p => p))
                    .ForMember(x => x.Detalle, opts => opts.MapFrom(p => p.DetalleEntradaFromIdEntrada));

                //config.CreateMap<ProductoViewModel, DatosProductoDto>();
                config.CreateMap<LiteStockDto, ExistenciaProductoViewModel>()
                    .ForMember(model => model.NombreProducto, member => member.MapFrom(s => s.Producto))
                    .ForMember(model => model.DescripcionBodega, member => member.MapFrom(s => s.Descripcion))
                    .ForMember(model => model.IdBodega, member => member.MapFrom(s => s.IdLocal))
                    .ForMember(model => model.StockDisponible, member => member.MapFrom(s => s.StockInventario));

                config.CreateMap<ProductoViewModel, ProductDto>();
                config.CreateMap<ProductoViewModel, LiteProductDto>();

                config.CreateMap<DebtDto, CuentasPorPagarViewModel>()
                    .ForMember(model => model.FechaVencimiento, member => member.MapFrom(s => s.Fecha));

                config.CreateMap<InputDetailDto, DetalleEntradaViewModel>()
                    .ForMember(model => model.Precio, member => member.MapFrom(s => s.FactorDistribucion));

                config.CreateMap<InputDto, EntradaViewModel>()
                    .ForMember(model => model.DetalleNotaPedido, member => member.MapFrom(s => s.DetalleEntradaFromIdEntrada))
                    .ForMember(model => model.CuentasPorPagar, member => member.MapFrom(s => s.CuentaPorPagarFromIdEntrada));

                //config.CreateMap<PrecioViewModel, DatosPrecioDto>();
                //config.CreateMap<DetalleCompraViewModel, DetalleCompraDto>();
                //config.CreateMap<CompraViewModel, DatosCompraDto>();
                //config.CreateMap<CuentasPorPagarViewModel, CuentaPorPagarDto>();

                config.CreateMap<DetalleCompraViewModel, InputDetailDto>()
                    .ForMember(model => model.FechaExpiracion, member => member.MapFrom(s => s.FechaCaducidad));
                config.CreateMap<CuentasPorPagarViewModel, DebtDto>();

                config.CreateMap<CompraViewModel, InputDto>()
                    .ForMember(model => model.DetalleEntradaFromIdEntrada, member => member.MapFrom(s => s.DetalleCompra))
                    .ForMember(model => model.CuentaPorPagarFromIdEntrada, member => member.MapFrom(s => s.CuentasPorPagar));

                config.CreateMap<EntradaViewModel, InputDto>()
                    .ForMember(model => model.DetalleEntradaFromIdEntrada, member => member.MapFrom(s => s.DetalleNotaPedido))
                    .ForMember(model => model.CuentaPorPagarFromIdEntrada, member => member.MapFrom(s => s.CuentasPorPagar));


                config.CreateMap<CierreCajaViewModel, CashBalanceRequestModel>();

            });
        }
    }
}