using AutoMapper;
using ER.BE;
using ER.DA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Common;
using TendaGo.Domain.Models;

namespace TendaGo.BusinessLogic.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            MapTendaGo();
        }

        private void MapTendaGo()
        {
            CreateMap<Custom_Usuario_LoadByTokenResult,UsuarioDTO>().ReverseMap();

            #region Brand
            CreateMap<MarcaEntity, BrandDto>().ReverseMap();
            CreateMap<BrandDto, MarcaEntity>().ReverseMap();
            #endregion

            #region Categoria
            CreateMap<Categoria_LoadByPKResult, CategoriaDTO>();

            #endregion
        }
    }
}
