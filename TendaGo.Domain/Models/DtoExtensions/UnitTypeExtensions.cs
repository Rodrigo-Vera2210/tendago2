using AutoMapper;
using ER.BE;
using TendaGo.Common;

namespace TendaGo.Domain
{
    internal static class UnitTypeExtensions
    {
        internal static UnitTypeDto ToUnitTypeDto(this TipoUnidadEntity entity)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<TipoUnidadEntity, UnitTypeDto>());
            var mapper = conf.CreateMapper();
            var dto = mapper.Map<UnitTypeDto>(entity);

            dto.CobraIva = entity.IdProductoAsProducto?.CobraIva;
            dto.Producto = entity.IdProductoAsProducto?.Producto;
            dto.UnidadMedida = entity.UnidadMedidadAsUnidadMedida?.UnidadMedida;
            return dto;
        }

        internal static TipoUnidadEntity ToUnitTypeEntity(this UnitTypeDto dto)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<UnitTypeDto, TipoUnidadEntity>());
            var mapper = conf.CreateMapper();
            var entity = mapper.Map<TipoUnidadEntity>(dto);
            return entity;
        }

    }
}