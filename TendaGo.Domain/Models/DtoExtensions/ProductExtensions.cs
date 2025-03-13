using AutoMapper;
using ER.BE;

namespace TendaGo.Domain
{
    internal static class ProductExtensions
    {
        public static MapperConfiguration ProductMapper = new MapperConfiguration(config =>
        {
            config.CreateMap<ProductoEntity, LiteProductDto>()
                        .ForMember(option => option.CodigoProductoPadre, member => member.MapFrom(prop => prop.ProductoPadreAsProducto.CodigoInterno))
                        .ForMember(option => option.Categoria, member => member.MapFrom(prop => prop.IdCategoriaAsCategoria.Categoria))
                        .ForMember(option => option.Division, member => member.MapFrom(prop => prop.IdDivisionAsDivision.Division))
                        .ForMember(option => option.Linea, member => member.MapFrom(prop => prop.IdLineaAsLinea.Linea))
                        .ForMember(option => option.Marca, member => member.MapFrom(prop => prop.IdMarcaAsMarca.Marca))
                        .ForMember(option => option.NombreUnidadMedida, member => member.MapFrom(prop => prop.UnidadMedida_DisplayMember));

            config.CreateMap<ProductoEntity, ProductDto>()
                        .ForMember(option => option.CodigoProductoPadre, member => member.MapFrom(prop => prop.ProductoPadreAsProducto.CodigoInterno))
                        .ForMember(option => option.Categoria, member => member.MapFrom(prop => prop.IdCategoriaAsCategoria.Categoria))
                        .ForMember(option => option.Division, member => member.MapFrom(prop => prop.IdDivisionAsDivision.Division))
                        .ForMember(option => option.Linea, member => member.MapFrom(prop => prop.IdLineaAsLinea.Linea))
                        .ForMember(option => option.Marca, member => member.MapFrom(prop => prop.IdMarcaAsMarca.Marca))
                        .ForMember(option => option.NombreUnidadMedida, member => member.MapFrom(prop => prop.UnidadMedida_DisplayMember));
        });

        internal static LiteProductDto ToLiteProductDto(this ProductoEntity entity)
        {
            var mapper = ProductMapper.CreateMapper();
            var dto = mapper.Map<LiteProductDto>(entity);
            dto.PhotoUrl = dto.PathArchivo;
            return dto;
        }
        internal static ProductDto ToProductDto(this ProductoEntity entity)
        {
            var mapper = ProductMapper.CreateMapper();
            var dto = mapper.Map<ProductDto>(entity);
            dto.PhotoUrl = dto.PathArchivo;
            return dto;
        }
    }
}