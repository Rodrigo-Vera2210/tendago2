﻿using AutoMapper;
using ER.BE;

namespace TendaGo.Common
{
    public static class CityExtensions
    {
        internal static CityDto ToCityDto(this CiudadEntity entity)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<CiudadEntity, CityDto>());
            var mapper = conf.CreateMapper();
            var dto = mapper.Map<CityDto>(entity);
            return dto;
        }

        internal static CiudadEntity ToCiudadEntity(this CategoryDto dto)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<CityDto, CiudadEntity>());
            var mapper = conf.CreateMapper();
            var entity = mapper.Map<CiudadEntity>(dto);
            return entity;
        }
    }
}