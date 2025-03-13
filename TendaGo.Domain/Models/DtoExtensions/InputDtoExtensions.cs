using AutoMapper;
using ER.BE;

namespace TendaGo.Domain
{
    public static class InputDtoExtensions
    {
        static MapperConfiguration DefaultMapper = new MapperConfiguration(config =>
        {
            config.CreateMap<DebtDto, CuentaPorPagarEntity>();
            config.CreateMap<InputDetailDto, DetalleEntradaEntity>()
                .ForMember(m => m.SerieEntradaFromIdSalida, opts => opts.MapFrom(s => s.Serie));


            config.CreateMap<EntityDto, EntidadEntity>();
            config.CreateMap<InputDto, EntradaEntity>();

            config.CreateMap<CuentaPorPagarEntity, DebtDto>();

            config.CreateMap<DetalleEntradaEntity, InputDetailDto>()
                .ForMember(m => m.Serie, opts => opts.MapFrom(s => s.SerieEntradaFromIdSalida));


            config.CreateMap<EntidadEntity, EntityDto>();
            config.CreateMap<EntradaEntity, InputDto>()
                .ForMember(m => m.Proveedor, opts => opts.MapFrom(s => s.IdProveedorAsEntidad));

            config.CreateMap<TransferDetailDto, DetalleEntradaEntity>();
            config.CreateMap<DetalleEntradaEntity, TransferDetailDto>();

            config.CreateMap<SerieEntradaDto, SerieEntradaEntity>();
            config.CreateMap<SerieEntradaEntity, SerieEntradaDto>();

        });


        internal static InputDto ToInputDto(this EntradaEntity entity)
        {
            var mapper = DefaultMapper.CreateMapper();
            var dto = mapper.Map<InputDto>(entity);
            return dto;
        }

        internal static DetalleEntradaEntity ToDetalleEntradaEntity(this InputDetailDto dto)
        {
            var mapper = DefaultMapper.CreateMapper();
            var entity = mapper.Map<DetalleEntradaEntity>(dto);
            return entity;
        }


        internal static DetalleEntradaEntity ToDetalleEntradaEntity(this TransferDetailDto dto)
        {
            var mapper = DefaultMapper.CreateMapper();
            var entity = mapper.Map<DetalleEntradaEntity>(dto);

            return entity;
        }

        static readonly MapperConfiguration DetalleEntradaConfiguration = new MapperConfiguration(config =>
        {
            config.CreateMap<InputDetailDto, DetalleEntradaEntity>()
                .ForMember(m => m.IdProveedor, x => x.Ignore())
                .ForMember(m => m.CostoUnitarioImportacion, x => x.Ignore())
                .ForMember(m => m.CostoVenta, x => x.Ignore());

            config.CreateMap<DetalleEntradaEntity, InputDetailDto>();

        });

        internal static DetalleEntradaEntity UpdateEntity(this DetalleEntradaEntity output, InputDetailDto request)
        {
            var mapper = DetalleEntradaConfiguration.CreateMapper();
            return mapper.Map(request, output);
        }

        internal static EntradaEntity ToEntradaEntity(this InputDto dto)
        {
            var mapper = DefaultMapper.CreateMapper();
            var entity = mapper.Map<EntradaEntity>(dto);

            //if (dto.DetalleEntradaFromIdEntrada.Any())
            //{
            //    entity.DetalleEntradaFromIdEntrada = new DetalleEntradaEntityCollection();
            //    foreach (var InputDetailDto in dto.DetalleEntradaFromIdEntrada)
            //    {
            //        entity.DetalleEntradaFromIdEntrada.Add(InputDetailDto.ToDetalleEntradaEntity());
            //    }
            //}
            //if (dto.CuentaPorPagarFromIdEntrada!=null)
            //{
            //    if (dto.CuentaPorPagarFromIdEntrada.Any())
            //    {
            //        entity.CuentaPorPagarFromIdEntrada = new CuentaPorPagarEntityCollection();
            //        foreach (var debtDto in dto.CuentaPorPagarFromIdEntrada)
            //        {
            //            entity.CuentaPorPagarFromIdEntrada.Add(debtDto.ToCuentaPorCobrarEntity());
            //        }
            //    }
            //}
            return entity;
        }
    }
}