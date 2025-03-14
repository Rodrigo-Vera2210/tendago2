using AutoMapper;
using ER.BE;
using ER.DA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //CreateMap<Custom_Usuario_LoadByTokenResult,UsuarioEntity>().ReverseMap();
        }
    }
}
