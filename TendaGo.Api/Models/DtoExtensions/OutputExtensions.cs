//using AutoMapper;
//using ER.BA;
//using ER.BE;
//using System.Data.SqlClient;
//using System.Linq;
//using TendaGo.Domain.Services;

//namespace TendaGo.Common
//{
//    internal static class OutputExtensions
//    {
//        //private readonly IEmpresaService empresaService;

//        //public OutputExtensions(IEmpresaService empresaService)
//        //{
//        //    this.empresaService = empresaService;
//        //}

//        internal static readonly MapperConfiguration MapperConfiguration = new MapperConfiguration(config =>
//        {
//            config.CreateMap<SalidaEntity, SalesOrderApprovalDto>();
//            config.CreateMap<SalesOrderApprovalDto, SalidaEntity>();

//            config.CreateMap<SalidaEntity, SalesOrderPackingDto>();
//            config.CreateMap<SalesOrderPackingDto, SalidaEntity>();

//            config.CreateMap<SalidaEntity, SalesOrderReviewDto>();
//            config.CreateMap<SalesOrderReviewDto, SalidaEntity>();

//            config.CreateMap<SalidaEntity, SalesOrderDeliverDto>();
//            config.CreateMap<SalesOrderDeliverDto, SalidaEntity>();

//            config.CreateMap<ReceivableDto, CuentaPorCobrarEntity>();

//            config.CreateMap<SerieSalidaDto, SerieSalidaEntity>();
//            config.CreateMap<SerieSalidaEntity, SerieSalidaDto>();

//            config.CreateMap<EntidadEntity, ClientDto>();
//            config.CreateMap<CuentaPorCobrarEntity, ReceivableDto>();
//            config.CreateMap<DetalleSalidaEntity, OutputDetailDto>()
//                .ForMember(m => m.NombreProducto, opt => opt.MapFrom(x => x.IdProductoAsProducto.Producto))
//                .ForMember(m => m.CobraIva, opt => opt.MapFrom(x => x.IdProductoAsProducto.CobraIva))
//                .ForMember(m => m.CodigoInterno, opt => opt.MapFrom(x => x.IdProductoAsProducto.CodigoInterno))
//                .ForMember(m => m.DesTipoUnidad, opt => opt.MapFrom(x => x.IdTipoUnidadAsTipoUnidad.TipoUnidad))
//                .ForMember(m => m.Serie, opt => opt.MapFrom(x => x.SerieSalidaFromIdSalida));

//            config.CreateMap<TipoPaqueteEntity, PackageTypeDto>();

//            config.CreateMap<PackingDto, EmpaquetadoEntity>();

//            config.CreateMap<OutputDetailDto, DetalleSalidaEntity>()
//                .ForMember(m => m.SerieSalidaFromIdSalida, opt => opt.MapFrom(x => x.Serie));

//            config.CreateMap<InputDetailDto, DetalleEntradaEntity>();

//            config.CreateMap<OutputRequestModel, SalidaEntity>()
//                 .ForMember(m => m.CuentaPorCobrarFromIdSalida, opt => opt.MapFrom(x => x.CuentasPorCobrar))
//                 .ForMember(m => m.DetalleSalidaFromIdSalida, opt => opt.MapFrom(x => x.DetalleNotaPedido))
//                 .ForMember(m => m.EmpaquetadoFromIdSalida, opt => opt.MapFrom(x => x.DetalleEmpaquetado));

//            config.CreateMap<OutputCreateRequestModel, SalidaEntity>()
//                 .ForMember(m => m.CuentaPorCobrarFromIdSalida, opt => opt.MapFrom(x => x.CuentasPorCobrar))
//                 .ForMember(m => m.DetalleSalidaFromIdSalida, opt => opt.MapFrom(x => x.DetalleNotaPedido));

