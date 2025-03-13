using AutoMapper;
using ER.BE;
using TendaGo.Common;

namespace TendaGo.Domain
{
    internal static class ProviderExtensions
    {
        internal static ProviderDto ToProviderDto(this EntidadEntity entity)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<EntidadEntity, ProviderDto>());
            var mapper = conf.CreateMapper();
            var dto = mapper.Map<ProviderDto>(entity);
            dto.Ciudad = entity.IdCiudadAsCiudad?.Ciudad;
            dto.Provincia = entity.IdProvinciaAsProvincia?.Provincia;
            dto.Sector = entity.IdSectorAsSector?.Sector;
            return dto;
        }

        internal static EntidadEntity ToEntidadProveedorEntity(this ProviderDto dto)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<ProviderDto, EntidadEntity>());
            var mapper = conf.CreateMapper();
            var entity = mapper.Map<EntidadEntity>(dto);
            return entity;
        }
    }
}