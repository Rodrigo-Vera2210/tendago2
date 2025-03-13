using AutoMapper;

namespace TendaGo.Domain
{
    internal static class CatalogsExtensions
    {
        internal static TOutput GlobalMapperConverter<TInput, TOutput>(this TInput tInput)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<TInput, TOutput>());
            var mapper = conf.CreateMapper();
            var tOutput = mapper.Map<TOutput>(tInput);
            return tOutput;
        }

    }
}