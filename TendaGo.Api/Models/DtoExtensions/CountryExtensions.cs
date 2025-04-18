﻿using AutoMapper;
using ER.BE;

namespace TendaGo.Common
{
    public static class CountryExtensions
    {
        internal static CountryDto ToCountryDto(this PaisEntity entity)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<PaisEntity, CountryDto>());
            var mapper = conf.CreateMapper();
            var dto = mapper.Map<CountryDto>(entity);
            return dto;
        }

        internal static PaisEntity ToPaisEntity(this CategoryDto dto)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<CountryDto, PaisEntity>());
            var mapper = conf.CreateMapper();
            var entity = mapper.Map<PaisEntity>(dto);
            return entity;
        }
    }
}