//            config.CreateMap<OutputDto, SalidaEntity>()
//                 .ForMember(m => m.CuentaPorCobrarFromIdSalida, opt => opt.MapFrom(x => x.CuentasPorCobrar))
//                 .ForMember(m => m.DetalleSalidaFromIdSalida, opt => opt.MapFrom(x => x.DetalleNotaPedido))
//                 .ForMember(m => m.EmpaquetadoFromIdSalida, opt => opt.MapFrom(x => x.DetalleEmpaquetado));

//            config.CreateMap<EmpaquetadoEntity, PackingDto>()
//                .ForMember(m => m.TipoPaquete, opts => opts.MapFrom(s => s.IdTipoPaqueteAsTipoPaquete));

//            config.CreateMap<SalidaEntity, OutputDto>()
//                .ForMember(m => m.Cliente, opts => opts.MapFrom(s => s.IdClienteAsEntidad))
//                .ForMember(m => m.DetalleNotaPedido, opts => opts.MapFrom(s => s.DetalleSalidaFromIdSalida))
//                .ForMember(m => m.CuentasPorCobrar, opts => opts.MapFrom(s => s.CuentaPorCobrarFromIdSalida))
//                .ForMember(m => m.DetalleEmpaquetado, opts => opts.MapFrom(s => s.EmpaquetadoFromIdSalida));

//            config.CreateMap<EntradaEntity, InputDto>()
//                .ForMember(m => m.Proveedor, opts => opts.MapFrom(s => s.IdProveedorAsEntidad))
//                .ForMember(m => m.DetalleEntradaFromIdEntrada, opts => opts.MapFrom(s => s.DetalleEntradaFromIdEntrada))
//                .ForMember(m => m.CuentaPorPagarFromIdEntrada, opts => opts.MapFrom(s => s.CuentaPorPagarFromIdEntrada));

//            config.CreateMap<OutputDetailDto, TransferDetailDto>();
//            config.CreateMap<InputDetailDto, TransferDetailDto>();

//            config.CreateMap<OutputDetailDto, AdjustInventoryDetailDto>();
//            config.CreateMap<InputDetailDto, AdjustInventoryDetailDto>();

//            config.CreateMap<TransferDetailDto, OutputDetailDto>();
//            config.CreateMap<TransferDetailDto, InputDetailDto>();

//            config.CreateMap<AdjustInventoryDetailDto, OutputDetailDto>();
//            config.CreateMap<AdjustInventoryDetailDto, InputDetailDto>();

//            config.CreateMap<DetalleEntradaEntity, TransferDetailDto>();
//            config.CreateMap<DetalleSalidaEntity, TransferDetailDto>();

//            config.CreateMap<DetalleEntradaEntity, AdjustInventoryDetailDto>();
//            config.CreateMap<DetalleSalidaEntity, AdjustInventoryDetailDto>();

//            config.CreateMap<TransferDetailDto, DetalleSalidaEntity>();
//            config.CreateMap<TransferDetailDto, DetalleEntradaEntity>();

//            config.CreateMap<AdjustInventoryDetailDto, DetalleSalidaEntity>();
//            config.CreateMap<AdjustInventoryDetailDto, DetalleEntradaEntity>();


//            config.CreateMap<TransferRequest, TransferDto>();
//            config.CreateMap<TransferDto, TransferRequest>();

//            config.CreateMap<AdjustInventoryRequest, AdjustInventoryDto>();
//            config.CreateMap<AdjustInventoryDto, AdjustInventoryRequest>();

//            config.CreateMap<TransferDto, SalidaEntity>();
//            config.CreateMap<TransferDto, EntradaEntity>();

//            config.CreateMap<AdjustInventoryDto, SalidaEntity>();
//            config.CreateMap<AdjustInventoryDto, EntradaEntity>();

