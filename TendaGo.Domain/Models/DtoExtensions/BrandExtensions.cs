using AutoMapper;
using ER.BE;
using TendaGo.Common;

namespace TendaGo.Domain
{
    internal static class BrandExtensions
    {
        internal static BrandDto ToBrandDto(this MarcaEntity entity)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<MarcaEntity, BrandDto>());
            var mapper = conf.CreateMapper();
            var dto = mapper.Map<BrandDto>(entity);
            return dto;
        }

        internal static MarcaEntity ToMarcaEntity(this BrandDto dto)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<BrandDto, MarcaEntity>());
            var mapper = conf.CreateMapper();
            var entity = mapper.Map<MarcaEntity>(dto);
            return entity;
        }
    }
}