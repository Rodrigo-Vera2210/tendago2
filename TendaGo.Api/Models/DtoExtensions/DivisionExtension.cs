using AutoMapper;
using ER.BE;

namespace TendaGo.Common
{
    internal static class DivisionExtension
    {
        internal static DivisionDto ToDivisionDto(this DivisionEntity division)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<DivisionEntity, DivisionDto>());
            var mapper = conf.CreateMapper();
            var divisionDto = mapper.Map<DivisionDto>(division);
            return divisionDto;
        }

        internal static DivisionEntity ToDivisionEntity(this DivisionDto divisionDto)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<DivisionDto, DivisionEntity>());
            var mapper = conf.CreateMapper();
            var divisionEntity = mapper.Map<DivisionEntity>(divisionDto);
            return divisionEntity;
        }
    }
}