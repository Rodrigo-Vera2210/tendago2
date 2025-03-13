using AutoMapper;
using ER.BE;
using TendaGo.Common;

namespace TendaGo.Domain
{
    public static class ProvinceExtensions
    {
        internal static ProvinceDto ToProvinceDto(this ProvinciaEntity entity)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<ProvinciaEntity, ProvinceDto>());
            var mapper = conf.CreateMapper();
            var dto = mapper.Map<ProvinceDto>(entity);
            return dto;
        }

        internal static ProvinciaEntity ToProvinciaEntity(this CategoryDto dto)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<ProvinceDto, ProvinciaEntity>());
            var mapper = conf.CreateMapper();
            var entity = mapper.Map<ProvinciaEntity>(dto);
            return entity;
        }
    }
}