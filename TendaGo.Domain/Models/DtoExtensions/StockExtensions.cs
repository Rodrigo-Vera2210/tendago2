using AutoMapper;
using ER.BE;

namespace TendaGo.Domain.DtoExtensions
{
    public static class StockExtensions
    {
        internal static LiteStockDto ToLiteStockDto(this StockEntity entity)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<StockEntity, LiteStockDto>()
            .ForMember(option => option.NombreUnidadMedida, member => member.MapFrom(prop => prop.UnidadMedida_DisplayMember))
            );

            var mapper = conf.CreateMapper();

            var dto = mapper.Map<LiteStockDto>(entity);
            if (entity.IdProductoAsProducto != null)
            {
                dto.Producto = entity.IdProductoAsProducto.Producto;
                dto.Descripcion = entity.IdProductoAsProducto.Descripcion;
                dto.PathArchivo = entity.IdProductoAsProducto.PathArchivo;
                dto.CodigoInterno = entity.IdProductoAsProducto.CodigoInterno;
                dto.ProductoPadre = entity.IdProductoAsProducto.ProductoPadre;
            }

            if (entity.IdLocalAsLocal != null)
            {
                dto.Local = entity.IdLocalAsLocal.Local;
            }
            return dto;
        }
    }
}