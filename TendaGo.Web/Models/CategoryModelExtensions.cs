using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TendaGo.Common;

namespace TendaGo.Web.Models
{

    internal static class CategoryModelExtensions
    {
        internal static CategoryDto ToCategoryDto(this CategoriaModel model)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<CategoriaModel, CategoryDto>());
            var mapper = conf.CreateMapper();
            var dto = mapper.Map<CategoryDto>(model);
            return dto;
        }

        internal static CategoriaModel ToCategoryModel(this CategoryDto dto)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<CategoryDto, CategoriaModel>());
            var mapper = conf.CreateMapper();
            var model = mapper.Map<CategoriaModel>(dto);
            return model;
        }
    }
}