using AutoMapper;
using ER.BE;
namespace TendaGo.Common
{
    public static class EntityExtension
    {
        internal static EntityDto ToEntityDto(this EntidadEntity entidad)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<EntidadEntity, EntityDto>());
            var mapper = conf.CreateMapper();
            var entidadDto = mapper.Map<EntityDto>(entidad);
            //entidadDto.Pais = entidad.IdPaisAsPais?.Pais;
            entidadDto.Ciudad = entidad.IdCiudadAsCiudad?.Ciudad;
            entidadDto.Provincia = entidad.IdProvinciaAsProvincia?.Provincia;
            entidadDto.Sector = entidad.IdSectorAsSector?.Sector;
            return entidadDto;
        }

        internal static EntidadEntity ToEntidadEntity(this EntityDto entityDto)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<EntityDto, EntidadEntity>());
            var mapper = conf.CreateMapper();
            var entidadEntity = mapper.Map<EntidadEntity>(entityDto);
            return entidadEntity;
        }
    }
}
