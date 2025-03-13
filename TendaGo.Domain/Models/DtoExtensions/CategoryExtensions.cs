using AutoMapper;
using ER.BE;
using TendaGo.Common;

namespace TendaGo.Domain
{
    internal static class CategoryExtensions
    {
        internal static CategoryDto ToCategoryDto(this CategoriaEntity entity)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<CategoriaEntity, CategoryDto>());
            var mapper = conf.CreateMapper();
            var dto = mapper.Map<CategoryDto>(entity);
            return dto;
        }

        internal static CategoriaEntity ToCategoryEntity(this CategoryDto dto)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<CategoryDto, CategoriaEntity>());
            var mapper = conf.CreateMapper();
            var entity = mapper.Map<CategoriaEntity>(dto);
            return entity;
        }
    }
}