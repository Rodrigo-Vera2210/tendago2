using AutoMapper;
using ER.BE;
using System.Collections.Generic;

namespace TendaGo.Domain
{
    internal static class ProfileExtensions
    {
        internal static ProfileDto ToProfileDTO(this PerfilEntity entity)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<PerfilEntity, ProfileDto>());
            var mapper = conf.CreateMapper();
            var dto = mapper.Map<ProfileDto>(entity);
            return dto;
        }

        internal static List<ProfileDto> ToProfileListDTO(this List<PerfilEntity> entities)
        {
            var conf = new MapperConfiguration(config =>
            {
                config.CreateMap<PerfilEntity, ProfileDto>();
                config.CreateMap<List<PerfilEntity>, List<ProfileDto>>();
            });
            var mapper = conf.CreateMapper();
            var dtos = mapper.Map<List<ProfileDto>>(entities);
            return dtos;
        }
    }
}