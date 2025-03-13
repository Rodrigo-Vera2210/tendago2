using AutoMapper;
using ER.BE;

namespace TendaGo.Domain
{
    internal static class LineExtensions
    {
        internal static LineDto ToLineDto(this LineaEntity line)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<LineaEntity, LineDto>());
            var mapper = conf.CreateMapper();
            var lineDto = mapper.Map<LineDto>(line);
            return lineDto;
        }

        internal static LineaEntity ToLineEntity(this LineDto lineDto)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<LineDto, LineaEntity>());
            var mapper = conf.CreateMapper();
            var lineEntity = mapper.Map<LineaEntity>(lineDto);
            return lineEntity;
        }
    }
}