//            config.CreateMap<SalidaEntity, TransferDto>()
//                .ForMember(x => x.Id, opts => opts.MapFrom(p => p.TransaccionPadre))
//                .ForMember(x => x.TipoTransaccion, opts => opts.MapFrom(p => p.TipoTransaccionPadre))
//                .ForMember(x => x.Salida, opts => opts.MapFrom(p => p))
//                .ForMember(x => x.Detalle, opts => opts.MapFrom(p => p.DetalleSalidaFromIdSalida));
//            //nProveedor\r\nTipoTrasaccionPadre\r\n"}
//            config.CreateMap<EntradaEntity, TransferDto>()
//                .ForMember(x => x.Id, opts => opts.MapFrom(p => p.TransaccionPadre))
//                .ForMember(x => x.TipoTransaccion, opts => opts.MapFrom(p => p.TipoTransaccionPadre))
//                .ForMember(x => x.Entrada, opts => opts.MapFrom(p => p))
//                .ForMember(x => x.Detalle, opts => opts.MapFrom(p => p.DetalleEntradaFromIdEntrada));


//        });


//        internal static OutputDto ToOutputLiteDto(this SalidaEntity entity)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            var dto = mapper.Map<OutputDto>(entity);

//            return dto;
//        }

//        internal static OutputDto ToOutputDto(this SalidaEntity entity)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            var dto = mapper.Map<OutputDto>(entity);

//            if (entity.DocumentoFromIdSalida != null && entity.DocumentoFromIdSalida.Count > 0)
//                dto.Factura = entity.DocumentoFromIdSalida?
//                    .FirstOrDefault()?
//                    .ToDocumentDto();

//            return dto;
//        }

//        internal static TransferDetailDto ToTransferDetailDto(this DetalleEntradaEntity entity)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            var dto = mapper.Map<TransferDetailDto>(entity);
//            return dto;
//        }


//        internal static TransferDto ToTransferDto(this EntradaEntity entity)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            var dto = mapper.Map<TransferDto>(entity);

//            return dto;
//        }


//        internal static TransferDto ToTransferDto(this SalidaEntity entity)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            var dto = mapper.Map<TransferDto>(entity);

//            return dto;
//        }

//        internal static AdjustInventoryDto ToAdjustInventoryDto(this AdjustInventoryRequest request)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            var dto = mapper.Map<AdjustInventoryDto>(request);

//            return dto;
//        }


//        internal static TransferDto ToTransferDto(this TransferRequest request)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            var dto = mapper.Map<TransferDto>(request);

//            return dto;
//        }

//        internal static StatisticsDto ToStatisticsDto(this EstadisticaEntity entity)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            var dto = mapper.Map<StatisticsDto>(entity);

//            return dto;
//        }

//        internal static StatisticsMonthDto ToStatisticsMonthDto(this VentasMensualEntity entity)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            var dto = mapper.Map<StatisticsMonthDto>(entity);

//            return dto;
//        }

//        internal static TransferRequest ToTransferRequest(this TransferDto dto)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            var request = mapper.Map<TransferRequest>(dto);

//            return request;
//        }


//        static readonly MapperConfiguration EmpaquetadoConfiguration = new MapperConfiguration(config =>
//            {
//                config.CreateMap<PackingDto, EmpaquetadoEntity>();
//                config.CreateMap<EmpaquetadoEntity, PackingDto>();
//            });

//        internal static EmpaquetadoEntity UpdateEntity(this EmpaquetadoEntity output, PackingDto request)
//        {
//            var mapper = EmpaquetadoConfiguration.CreateMapper();
//            return mapper.Map(request, output);
//        }
//        internal static PackingDto ToPackingDto(this EmpaquetadoEntity entity)
//        {
//            var mapper = EmpaquetadoConfiguration.CreateMapper();
//            var dto = mapper.Map<PackingDto>(entity);
//            return dto;
//        }

//        internal static EmpaquetadoEntity ToEmpaquetadoEntity(this PackingDto dto)
//        {
//            var mapper = EmpaquetadoConfiguration.CreateMapper();
//            var entity = mapper.Map<EmpaquetadoEntity>(dto);

//            return entity;
//        }


