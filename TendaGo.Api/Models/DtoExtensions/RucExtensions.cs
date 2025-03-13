using AutoMapper;
using ER.BE;

namespace TendaGo.Common
{
    internal static class WarehouseExtensions
    {
        internal static WarehouseDto ToWarehouseDto(this LocalBodegaEntity entity)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<LocalBodegaEntity, WarehouseDto>());
            var mapper = conf.CreateMapper();
            var dto = mapper.Map<WarehouseDto>(entity);
            return dto;
        }

        internal static LocalBodegaEntity ToLocalBodegaEntity(this WarehouseDto dto)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<WarehouseDto, LocalBodegaEntity>());
            var mapper = conf.CreateMapper();
            var entity = mapper.Map<LocalBodegaEntity>(dto);
            return entity;
        }
    }
}