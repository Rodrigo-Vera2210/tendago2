using AutoMapper;
using ER.BE;

namespace TendaGo.Domain
{
    internal static class ReceivableExtensions
    {
        internal static ReceivableDto ToReceivableDto(this CuentaPorCobrarEntity cxc)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<CuentaPorCobrarEntity, ReceivableDto>());
            var mapper = conf.CreateMapper();
            var cxcDto = mapper.Map<ReceivableDto>(cxc);
            return cxcDto;
        }
        internal static CuentaPorCobrarEntity ToReceivableEntity(this ReceivableDto cxcDto)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<ReceivableDto, CuentaPorCobrarEntity>());
            var mapper = conf.CreateMapper();
            var cxc = mapper.Map<CuentaPorCobrarEntity>(cxcDto);
            return cxc;
        }
    }
}