//        static readonly MapperConfiguration CuentaPorCobrarConfiguration = new MapperConfiguration(config =>
//        {
//            config.CreateMap<ReceivableDto, CuentaPorCobrarEntity>();
//            config.CreateMap<CuentaPorCobrarEntity, ReceivableDto>();
//        });

//        internal static CuentaPorCobrarEntity UpdateEntity(this CuentaPorCobrarEntity output, ReceivableDto request)
//        {
//            var mapper = CuentaPorCobrarConfiguration.CreateMapper();
//            return mapper.Map(request, output);
//        }


//        static readonly MapperConfiguration DetalleSalidaConfiguration = new MapperConfiguration(config =>
//        {
//            config.CreateMap<SalesOrderDetailDto, DetalleSalidaEntity>()
//                .ForMember(m => m.IdProveedor, x => x.Ignore())
//                .ForMember(m => m.CostoUnitarioImportacion, x => x.Ignore())
//                .ForMember(m => m.CostoVenta, x => x.Ignore());

//            config.CreateMap<DetalleSalidaEntity, SalesOrderDetailDto>();

//        });

//        internal static DetalleSalidaEntity UpdateEntity(this DetalleSalidaEntity output, SalesOrderDetailDto request)
//        {
//            var mapper = DetalleSalidaConfiguration.CreateMapper();
//            return mapper.Map(request, output);
//        }

//        internal static DetalleSalidaEntity ToDetalleSalidaEntity(this SalesOrderDetailDto dto)
//        {
//            var mapper = DetalleSalidaConfiguration.CreateMapper();
//            var entity = mapper.Map<DetalleSalidaEntity>(dto);

//            return entity;
//        }

//        internal static SalesOrderDetailDto ToDetailDto(this DetalleSalidaEntity entity)
//        {
//            var mapper = DetalleSalidaConfiguration.CreateMapper();
//            var dto = mapper.Map<SalesOrderDetailDto>(entity);
//            return dto;
//        }


//        internal static DetalleSalidaEntity ToDetalleSalidaEntity(this OutputDetailDto dto)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            var entity = mapper.Map<DetalleSalidaEntity>(dto);

//            return entity;
//        }

//        internal static DetalleSalidaEntity ToDetalleSalidaEntity(this TransferDetailDto dto)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            var entity = mapper.Map<DetalleSalidaEntity>(dto);

//            return entity;
//        }

//        internal static TransferDetailDto ToTransferDetailDto(this DetalleSalidaEntity entity)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            var dto = mapper.Map<TransferDetailDto>(entity);
//            return dto;
//        }


//        internal static SalidaEntity ToSalidaEntity(this OutputCreateRequestModel dto)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            var entity = mapper.Map<SalidaEntity>(dto);

//            if (dto.Factura != null && dto.Factura.Id == null)
//            {
//                entity.DocumentoFromIdSalida = new DocumentoEntityCollection
//                {
//                    dto.Factura.ToDocumentoEntity()
//                };
//            }

//            return entity;
//        }
//        internal static SalidaEntity ToSalidaEntity(this OutputRequestModel dto)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            var entity = mapper.Map<SalidaEntity>(dto);

//            if (dto.Factura != null && dto.Factura.Id == null)
//            {
//                entity.DocumentoFromIdSalida = new DocumentoEntityCollection
//                {
//                    dto.Factura.ToDocumentoEntity()
//                };
//            }

//            return entity;
//        }

//        internal static SalidaEntity ToSalidaEntity(this OutputDto dto)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            var entity = mapper.Map<SalidaEntity>(dto);

//            if (dto.Factura != null && dto.Factura.Id == null)
//            {
//                entity.DocumentoFromIdSalida = new DocumentoEntityCollection
//                {
//                    dto.Factura.ToDocumentoEntity()
//                };
//            }

//            return entity;
//        }

//        internal static DocumentoEntityCollection GenerarFacturas(this DocumentoEntityCollection documentos)
//        {
//            if (documentos != null)
//            {
//                var documento = documentos.FirstOrDefault();
//                //var facturaGo = EmpresaBussinesAction.LoadByPK(documento.IdEmpresa);

