using AutoMapper;
using ER.BE;
using TendaGo.Common;

namespace TendaGo.Domain
{
    public static class DebtDtoExtensions
    {
        internal static DebtDto ToDebtDto(this CuentaPorPagarEntity entity)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<CuentaPorPagarEntity, DebtDto>());
            var mapper = conf.CreateMapper();
            var dto = mapper.Map<DebtDto>(entity);
            return dto;
        }

        internal static CuentaPorPagarEntity ToCuentaPorCobrarEntity(this DebtDto dto)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<DebtDto, CuentaPorPagarEntity>());
            var mapper = conf.CreateMapper();
            var entity = mapper.Map<CuentaPorPagarEntity>(dto);
            return entity;
        }
    }
}