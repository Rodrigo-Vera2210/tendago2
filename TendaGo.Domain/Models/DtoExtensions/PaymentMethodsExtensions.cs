using AutoMapper;
using ER.BE;
using TendaGo.Common;

namespace TendaGo.Domain
{
    internal static class PaymentMethodsExtensions
    {
        internal static PaymentMethodDto ToPaymentMethodDto(this MedioPagoEntity entity)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<MedioPagoEntity, PaymentMethodDto>());
            var mapper = conf.CreateMapper();
            var dto = mapper.Map<PaymentMethodDto>(entity);
            return dto;
        }

        //internal static EntidadEntity ToEntidadProveedorEntity(this ProviderDto dto)
        //{
        //    var conf = new MapperConfiguration(config => config.CreateMap<ProviderDto, EntidadEntity>());
        //    var mapper = conf.CreateMapper();
        //    var entity = mapper.Map<EntidadEntity>(dto);
        //    return entity;
        //}
    }
}