//                //if (documento.IdEmpresa > 0 && facturaGo.FacturaGo)
//                //    documento.GenerateInvoiceGo();//Demas empresas que tienen facturaGo
//                //else
//                //    documento.GenerateInvoice();//Carmita y Agrocart

//                if (documento.IdEmpresa > 0)
//                    documento.GenerateInvoiceGo();//Demas empresas que tienen facturaGo
//                else
//                    documento.GenerateInvoice();//Carmita y Agrocart
//            }
//            return documentos;
//        }

//        internal static DocumentoEntityCollection GenerarNotasCreditos(this DocumentoEntityCollection documentos)
//        {
//            if (documentos != null)
//            {
//                foreach (var documento in documentos)
//                {
//                    //var facturaGo = EmpresaBussinesAction.LoadByPK(documento.IdEmpresa);
//                    //if (documento.IdEmpresa > 0 && facturaGo.FacturaGo)
//                    //    documento.GenerateCreditGo();

//                    if (documento.IdEmpresa > 0)
//                        documento.GenerateCreditGo();
//                }
//            }
//            return documentos;
//        }

//        internal static SalidaEntity UpdateEntity(this SalidaEntity output, SalesOrderApprovalDto request)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            return mapper.Map(request, output);
//        }

//        internal static SalidaEntity UpdateEntity(this SalidaEntity output, SalesOrderPackingDto request)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            return mapper.Map(request, output);
//        }


//        internal static SalidaEntity UpdateEntity(this SalidaEntity output, SalesOrderReviewDto request)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            return mapper.Map(request, output);
//        }

//        internal static SalidaEntity UpdateEntity(this SalidaEntity output, SalesOrderDeliverDto request)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            return mapper.Map(request, output);
//        }




//        internal static OutputDto ToOutputDto(this SalesOrderApprovalDto request)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            var output = mapper.Map<OutputDto>(request);

//            return output;
//        }

//        internal static OutputDto ToOutputDto(this SalesOrderPackingDto request)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            var output = mapper.Map<OutputDto>(request);

//            return output;
//        }

//        internal static OutputDto ToOutputDto(this SalesOrderReviewDto request)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            var output = mapper.Map<OutputDto>(request);

//            return output;
//        }

//        internal static OutputDto ToOutputDto(this SalesOrderDeliverDto request)
//        {
//            var mapper = MapperConfiguration.CreateMapper();
//            var output = mapper.Map<OutputDto>(request);

//            return output;
//        }


//        public static string GetCode(this TransactionType tipo)
//        {
//            return GetCode((TransactionType?)tipo);
//        }

//        public static string GetCode(this TransactionType? tipo, bool salida = true)
//        {
//            switch (tipo)
//            {
//                case TransactionType.NotaPedido:
//                    return "NP";
//                case TransactionType.Compra:
//                    return "CP";
//                case TransactionType.SalidaBodega:
//                    return "GE";
//                case TransactionType.EntradaBodega:
//                    return "GR";
//                case TransactionType.Cotizacion:
//                    return "CT";
//            }

//            return default;
//        }

//        public static string GetStatus(this TransactionStatus? value)
//        {
//            switch (value)
//            {
//                case TransactionStatus.EnProceso:
//                    return "EN PROCESO";
//                case TransactionStatus.Aprobada:
//                    return "APROBADA";
//                case TransactionStatus.Revisada:
//                    return "REVISADA";
//                case TransactionStatus.Empaquetada:
//                    return "EMPAQUETADA";
//                case TransactionStatus.Entregada:
//                    return "ENTREGADA";
//                case TransactionStatus.Anulada:
//                    return "ANULADA";
//                case TransactionStatus.Facturada:
//                    return "FACTURADA";
//                case TransactionStatus.Cotizacion:
//                    return "COTIZACION";
//            }

//            return default;
//        }


//    }
//}