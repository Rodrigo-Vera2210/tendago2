using AutoMapper;
using ER.BE;

namespace TendaGo.Domain
{
    internal static class CierreCajaExtensions
    {
        internal static CashBalanceDto ToDto(this CierreCajaEntity entity)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<CierreCajaEntity, CashBalanceDto>());
            var mapper = conf.CreateMapper();
            var dto = mapper.Map<CashBalanceDto>(entity);
            return dto;
        }

        internal static CierreCajaEntity ToEntity(this CashBalanceRequestModel dto)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<CashBalanceRequestModel, CierreCajaEntity>());
            var mapper = conf.CreateMapper();
            var entity = mapper.Map<CierreCajaEntity>(dto);
            return entity;
        }

        internal static CierreCajaEntity ToEntity(this CashBalanceDto dto)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<CashBalanceDto, CierreCajaEntity>());
            var mapper = conf.CreateMapper();
            var entity = mapper.Map<CierreCajaEntity>(dto);
            return entity;
        }
    }
}