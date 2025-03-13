using AutoMapper;
using ER.BE;

namespace TendaGo.Common
{
    internal static class MeasurementUnitExtensions
    {
        internal static MeasurementUnitDto ToMeasurementUnitDto(this UnidadMedidaEntity entity)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<UnidadMedidaEntity, MeasurementUnitDto>());
            var mapper = conf.CreateMapper();
            var dto = mapper.Map<MeasurementUnitDto>(entity);
            return dto;
        }

        internal static UnidadMedidaEntity ToUnidadMedidaEntity(this MeasurementUnitDto dto)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<MeasurementUnitDto, UnidadMedidaEntity>());
            var mapper = conf.CreateMapper();
            var entity = mapper.Map<UnidadMedidaEntity>(dto);
            return entity;
        }
    }
}